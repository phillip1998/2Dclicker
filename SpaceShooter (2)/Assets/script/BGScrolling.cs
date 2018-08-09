using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScrolling : MonoBehaviour {
    Rigidbody rb;
    Vector3 rollbackamount = new Vector3(0, 0,40.96f);
    public float speed;
    // Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BGBumpr"))
        {
            rb.position += rollbackamount;
        }
    }

    public void StopScroll()
    {
        rb.velocity = new Vector3(0, 0, 0);
    }
    public void StartScroll()
    {
        rb.velocity = new Vector3(0, 0, -speed);
    }
    // Update is called once per frame
    void Update () {
	    
	}
}
