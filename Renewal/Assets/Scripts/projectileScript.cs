using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour {
    public float expireIn;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, expireIn);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
