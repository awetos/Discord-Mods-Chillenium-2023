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

    public Animator myAnimator;
    public void TakeDamage(int damage)
    {
        health -= damage;
        OnEnemyTakeDamage(this.transform.position, damage);
        if(health <= 0)
        {

            //Destroy(this.gameObject);
            StartCoroutine("DelayedSetActive");
            
            myAnimator.Play("Enemy_Die");
            DropCollectible();
            OnEnemyDeath(enemyID);
        }
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
