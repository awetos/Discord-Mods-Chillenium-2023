using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public delegate void EnemyDied(int enemyID);
    public static event EnemyDied OnEnemyDeath;
    public int MAX_HEALTH;
    public int health;

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
       
        if(MAX_HEALTH == 0)
        {
            MAX_HEALTH = 100;
        }
        health = MAX_HEALTH;
    }
    [Header("Damage Settings")]
    [SerializeField] HealthBar healthBar;
    public void TakeDamage(int damage)
    {
        if(health == MAX_HEALTH)
        {
            healthBar.MakeBarAppear();
        }
        health -= damage;
        OnEnemyTakeDamage(this.transform.position, damage);
        if(health <= 0)
        {
            isHurt = true;
            StartCoroutine("PushBack");
            StartCoroutine("resetHurt");

            if (this.gameObject.activeInHierarchy == false)
            {

            }
            else
            {
                DisableEnemy();
                deathLocation = transform.position;
            }

            DropCollectible();

            lockMovement = true;

            OnEnemyDeath(enemyID);

            StartCoroutine("DoNotMove");
        }
        else
        {
            float percent = (float)health / (float)MAX_HEALTH;

            healthBar.DecreaseHealth(percent);
            myController.Hurt();

            isHurt = true;
            StartCoroutine("PushBack");
            StartCoroutine("resetHurt");
        }
    }

    Vector3 deathLocation;
    void DisableEnemy()
    {
        myCollider.enabled = false;
        healthBar.Disappear();


        attackscript.StopAttacking();
        myController.Die();
      
    }

    //called by enemy spawner.
    public void ResetEnemy()
    {
        StopAllCoroutines();
        isHurt = false;
        myCollider.enabled = true;
        myController.ResetEnemyAfterDeath(true);
        lockMovement = false;
        health = MAX_HEALTH;


        healthBar.ResetHealthBar();
    }

     bool lockMovement;

     bool isHurt;
    IEnumerator PushBack()
    {
        Vector3 previousDirection = myEnemyAgent.velocity * -1.5f;

        for (int i = 0; i < FramesToMoveBack; i++)
        {
            myEnemyAgent.velocity = previousDirection;
            if (isHurt == false)
            {
                break;
            }
            yield return new WaitForEndOfFrame();

        }



        while(isHurt == true)
        {
            myEnemyAgent.velocity = previousDirection * 0.3f; //slow down
            if (isHurt == false)
            {
                break;
            }
            yield return new WaitForEndOfFrame();

        }
        yield return new WaitForEndOfFrame();
    }

    public int FramesToMoveBack = 20;
    public int FramesToHurtTotal = 80;

    IEnumerator resetHurt()
    {
        isHurt = true;

        for(int i = 0; i < FramesToHurtTotal; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        isHurt = false;
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
