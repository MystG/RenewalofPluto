using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraControl : MonoBehaviour {

    public float yFocus;
    public float upBounds;
    public float downBounds;
    public float maxBackDistance;
    public float maxRotateSpeed;
    public GameObject player;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        //set the focuspoint to yOffest above the player's position
        Vector3 focuspoint = player.transform.position + new Vector3(0, yFocus, 0);

        //get normalized vector in direction of focuspoint to camera
        Vector3 focusToCamera = (transform.position - focuspoint).normalized;

        //set the camera's distance from focuspoint to maxDistance
        transform.position = focuspoint + focusToCamera * maxBackDistance;
        
        //if a ray from the focuspoint toward the camera with the length of the camera's max distance dits a collider,
        //move the camera to the hit point so there is nothing between the player and the camera.
        RaycastHit hit;
        if (Physics.Raycast(focuspoint, focusToCamera, out hit, maxBackDistance))
        {
            float hitDistance = Vector3.Magnitude(transform.position = focuspoint + focusToCamera);
            transform.position = focuspoint + focusToCamera * hitDistance;
        }

        //clamp the cameras hight to between downBounds and upBounds of the player's position
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, focuspoint.y - downBounds, focuspoint.y + upBounds), transform.position.z);

        //have the camera rotate around the focuspoint if the mouse moves
        float horiz = Input.GetAxis("Mouse X");
        float vert = Input.GetAxis("Mouse Y");
        if (horiz!=0)
        {
            transform.RotateAround(focuspoint, transform.up, horiz * maxRotateSpeed * Time.deltaTime);
        }
        if (vert!=0)
        {
            transform.RotateAround(focuspoint, transform.right, -vert * maxRotateSpeed * Time.deltaTime);
        }

        //have the camera look at focuspoint
        transform.LookAt(focuspoint);
    }
}
