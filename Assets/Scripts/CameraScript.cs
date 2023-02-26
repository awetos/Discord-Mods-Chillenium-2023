using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour{

	public delegate void SwitchPlayer(bool isPlayerOne);
	public static event SwitchPlayer OnPlayerSwitched;


	public bool isPlayerOne;
	public GameObject playerOne;
	public GameObject playerTwo; //set to public so scripts can read location
	[SerializeField] private GameObject playerOneGFX;
	[SerializeField] private GameObject playerTwoGFX;
	[SerializeField] private GameObject playerOneArrow;
	[SerializeField] private GameObject playerTwoArrow;
	[SerializeField] private GameObject playerOneBullet;
	[SerializeField] private GameObject playerTwoFist;

	public void switchPlayer() {
		if(isPlayerOne){
			//switch from player one
			playerOne.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;//player can't move
			playerOne.GetComponent<PlayerMovement>().enabled = false;//disable character controller
			playerOne.GetComponent<HealthManager>().cancelAnim();//disable health going down?
			playerOneBullet.SetActive(false);
			playerOneArrow.SetActive(false);

			playerTwo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
			playerTwo.GetComponent<HealthManager>().enabled = false;
			playerTwo.GetComponent<HealthManager>().startAnim();
			playerTwo.GetComponent<PlayerMovement>().speed = 5;
			playerTwo.GetComponent<PlayerMovement>().enabled = true;
			playerTwo.GetComponent <HealthManager>().enabled = true;
			playerTwoFist.SetActive(true);
			playerTwoArrow.SetActive(true);

            isPlayerOne = false;
		}
		else{
			//switch from player two
			playerOne.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
			playerOne.GetComponent<PlayerMovement>().enabled = true;
			playerOne.GetComponent<HealthManager>().startAnim();
			playerOneBullet.SetActive(true);
			playerOneArrow.SetActive(true);

			playerTwo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			playerTwo.GetComponent<HealthManager>().enabled = true;
			playerTwo.GetComponent<HealthManager>().cancelAnim();
			playerTwo.GetComponent<PlayerMovement>().speed = 0;
			playerTwo.GetComponent<PlayerMovement>().enabled = false;
			playerTwo.GetComponent <HealthManager>().enabled = false;
			playerTwoFist.SetActive (false);
			playerTwoArrow.SetActive(false);

            isPlayerOne = true;
		}

		OnPlayerSwitched(isPlayerOne);
	}
}
