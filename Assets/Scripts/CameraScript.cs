using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour{
	public bool isPlayerOne;
	public GameObject playerOne;
	public GameObject playerTwo; //set to public so scripts can read location
	[SerializeField] private GameObject playerOneGFX;
	[SerializeField] private GameObject playerTwoGFX;
	[SerializeField] private GameObject playerOneArrow;
	[SerializeField] private GameObject playerTwoArrow;

	public void switchPlayer() {
		if(isPlayerOne){ //if you are player 1, switch and enable player 2
			playerOne.GetComponent<PlayerMovement>().enabled = false;
			playerTwo.GetComponent<HealthManager>().enabled = false;

			playerTwo.GetComponent<HealthManager>().startAnim();
			playerOne.GetComponent<HealthManager>().cancelAnim();

			playerOneArrow.SetActive(false);
			playerTwo.GetComponent<PlayerMovement>().enabled = true;
			playerTwo.GetComponent <HealthManager>().enabled = true;
			playerTwoArrow.SetActive(true);
			playerTwo.GetComponent<BoxCollider>().enabled = true;
			playerOne.GetComponent<BoxCollider>().enabled = false;

			//combat
			playerTwo.GetComponentInChildren<FistAttack>().enabled = true;
            playerOne.GetComponentInChildren<BulletSpawner>().enabled = false;


            isPlayerOne = false;
		}
		else{
			playerOne.GetComponent<PlayerMovement>().enabled = true;
			playerTwo.GetComponent<HealthManager>().enabled = true;

			playerOne.GetComponent<HealthManager>().startAnim();
			playerTwo.GetComponent<HealthManager>().cancelAnim();

			playerOneArrow.SetActive(true);
			playerTwo.GetComponent<PlayerMovement>().enabled = false;
			playerTwo.GetComponent <HealthManager>().enabled = false;
			playerTwoArrow.SetActive(false);
			playerTwo.GetComponent<BoxCollider>().enabled = true;
			playerOne.GetComponent<BoxCollider>().enabled = false;
            //combat
            playerTwo.GetComponentInChildren<FistAttack>().enabled = false;
			playerOne.GetComponentInChildren<BulletSpawner>().enabled = true;
            isPlayerOne = true;
		}
	}
}
