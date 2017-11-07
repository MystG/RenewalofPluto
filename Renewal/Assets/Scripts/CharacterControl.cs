using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {

    public float moveSpeed; //speed the character moves at
    public float jumpForce; //force with which the player jumps when Space is pressed
    public float fallAccel; //additional downward acceleration (aside from gravity), applied to the player when falling while Space is not held
    public Texture2D crosshair; //image to use for the crosshair
    public GameObject bullet; //the object that will be created when player fires
    public float bulletStartOffset; //distance in front of the player the bullets spawn
    public float bulletSpeed; //speed that the bullet will be set to when fired
    public float maxAimDistance; //if the object thte crosshair is pointed at is within this distance, the bullet will home at the object
    public float fireCooldown; //the minimum time between firing

    private Rigidbody rb;

    private bool grounded;

    private float fireTimer;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        grounded = false;
        fireTimer = 0;
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
        
        //shooting control
        if (Input.GetMouseButtonDown(0) && fireTimer <= 0)
        {
            //transform.forward = cameraBearing;

            GameObject shot = Instantiate(bullet, transform.position + cameraBearing * bulletStartOffset, Quaternion.identity);
            
            //if a ray from the camera through the crosshair hits something, set the target to that point.
            //otherwise, set the target to the maxAimDistance in the direction of that ray
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            Vector3 playerToTarget;
            if(Physics.Raycast(ray, out hit, maxAimDistance))
            {
                playerToTarget = (hit.point - shot.transform.position).normalized;
            }
            else
            {
                Vector3 target = ray.direction * maxAimDistance + transform.position;
                playerToTarget = (target - shot.transform.position).normalized;
            }

            //set the velocity of the bullet toward the target at the bulletSpeed
            shot.transform.forward = playerToTarget;
            shot.GetComponent<Rigidbody>().velocity = playerToTarget * bulletSpeed;

            fireTimer = fireCooldown;
        }
        fireTimer = Mathf.Clamp(fireTimer - Time.deltaTime, 0, fireCooldown);
    }

    void OnCollisionStay(Collision other)
    {
        grounded = true;
    }

    void OnCollisionExit(Collision other)
    {
        grounded = false;
    }

    void OnGUI()
    {
        float xMin = (Screen.width / 2) - (crosshair.width / 2);
        float yMin = (Screen.height / 2) - (crosshair.height / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, crosshair.width, crosshair.height), crosshair);
    }
}
