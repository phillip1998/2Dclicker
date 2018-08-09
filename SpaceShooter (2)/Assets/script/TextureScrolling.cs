using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScrolling : MonoBehaviour {

    Material mat;
	// Use this for initialization
	void Start () {
        Renderer r = GetComponent<Renderer>();
        mat = r.material;

	}
	
	// Update is called once per frame
	void Update () {
        mat.mainTextureOffset+=new Vector2(0, (float)0.1) * Time.deltaTime;
	}
}
