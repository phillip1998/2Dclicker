using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour {
    public float time;

    Collider go;
    [SerializeField]
    private Transform pos;
    private SoundController sc;
    void Awake()
    {
        sc = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
    }
    private void OnEnable()
    {
        gameObject.transform.position = pos.transform.position+new Vector3(0,0,2);
        go = GetComponent<Collider>();
        StartCoroutine(Waiting());
        go.gameObject.transform.localScale = new Vector3(1,1,1);
        sc.PlayEffectSound(eSoundEffect.exp_astroid);
    }
    private IEnumerator Waiting()
    {
        WaitForSeconds s1 = new WaitForSeconds(0.02f);
        for (int i = 0; i < 20; i++)
        {
            go.gameObject.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
            yield return s1;
        }
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.SendMessage("Bomb");
        }
    }
}
