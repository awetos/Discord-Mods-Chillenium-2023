using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour{
    public float health = 100;//player current health percent
	public Scrollbar health1;//health bar for player 1
	public Scrollbar health2;//health bar for player 2
	public float animationSize;//number of sprite in animation
	[SerializeField]private float lifeLength;//how many seconds does player have before they die
	public bool isPlayerOne;//true if is attached to first player

	void Start() {
		//keep running the function every x secnods depending on how long you want the health to last
		InvokeRepeating("reduceHealth", lifeLength/animationSize, lifeLength/animationSize);
	}

	public void reduceHealth(){
		health -= 100/animationSize;//reduce health based on number of sprites on health bar

		//set ui for player health
		if(!isPlayerOne)
			health2.size = health/100;
		else
			health1.size = health/100;
	}

	//space reduces player health for testing purposes
	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			reduceHealth();
		}

		//if player's health is below zero, stop repeating the command
		if(health<=0)
			CancelInvoke("reduceHealth");
	}
}
