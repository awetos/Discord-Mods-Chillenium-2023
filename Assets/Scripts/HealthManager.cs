using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour{
    public float health = 100;//player current health percent
	public float animationSize;//number of sprite in animation
	[SerializeField]private Sprite[] animationSprites;
	[SerializeField]private Image healthImg;
	[SerializeField]private float lifeLength;//how many seconds does player have before they die
	public bool isPlayerOne;//true if is attached to first player
	int i=0;

	void Start() {
		//keep running the function every x secnods depending on how long you want the health to last
		InvokeRepeating("reduceHealth", lifeLength/animationSize, lifeLength/animationSize);
	}

	public void reduceHealth(){
		health -= 100/animationSize;//reduce health based on number of sprites on health bar
		print((int)(lifeLength/animationSize));
		//set ui for player health
		healthImg.sprite = animationSprites[i];
		i++;
		
	}

	//space reduces player health for testing purposes
	void Update() {
		//if player's health is below zero, stop repeating the command
		if(health<=0){
			CancelInvoke("reduceHealth");
			//DEATH
		}
	}
}
