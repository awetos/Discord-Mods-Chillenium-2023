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
    public Collider myCollider;
    public EnemyAnimationController myController;

    public NavMeshAgent myEnemyAgent;

    private void Start()
    {
        if(myCollider == null)
        {
            myCollider = GetComponent<Collider>();
        }
       
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

            lockMovement = true;

            //StartCoroutine("ResetHoldBool");

            OnEnemyDeath(enemyID);

            StartCoroutine("DoNotMove");
        }
    }

    Vector3 deathLocation;
    void DisableEnemy()
    {
        myCollider.enabled = false;
        myController.Die(); 
        
    }

    //called by enemy spawner.
    public void ResetEnemy()
    {
        myCollider.enabled = true;
        myController.ResetEnemyAfterDeath(true);
        lockMovement = false;
        health = 100;
    }

    public bool lockMovement;
    IEnumerator ResetHoldBool()
    {
        yield return new WaitForSeconds(1.25f);
        lockMovement = false;
    }

    IEnumerator DoNotMove()
    {
        //let the death animation play.
        while(lockMovement == true)
        {
            transform.position = deathLocation;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        //ResetEnemy();
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
