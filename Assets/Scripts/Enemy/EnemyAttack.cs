using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

   

  [SerializeField]  bool isAttacking;
    [SerializeField] float attackSpeed;
    [SerializeField] public int damageAmount;

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
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            Debug.Log("dealing damage!");
            isAttacking = true;
        }
    }

    IEnumerator Attacking()
    {
        while (isAttacking == true)
        {
            Camera.main.GetComponent<HealthReferences>().TakeDamage(damageAmount);
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    private void OnCollisionExit()
    {
        isAttacking = false;
    }
}
