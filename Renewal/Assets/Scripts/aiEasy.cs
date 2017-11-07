using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiEasy : MonoBehaviour {

    public float enemyMovementSpeed;
    public float fpsTargetDistance;
    public float enemyLookDistance;
    public float attackDistance;
    public float damping;
    public Transform fpsTarget;
    Rigidbody theRigidBody;
    Renderer myRender;
    
    // Use this for initialization
	void Start () {
        myRender = GetComponent<Renderer>();
        theRigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //attack radius logic  
        fpsTargetDistance = Vector3.Distance(fpsTarget.position, transform.position);
        if (fpsTargetDistance<enemyLookDistance)
        {
            myRender.material.color = Color.yellow;
            lookAtPlayer();
            //add "Do I hear something?" logic
        }
        else if (fpsTargetDistance < attackDistance)
        {
            myRender.material.color = Color.red;
            enemyAttack();
        }
	}

    void lookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(fpsTarget.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime + damping);
    }

    void enemyAttack()
    {
        //define attack movement pattern
        //define attack rate 
        // make controllable for difficulty
    }
}
