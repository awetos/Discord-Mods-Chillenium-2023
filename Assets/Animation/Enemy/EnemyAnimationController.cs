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
        StartCoroutine("ResetAfterDeath");
    }

    public void Attack()
    {
        myAnimator.SetBool("isAttacking", true);
        StartCoroutine("ResetAttack");
    }

    IEnumerator ResetAfterDeath()
    {
        yield return new WaitForEndOfFrame();
        myAnimator.SetBool("isDead", false);
        isDead = false;
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

    public Vector3 cross;

    public enum MoveDirection
    {
        up, down, left, right
    }

    [SerializeField]
    MoveDirection currentDirection;
    [SerializeField]
    MoveDirection nextDirection;

    void CheckNavAgentDirection()
    {
        if(myAnimator.GetBool("IsDead")== true)
        {
            myAnimator.Play("Enemy_Die");
        }
        //ignore if it is attacking
        else if(isAttacking == false && isDead == false)
        {
            // Get the NavMeshAgent component attached to the object
           
            Vector3 velocity = agent.velocity;

            Vector3 forward = transform.forward;

            cross = Vector3.Cross(velocity, forward);

            if (cross.x > 0.3 || cross.x < -0.3)
            {
                myAnimator.SetBool("prioritizeUpDown", true);
                myAnimator.SetFloat("Vertical", cross.x);
            }
            else 
            {
                myAnimator.SetBool("prioritizeUpDown", false);
                myAnimator.SetFloat("Horizontal", cross.y);
            }
            /*
            if (cross.x > 0.3 || cross.x < -0.3)
            {
                myAnimator.SetFloat("Vertical", cross.x);
            }
            else if (cross.y > 0)
            {
                // The agent is moving to the right
                myAnimator.SetFloat("Horizontal", cross.y);
                nextDirection = MoveDirection.right;
            }
            else if (cross.y < 0)
            {
                // The agent is moving to the left
                nextDirection = MoveDirection.left;

            }
            else
            {
                //continue
            }
            */
        }
    }

    void SetDirection(MoveDirection next)
    {
        if(next == currentDirection)
        {
            //do nothing, you are already moving in that direction
        }
        else
        {

        }

    }

    void ClearDirectionBools()
    {

    }


}
