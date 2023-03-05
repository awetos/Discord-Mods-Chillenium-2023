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

    public void SetIsDead(bool newIsDead)
    {
        isDead = newIsDead;
        myAnimator.SetBool("isDead", newIsDead);
    }

    public void Attack()
    {
        myAnimator.SetBool("isAttacking", true);
        StartCoroutine("ResetAttack");
    }


    IEnumerator ResetAttack()
    {
        yield return new WaitForEndOfFrame();
        myAnimator.SetBool("isAttacking", false);
    }

    float myRotation_X;
    float myRotation_y;
    float myRotation_z;
    void Start()
    {
        isAttacking = false;
        isDead = false;
        myAnimator = GetComponent<Animator>();

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
    bool isDead;
    // Update is called once per frame
    void Update()
    {
        AdjustRotation();
       CheckNavAgentDirection();
    }

    void CheckNavAgentDirection()
    {
        if(myAnimator.GetBool("IsDead")== true)
        {
            myAnimator.Play("Enemy_Die");
        }
        //ignore if it is attacking
        else if(isAttacking == false && isDead)
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
