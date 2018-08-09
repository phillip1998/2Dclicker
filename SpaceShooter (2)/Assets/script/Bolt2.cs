using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt2 : MonoBehaviour {

    Rigidbody rb;
    public float speed;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    public void Bomb()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
