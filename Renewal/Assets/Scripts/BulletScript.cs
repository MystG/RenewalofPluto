using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    // Use this for initialization
    public float damage;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<aiEasy>().damageEnemy(damage);

            Destroy(this.gameObject);
        }
        Destroy(this.gameObject);
    }
}
