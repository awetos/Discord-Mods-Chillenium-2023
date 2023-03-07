using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public delegate void attackPlayer(int damage);
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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            isAttacking = true;
            StartCoroutine("Attacking");
        }
    }

    IEnumerator Attacking()
    {
        
        while (isAttacking == true)
        {
            Camera.main.GetComponent<HealthReferences>().TakeDamage(damageAmount);
            OnAttackPlayer(damageAmount);


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
