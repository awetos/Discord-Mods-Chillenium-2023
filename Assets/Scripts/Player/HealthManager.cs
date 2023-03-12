using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using static UnityEditor.FilePathAttribute;

public class HealthManager : MonoBehaviour{
	public delegate void deathdelegate(int PlayerID);
	public static event deathdelegate OnPlayerDeath;

    public delegate void takeDamage( Transform _location, int damagedealt);
    public static event takeDamage OnTakeDamage;


    [SerializeField] HeartAnimationManager myHeartHalf;
    public int MAX_HEALTH;
    int health;



	public int PlayerID; //0 if player1, 1 if player 2.


	[SerializeField]private DeathScreen deathScreen;


	public bool isDead;

	void Start() {
		if (MAX_HEALTH == 0)
		{
            MAX_HEALTH = 100;
		}
        health = MAX_HEALTH;


		if(PlayerID == 0)
		{
			startAnim();
		}

        isDead = false;
    }

	//does not call the attack text.
	public void reduceHealth(){
        health -= 1;

        if (health <= 0)
        {
            Death();
        }

        percent = ((float)health) / ((float)MAX_HEALTH);
        myHeartHalf.UpdateCurrentSpriteFromPercent(percent);

    }
	
	//space reduces player health for testing purposes
	void Update() {
		//if player's health is below zero, stop repeating the command
		if(health<=0){
			cancelAnim();
			isDead = true;
			Death();
			//DEATH
		}

		
	}

	bool isDecaying;
	public void startAnim(){

		isDecaying = true;
        //in case it was already started on start, you don't want to call it twice when switching for the first time.
        StopCoroutine("ReduceHealth"); 

        StartCoroutine("ReduceHealth");
	}
	public void cancelAnim(){
		isDecaying = false;
		StopCoroutine("ReduceHealth");
	}

	IEnumerator ReduceHealth()
	{
		while (isDecaying == true)
		{
			reduceHealth();
            yield return new WaitForSeconds(1f);
        }
		
	}
	public float percent;
	public void TakeDamage(int damage)
	{
		health -= damage;

		OnTakeDamage( transform, damage);
		if(health <= 0)
		{
			Death();
		}

		 percent = ((float)health) / ((float)MAX_HEALTH);
        myHeartHalf.UpdateCurrentSpriteFromPercent(percent);
    }
    public void AddHealth(int healthToAdd)
    {
		health += healthToAdd;


        if (health > MAX_HEALTH)
        {
			health = MAX_HEALTH;
        }
        percent = ((float)health) / ((float)MAX_HEALTH);
        myHeartHalf.UpdateCurrentSpriteFromPercent(percent);

    }
    public void Death(){

		isDead = true;
		deathScreen.ShowDeathScreen();;

		OnPlayerDeath(PlayerID);
	}

}
