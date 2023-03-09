using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public delegate void attackPlayer(int damage, int playerID);
    public static event attackPlayer OnAttackPlayer;

    [SerializeField] NavMeshAgent myAgent;

  [SerializeField]  bool isAttacking;
    [SerializeField] float attackSpeed;



    [SerializeField] public int DEFAULT_DAMAGE;

    int damageAmount;

    public EnemyAnimationController myAnimationController;
    float walkingSpeed;//to pause when attacking.
    float acceleration;
    public void SetDamageAmount(int newAmount)
    {
        damageAmount = newAmount;
    }

    public void ResetDamage()
    {
        damageAmount = DEFAULT_DAMAGE;
    }
    private void Start()
    {
        isAttacking = false;
        if(attackSpeed == 0)
        {
            attackSpeed = 0.5f;
        }
        if(damageAmount == 0)
        {
            damageAmount = 10;
        }
        if (myAgent == null)
        {
            myAgent = GetComponent<NavMeshAgent>();
        }
        if(myAnimationController == null)
        {
            myAnimationController = GetComponentInChildren<EnemyAnimationController>();
        }

        walkingSpeed = myAgent.speed;
        acceleration = myAgent.acceleration;
    }

    int playerID;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            if(collision.gameObject.GetComponent<HealthManager>().isPlayerOne == true)
            {
                playerID = 0;
            }
            else
            {
                playerID = 1;
            }
           
            isAttacking = true;
            StartCoroutine("Attacking");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            isAttacking = true;

        }
    }

    IEnumerator Attacking()
    {
        
        while (isAttacking == true)
        {
            OnAttackPlayer(damageAmount, playerID);


            myAnimationController.Attack();
            myAgent.speed = 0;
            myAgent.acceleration = 0;
            yield return new WaitForSeconds(attackSpeed);

        }
        myAgent.speed = 0;
        myAgent.acceleration = 0;
        yield return new WaitForSeconds(1f);
        myAgent.acceleration = acceleration;
        myAgent.speed = walkingSpeed;
    }


    private void OnCollisionExit()
    {
        isAttacking = false;
    }
}
