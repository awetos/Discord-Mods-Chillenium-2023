using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    
    Animator myAnimator;
    public GameObject myEnemy;
    public UnityEngine.AI.NavMeshAgent agent;
    // Start is called before the first frame update

    private void OnEnable()
    {
        
    }

    float myRotation_X;
    float myRotation_y;
    float myRotation_z;
    void Start()
    {
        isAttacking = false;
        isAlive = true;
        myAnimator = GetComponent<Animator>();

        myAnimator.Play("Enemy_Walk_Down");
        if(myEnemy == null)
        {
            myEnemy = transform.parent.gameObject;
        }
        myRotation_X = transform.eulerAngles.x;
        myRotation_y = transform.eulerAngles.y;
        myRotation_z = transform.eulerAngles.z;

    }
    void AdjustRotation()
    {
        myRotation_X = myEnemy.transform.rotation.x - 90f;
        transform.eulerAngles = new Vector3(90, 0, 0);
    }
    bool isAttacking;
    bool isAlive;
    // Update is called once per frame
    void Update()
    {
        AdjustRotation();

       CheckNavAgentDirection();


    }

    void CheckNavAgentDirection()
    {
        //ignore if it is attacking
        if(isAttacking == false && isAlive)
        {
            // Get the NavMeshAgent component attached to the object
           

            // Get the velocity vector of the agent
            Vector3 velocity = agent.velocity;

            // Get the forward vector of the agent
            Vector3 forward = transform.forward;

            // Calculate the cross product of the velocity and forward vectors
            Vector3 cross = Vector3.Cross(velocity, forward);

            // Check if the y component of the cross product is positive or negative
            if (cross.y > 0)
            {
                // The agent is moving to the right
                myAnimator.Play("Enemy_R_Walk");
            }
            else if (cross.y < 0)
            {
                // The agent is moving to the left
                myAnimator.Play("Enemy_L_Walk");

            }
            else
            {
                // The agent is not moving left or right
            }
        }
    }


}
