using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                attackscript.SetDamageAmount(0);
                transform.position = new Vector3(10,10,10);
                ResetEnemy();
               // StartCoroutine("DelayedSetActive");
            }



            myController.SetIsDead(true);

            DropCollectible();
            OnEnemyDeath(enemyID);
        }
    }

    void ResetEnemy()
    {
        health = 100;
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
