using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour{
	public bool isPlayerOne;
	[SerializeField] private GameObject playerOne;
	[SerializeField] private GameObject playerTwo;
	[SerializeField] private GameObject playerOneGFX;
	[SerializeField] private GameObject playerTwoGFX;
	[SerializeField] private GameObject playerOneArrow;
	[SerializeField] private GameObject playerTwoArrow;

	public void switchPlayer() {
		if(isPlayerOne){
			playerOne.GetComponent<PlayerMovement>().enabled = false;
			playerTwo.GetComponent<HealthManager>().enabled = false;

			playerTwo.GetComponent<HealthManager>().startAnim();
			playerOne.GetComponent<HealthManager>().cancelAnim();

			playerOneArrow.SetActive(false);
			playerTwo.GetComponent<PlayerMovement>().enabled = true;
			playerTwo.GetComponent <HealthManager>().enabled = true;
			playerTwoArrow.SetActive(true);
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
			isPlayerOne = true;
		}
	}
}
