using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    Rigidbody rb;
    public float speed;
    public float rot;
    public float fireRate;
    private float currentFireRate;
    public Transform BoltPos;
    [SerializeField]
    public BoltPool BoltP;
    [SerializeField]
    public bomb bomb;
    public int maxhp;
    private int hp;
    private GameController gc;
    private SoundController sc;
    public int defaultLife;
    private int playerLife;
    private bool moving;
    // Use this for initialization
    void Awake () {
        moving = false;
        currentFireRate = 0;
        rb = GetComponent<Rigidbody>();
        sc = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerLife = defaultLife;
    }
    private void OnEnable()
    {
        moving = false;
        hp = maxhp;
        rb.transform.position = new Vector3(0, 0, -10);
        rb.velocity = new Vector3(0, 0, 2) * speed;
        StartCoroutine(checking());
    }
    private IEnumerator checking()
    {
        WaitForSeconds s1 = new WaitForSeconds(0.1f);
        while (rb.position.z <= -2)
        {
            yield return s1;
        }
        moving = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && moving)
        {
            hp--;;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && moving)
        {
            hp--; ;
        }
    }
    public void setHP(int value)
    {
        hp=value;
        gc.setHP(hp);
    }
    public void respawn()
    {

    }
    public void setLife()
    {
        playerLife = defaultLife;
        gc.setLife(playerLife);
    }
    // Update is called once per frame
    void Update () {

        gc.setHP(hp);
        if (hp <= 0)
        {
            gameObject.SetActive(false);
            sc.PlayEffectSound(eSoundEffect.exp_player);
            GameObject effect = gc.GetEffect(eEffectType.expPlayer);
            effect.transform.position = transform.position;
            effect.SetActive(true);

            playerLife--;
            gc.setLife(playerLife);
            if (playerLife <= 0)
            {
                gc.GameOver();
            }
            else
            {
                gc.RemoveAll();
                setHP(maxhp);
                gameObject.SetActive(true);
            }
        }

        if (moving)
        {

            float horizontal = Input.GetAxis("Horizontal") * speed;
            float vertical = Input.GetAxis("Vertical") * speed;

            rb.velocity = new Vector3(horizontal, 0, vertical);
            rb.position = new Vector3(Mathf.Clamp(rb.position.x, -5, 5), 0, Mathf.Clamp(rb.position.z, -4, 10));
            rb.rotation = Quaternion.Euler(0, 0, horizontal * -rot);

            currentFireRate -= Time.deltaTime;
            if (currentFireRate <= 0 && Input.GetButton("Shoot"))
            {
                currentFireRate = fireRate;
                Bolt temp = BoltP.GetFromPool();
                temp.gameObject.SetActive(true);
                temp.transform.position = BoltPos.position;
                sc.PlayEffectSound(eSoundEffect.wea_player);
            }
            if ( Input.GetKeyDown(KeyCode.X))
            {
                bomb.gameObject.SetActive(true);
            }
        }
    }
}
