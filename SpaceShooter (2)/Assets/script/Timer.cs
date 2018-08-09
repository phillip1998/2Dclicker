using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    [SerializeField]
    private float time;
	// Use this for initialization
	void Start () {
		
	}
    private void OnEnable()
    {
        StartCoroutine(Waiting());
    }
    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
