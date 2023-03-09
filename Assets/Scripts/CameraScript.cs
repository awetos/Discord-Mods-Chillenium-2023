using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour{

	public delegate void SwitchPlayer(int playerID);
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

	private void Start()
	{
		//at the start of the game, player two should be dying because he is not active.
       

    }
    public void switchPlayer() {
		if(isPlayerOne){
			//switch from player one
			//activate player two
			//player one starts health decay.
			playerOne.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;//player can't move
			playerOne.GetComponent<PlayerMovement>().enabled = false;//disable character controller
			playerOne.GetComponent<HealthManager>().startAnim();
			playerOneBullet.SetActive(false);
			playerOneArrow.SetActive(false);

			playerTwo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
			playerTwo.GetComponent<HealthManager>().cancelAnim();
			playerTwo.GetComponent<PlayerMovement>().enabled = true;
			playerTwoFist.SetActive(true);
			playerTwoArrow.SetActive(true);

            isPlayerOne = false;
		}
		else{
			//switch from player two to player one
			//player two begins decay
			//player one stops decay.
			playerOne.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
			playerOne.GetComponent<PlayerMovement>().enabled = true;
			playerOne.GetComponent<HealthManager>().cancelAnim();
			playerOneBullet.SetActive(true);
			playerOneArrow.SetActive(true);

			playerTwo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			playerTwo.GetComponent<HealthManager>().startAnim();
			playerTwo.GetComponent<PlayerMovement>().enabled = false;
			playerTwoFist.SetActive (false);
			playerTwoArrow.SetActive(false);

            isPlayerOne = true;
		}

		if (isPlayerOne)
		{
            OnPlayerSwitched(0);

        }
        else
		{
            OnPlayerSwitched(1);

        }

	}
}
