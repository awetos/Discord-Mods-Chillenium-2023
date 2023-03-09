using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class HealthManager : MonoBehaviour{



    [SerializeField] HeartAnimationManager myHeartHalf;
    public int MAX_HEALTH;
    int health;


    public bool isPlayerOne;//true if is attached to first player
	int i=0;
	[SerializeField]private GameObject deathScreen;
	[SerializeField]private Timer timer;
	[SerializeField]private TextMeshProUGUI leaderboardTxt;
	[SerializeField]private AudioSource gameover;
	[SerializeField]private GameObject aimer;
	[SerializeField]private GameObject fister;
	private bool playedOnce = false;

	public bool isDead;

	void Start() {
		if (MAX_HEALTH == 0)
		{
            MAX_HEALTH = 100;
		}
        health = MAX_HEALTH;


        leaderboardTxt.text = "Best: " + PlayerPrefs.GetString("Time");

		if(isPlayerOne == false)
		{
			startAnim();
		}

        isDead = false;
    }

	public void reduceHealth(){
		TakeDamage(1);
		
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

        //show death screen
        if (PlayerPrefs.GetFloat("HighTime") <= 0)
            PlayerPrefs.SetFloat("HighTime", 0);
        if (PlayerPrefs.GetString("Time") == null)
            PlayerPrefs.SetString("Time", "00:00:00");
        if (timer.timeElapsed > PlayerPrefs.GetFloat("HighTime"))
        {
            PlayerPrefs.SetFloat("HighTime", timer.timeElapsed);
            PlayerPrefs.SetString("Time", timer.timeTxt);
        }
        leaderboardTxt.text = "Best: " + PlayerPrefs.GetString("Time");
        GetComponent<PlayerMovement>().enabled = false;
        timer.StopTimer();
		aimer.SetActive(false);
		fister.SetActive(false);
        StartCoroutine("ShowDeathScreenAfterTime");
	}

	float deathDelay = 2.5f;
	IEnumerator ShowDeathScreenAfterTime()
	{
		yield return new WaitForSeconds(deathDelay);
		Camera.main.GetComponent<AudioSource>().Stop();
		if(!playedOnce){
			gameover.Play();
			print(gameover.clip+" "+gameover.isPlaying);
			playedOnce = true;
		}
        deathScreen.SetActive(true);
        GetComponent<HealthManager>().enabled = false;
    }
}
