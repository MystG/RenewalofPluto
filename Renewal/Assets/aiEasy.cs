using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiEasy : MonoBehaviour {

    public float enemyMovementSpeed;
    public float fpsTargetDistance;
    public float enemyLookDistance;
    public float attackDistance;
    public float damping;
    public float aggression; //delta time between each attack
    public Transform playerTarget;
    Rigidbody theRigidBody;
    Renderer myRender;
    static Animator anim;
    
    // Use this for initialization
	void Start () {
        myRender = GetComponent<Renderer>();
        theRigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //attack radius logic  
        fpsTargetDistance = Vector3.Distance(playerTarget.position, transform.position);
        if (fpsTargetDistance<enemyLookDistance)
        {
            myRender.material.color = Color.yellow;
            lookAtPlayer();
            //add "Do I hear something?" logic
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
        }
        else if (fpsTargetDistance < attackDistance)
        {
            myRender.material.color = Color.red;
            enemyAttack();
        }
        else
        {
            wonder();
        }
	}

    void lookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(playerTarget.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime + damping);
    }

    void enemyAttack()
    {
        float currentTime = 0.0f;
        //define attack movement pattern
        //define attack rate 
        if (Time.time > currentTime)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", true);
            currentTime = Time.time + aggression;
            GameObject clone = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        }
        // make controllable for difficulty
    }

    void wonder()
    {
        //move around navmesh or patrol path
        anim.SetBool("isIdle", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);
    }
}
