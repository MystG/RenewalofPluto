using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiEasy : MonoBehaviour {

    public float curHealth;
    public float enemyMovementSpeed;
    public float fpsTargetDistance;
    public float enemyLookDistance;
    public float attackDistance;
    public float damping;
    public Transform fpsTarget;
    Rigidbody theRigidBody;
    Renderer myRender;
    public Animator anim;

    private bool active;
    // Use this for initialization
	void Start () {
        myRender = GetComponent<Renderer>();
        theRigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        active = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //attack radius logic  
        if (!active) return;

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

    public void damageEnemy(float damagePerHit)
    {
        curHealth = curHealth - damagePerHit;
        if (curHealth <= 0 && active)
        {
            anim.SetBool("isDying", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
            DestroyObject(this.gameObject, 3.0f);
            active = false;
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
