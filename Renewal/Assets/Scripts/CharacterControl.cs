using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {

    public float moveSpeed;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        //set the direction the player is facing to the camera's bearing
        Vector3 cameraBearing = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        transform.forward = cameraBearing;

        //poll the keyboard for directions and set the player's velocity
        Vector3 netVel = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            netVel += transform.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            netVel += -1 * transform.forward;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            netVel += -1 * transform.right;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            netVel += transform.right;
        }
        netVel = netVel.normalized * moveSpeed;
        rb.velocity = new Vector3(netVel.x, rb.velocity.y, netVel.z);
    }
}
