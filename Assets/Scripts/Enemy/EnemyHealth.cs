using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public delegate void EnemyDied(int enemyID);
    public static event EnemyDied OnEnemyDeath;
    public int health = 100;

    public int enemyID;


    //calls number canvas
    public delegate void EnemyTakeDamage(Vector3 _location, int damageAmount);
    public static event EnemyTakeDamage OnEnemyTakeDamage;

    public GameObject testtubePrefab;


    public EnemyAttack attackscript;
    public EnemyAnimationController myController;

    public NavMeshAgent myEnemyAgent;

    private void Start()
    {
       
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        OnEnemyTakeDamage(this.transform.position, damage);
        if(health <= 0)
        {

            if(this.gameObject.activeInHierarchy == false)
            {

            }
            else
            {
                DisableEnemy();
                deathLocation = transform.position;
               // StartCoroutine("DelayedSetActive");
            }



            DropCollectible();

            holdStillWhileDying = true;

            StartCoroutine("ResetHoldBool");

            StartCoroutine("DelayedResetEnemy");
        }
    }

    Vector3 deathLocation;
    void DisableEnemy()
    {
        attackscript.SetDamageAmount(0);
        myController.SetIsDead(true); //it will reset on its own
        
    }
    void ResetEnemy()
    {
        attackscript.ResetDamage();
        health = 100;
    }

    public bool holdStillWhileDying;
    IEnumerator ResetHoldBool()
    {
        yield return new WaitForSeconds(1.25f);
        holdStillWhileDying = false;
    }

    IEnumerator DelayedResetEnemy()
    {
        //let the death animation play.
        while(holdStillWhileDying == true)
        {
            transform.position = deathLocation;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        OnEnemyDeath(enemyID);
        ResetEnemy();
    }
    IEnumerator DelayedSetActive()
    {
        yield return new WaitForSeconds(0.5f);

        transform.gameObject.SetActive(false);
    }
    
    void DropCollectible()
    {
        GameObject drop = Instantiate(testtubePrefab);
       

        float x = this.transform.position.x;
        float y = 0.1f;
        float z = this.transform.position.z;

        Vector3 dropLocation = new Vector3(x, y, z);
        drop.transform.position = dropLocation;


    }
}
