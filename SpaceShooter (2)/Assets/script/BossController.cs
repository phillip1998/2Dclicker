using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
    private Rigidbody rb;
    [SerializeField]
    private Transform boltPos;
    private BoltPool2 BoltP;
    private Bolt2 bolt;
    float speed;
    [SerializeField]
    private int hp;
    private int currenthp;
    private GameController gc;
    private SoundController sc;
    private int score;

    private bool attackStart;
    // Use this for initialization

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        speed = 1;
        currenthp = hp;
        score = 20;
        sc = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    public void OnEnable()
    {
        attackStart = false;
        rb.velocity = Vector3.back * speed;
        StartCoroutine(AutoShoot());
        StartCoroutine(BossPhase());
    }
    public void setBoltPool(BoltPool2 boltP)
    {
        BoltP = boltP;
    }
    public void RemoveAll()
    {
        BoltP.RemoveAll();
    }
    private IEnumerator BossPhase()
    {
        WaitForSeconds s1 = new WaitForSeconds(.2f);
        WaitForSeconds s2 = new WaitForSeconds(2.5f);
        WaitForSeconds s3 = new WaitForSeconds(5f);
        while (rb.position.z>12.2f)
        {
            yield return s1;
        }
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(1);
        attackStart = true;
        while (attackStart)
        {
            rb.velocity = Vector3.left * speed;
            yield return s2;
            rb.velocity = Vector3.right * speed;
            yield return s3;
            rb.velocity = Vector3.left * speed;
            yield return s2;
        }
    }
    private IEnumerator AutoShoot()
    {
        int degree = 0;
        int speed = 10;
        WaitForSeconds s1 = new WaitForSeconds(1.5f);
        
        while (true)
        {
            yield return s1;
            if (attackStart)
            {

                degree = 0;
                bolt = BoltP.GetFromPool();
                bolt.transform.position = boltPos.position;
                bolt.gameObject.SetActive(true);
                bolt.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(Mathf.PI / 180 * degree), 0, -Mathf.Cos(Mathf.PI / 180 * degree)) * speed;
                bolt.GetComponent<Rigidbody>().rotation = Quaternion.Euler(90, -degree, 0);


                degree = 13;
                bolt = BoltP.GetFromPool();
                bolt.transform.position = boltPos.position + new Vector3(0.3f, 0, 0);
                bolt.gameObject.SetActive(true);
                bolt.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(Mathf.PI / 180 * degree), 0, -Mathf.Cos(Mathf.PI / 180 * degree)) * speed;
                bolt.GetComponent<Rigidbody>().rotation = Quaternion.Euler(90, -degree, 0);

                degree = -13;
                bolt = BoltP.GetFromPool();
                bolt.transform.position = boltPos.position + new Vector3(-0.3f, 0, 0);
                bolt.gameObject.SetActive(true);
                bolt.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(Mathf.PI / 180 * degree), 0, -Mathf.Cos(Mathf.PI / 180 * degree)) * speed;
                bolt.GetComponent<Rigidbody>().rotation = Quaternion.Euler(90, -degree, 0);


                sc.PlayEffectSound(eSoundEffect.wea_enemy);
            }
        }
      
    }

    private void OnTriggerEnter(Collider other)
    {
        
            if (other.gameObject.CompareTag("playerBolt"))
            {
                other.gameObject.SetActive(false);
               
            if (attackStart)
            {
                currenthp--;
                if (currenthp <= 0)
                {
                    sc.PlayEffectSound(eSoundEffect.exp_enemy);
                    GameObject effect = gc.GetEffect(eEffectType.expEnemy);
                    effect.transform.position = transform.position;
                    effect.SetActive(true);
                    gameObject.SetActive(false);
                    gc.addScore(score);
                    currenthp = hp;
                }
            }
            //TODO 점수 올리기
        }
    }
    public void Bomb()
    {
        currenthp-=10;
        if (currenthp <= 0)
        {
            sc.PlayEffectSound(eSoundEffect.exp_enemy);
            GameObject effect = gc.GetEffect(eEffectType.expEnemy);
            effect.transform.position = transform.position;
            effect.SetActive(true);
            gameObject.SetActive(false);
            gc.addScore(score);
            currenthp = hp;
        }
    }
    // Update is called once per frame
    void Update()
    {
        float y = rb.position.y;
        float z = rb.position.z;
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, -5, 5), y, z);
    }
}

