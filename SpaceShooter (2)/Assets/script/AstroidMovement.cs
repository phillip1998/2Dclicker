using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidMovement : MonoBehaviour {
    Rigidbody rb;
    [SerializeField]
    public float angularSpeed,speed;
    [SerializeField]
    int scourvalue;
    public int hp;
    private int currenthp;

    private GameController gc;
    private SoundController sc;

    // Use this for initialization
    void Awake () {
        currenthp = hp;
        rb = GetComponent<Rigidbody>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        sc = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();

    }
    private void OnEnable()
    {
        rb.angularVelocity = Random.onUnitSphere * angularSpeed;
        rb.velocity = Vector3.back * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);

            sc.PlayEffectSound(eSoundEffect.exp_astroid);
            GameObject effect = gc.GetEffect(eEffectType.expAstroid);
            effect.transform.position = transform.position;
            effect.SetActive(true);
        }
        if (other.gameObject.CompareTag("playerBolt"))
        {
            other.gameObject.SetActive(false);
            currenthp--;
            if (currenthp <= 0)
            {
                sc.PlayEffectSound(eSoundEffect.exp_astroid);
                GameObject effect = gc.GetEffect(eEffectType.expAstroid);
                effect.transform.position = transform.position;
                effect.SetActive(true);

                gameObject.SetActive(false);
                gc.addScore(scourvalue);
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
            sc.PlayEffectSound(eSoundEffect.exp_astroid);
            GameObject effect = gc.GetEffect(eEffectType.expAstroid);
            effect.transform.position = transform.position;
            effect.SetActive(true);

            gameObject.SetActive(false);
            gc.addScore(scourvalue);
            currenthp = hp;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
