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
    public float projectileSpeed;
    public Transform playerTarget;
    Rigidbody theRigidBody;
    Renderer myRender;
    static Animator anim;
    public GameObject blob;
    public Transform projector;
    private float currentTime = 0.0f;

    // Use this for initialization
    void Start () {
        myRender = GetComponent<Renderer>();
        theRigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //blob = GetComponent<GameObject>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //attack radius logic  
        fpsTargetDistance = Vector3.Distance(playerTarget.position, transform.position);
        if (fpsTargetDistance < attackDistance)
        {
            myRender.material.color = Color.red;
            enemyAttack();
        }
        else if(fpsTargetDistance < enemyLookDistance)
        {
            myRender.material.color = Color.yellow;
            lookAtPlayer();
            //add "Do I hear something?" logic
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
        }
        else
        {
            myRender.material.color = Color.grey;
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
        //define attack movement pattern
        // attack rate 
        if (Time.time > currentTime)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", true);
            print("shot");
            GameObject clone = (GameObject)Instantiate(blob, projector.position, projector.rotation);
            clone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(projector.forward * projectileSpeed);
            currentTime = Time.time + aggression;
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
