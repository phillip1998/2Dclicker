using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    private Rigidbody rb;
    [SerializeField]
    private Transform boltPos;
    private BoltPool BoltP;
    private Bolt bolt;
    float speed;
    [SerializeField]
    private int hp;
    private int currenthp;
    private GameController gc;
    private SoundController sc;
    private int score;
	// Use this for initialization

	void Awake () {
        rb = GetComponent<Rigidbody>();
        speed = 1;
        currenthp = hp;
        score = 3;
        sc = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //BoltP = GameObject.FindGameObjectWithTag("BoltPool").GetComponent<BoltPool>();
	}
    public void OnEnable()
    {
        rb.velocity = Vector3.back * speed;
        StartCoroutine(AutoShoot());
        StartCoroutine(AutoMovement());
    }
    public void setBoltPool(BoltPool boltP)
    {
        BoltP = boltP;
    }
    public void RemoveAll()
    {
        BoltP.RemoveAll();
    }
    private IEnumerator AutoMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            float moveForce = Random.Range(2f, 3f);
            float moveTime = Random.Range(1f, 2f);
            int position;
            if (rb.position.x >= 2)
            {
                position = 0;
            } else if (rb.position.x <= -2) {
                position = 1;
            } else
            {
                if (Random.Range(0, 2) == 0) position =0;
                else position =1;
            }

            switch (position) {
                case 0 :
                     rb.velocity += Vector3.left * moveForce;

                    break;
                case 1:
                    rb.velocity += Vector3.right * moveForce;

                    break;
            }
            yield return new WaitForSeconds(moveTime);

            switch (position)
            {
                case 0:
                    rb.velocity -= Vector3.left * moveForce;
                    break;
                case 1:
                    rb.velocity -= Vector3.right * moveForce;
                    break;
            }

        }
    }
    private IEnumerator AutoShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            bolt =BoltP.GetFromPool();
            bolt.transform.position = boltPos.position;
            bolt.gameObject.SetActive(true);
            sc.PlayEffectSound(eSoundEffect.wea_enemy);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);

            sc.PlayEffectSound(eSoundEffect.exp_enemy);
            GameObject effect = gc.GetEffect(eEffectType.expEnemy);
            effect.transform.position = transform.position;
            effect.SetActive(true);
        }
        if (other.gameObject.CompareTag("playerBolt"))
        {
            other.gameObject.SetActive(false);
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
            //TODO 점수 올리기
        }
    }
    public void Bomb()
    {
        currenthp -= 10;
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
    void Update ()
    {
        rb.rotation = Quaternion.Euler(0, 180, rb.velocity.x*10);
        float y = rb.position.y;
        float z = rb.position.z;
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, -5, 5),y,z );
    }
}
