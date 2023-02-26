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

    public GameObject normalController;
    public GameObject deathController;
    public Animator normalAnimator;
    /*
    private void Start()
    {
        WakeUpAnimators();
    }

    void WakeUpAnimators()
    {
        TurnOnNormalAnimator();
        TurnOffDeathAnimator();
    }

    void OnDeathAnimators()
    {
        TurnOffNormalAnimator();
        TurnOnDeathAnimator();
    }

    public SpriteRenderer normalRenderer;
    public Animator normalAnimator;

    public SpriteRenderer deathRenderer;
    public Animator deathAnimator;
    void TurnOnNormalAnimator()
    {

        normalRenderer.enabled = true;
        normalAnimator.enabled = true;
        normalController.SetActive(true);
    }

    void TurnOffNormalAnimator()
    {

        normalRenderer.enabled = false;
        normalAnimator.enabled = false;
        normalController.SetActive(false);
    }

    void TurnOnDeathAnimator()
    {
        deathRenderer.enabled = true;
        deathAnimator.enabled = true;
        deathController.SetActive(true);
    }

    void TurnOffDeathAnimator()
    {
        deathRenderer.enabled = false;
        deathAnimator.enabled = false;
        deathController.SetActive(false);
    }
    */
    public void TakeDamage(int damage)
    {
        health -= damage;
        OnEnemyTakeDamage(this.transform.position, damage);
        if(health <= 0)
        {

            //Destroy(this.gameObject);
            if(this.gameObject.activeInHierarchy == false)
            {

            }
            else
            {
                StartCoroutine("DelayedSetActive");
            }
           


            normalAnimator.SetBool("IsDead", true);

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
