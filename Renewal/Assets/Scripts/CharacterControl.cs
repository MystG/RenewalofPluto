using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {

    public float moveSpeed; //speed the character moves at
    public float jumpForce; //force with which the player jumps when Space is pressed
    public float fallAccel; //additional downward acceleration (aside from gravity), applied to the player when falling while Space is not held

    private Rigidbody rb;

    private bool grounded;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        grounded = true;
    }
	
	// Update is called once per frame
	void Update () {

        //set the direction the player is facing to the camera's bearing
        Vector3 cameraBearing = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;

        //poll the keyboard for directions and set the player's velocity
        Vector3 netVel = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            netVel += cameraBearing;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            netVel += -1 * cameraBearing;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            netVel += -1 * new Vector3(cameraBearing.z, 0, -1 * cameraBearing.x);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            netVel += new Vector3(cameraBearing.z, 0, -1 * cameraBearing.x);
        }

        if (netVel.magnitude != 0)
        {
            transform.forward = netVel.normalized;
        }
        netVel = netVel.normalized * moveSpeed;
        rb.velocity = new Vector3(netVel.x, rb.velocity.y, netVel.z);

        //when player presses Space while character is on the ground, add a jump impulse
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        //while player is not on ground ad Space is not held, add an additional acceleration downward
        if (!Input.GetKey(KeyCode.Space) && !grounded)
        {
            rb.AddForce(-1 * Vector3.up * fallAccel, ForceMode.Acceleration);
        }
    }

    void OnCollisionStay(Collision other)
    {
        grounded = true;
    }

    void OnCollisionExit(Collision other)
    {
        grounded = false;
    }
}
