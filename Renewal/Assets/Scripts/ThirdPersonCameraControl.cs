using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraControl : MonoBehaviour {

    public float yFocus; //units above the player that the camera will look at
    public float upBounds; //maximum units above the player (y axis) that the camera can be above the focus point
    public float downBounds; //maximum units below the player (y axis) that the camera can be above the focus point
    public float maxBackDistance; //maximum distance that the camera can befrom the focuspoint
    public float maxRotateSpeed; //max degrees per second that the camera rotates
    public GameObject player; //player that this camera follows

    private Vector3 focusToCamera;

	// Use this for initialization
	void Start () {
        focusToCamera = (transform.position - ( player.transform.position + new Vector3(0, yFocus, 0) )).normalized * maxBackDistance;
    }
	
	// Update is called once per frame
	void Update () {

        //set the focuspoint to yOffest above the player's position
        Vector3 focuspoint = player.transform.position + new Vector3(0, yFocus, 0);

        /*
        //get normalized vector in direction of focuspoint to camera
        Vector3 focusToCamera = (transform.position - focuspoint).normalized;
        */

        //set the camera's distance from focuspoint to maxDistance
        transform.position = focuspoint + focusToCamera * maxBackDistance;
        
        //if a ray from the focuspoint toward the camera with the length of the camera's max distance dits a collider,
        //move the camera to the hit point so there is nothing between the player and the camera.
        RaycastHit hit;
        if (Physics.Raycast(focuspoint, focusToCamera, out hit, maxBackDistance))
        {
            transform.position = hit.point;
        }

        //clamp the cameras hight to between downBounds and upBounds of the player's position
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, focuspoint.y - downBounds, focuspoint.y + upBounds), transform.position.z);

        //have the camera rotate around the focuspoint if the mouse moves
        float horiz = Input.GetAxis("Mouse X");
        float vert = Input.GetAxis("Mouse Y");
        if (horiz!=0)
        {
            transform.RotateAround(focuspoint, transform.up, horiz * maxRotateSpeed * Time.deltaTime);
            focusToCamera = (transform.position - focuspoint).normalized;
        }
        if (vert!=0)
        {
            transform.RotateAround(focuspoint, transform.right, -vert * maxRotateSpeed * Time.deltaTime);
            focusToCamera = (transform.position - focuspoint).normalized;
        }

        //have the camera look at focuspoint
        transform.LookAt(focuspoint);
    }
}
