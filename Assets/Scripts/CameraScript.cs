using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour{

    //is received by the heart animator.
	public delegate void SwitchPlayer(int playerID);
	public static event SwitchPlayer OnPlayerSwitched;


	public bool isPlayerOne;



    [SerializeField] private PlayerProfile player1;
    [SerializeField] private PlayerProfile player2;

	private void Start()
	{
        //at the start of the game, player two should be dying because he is not active.
        

    }
    public void switchPlayer() {
		
		if (isPlayerOne)
		{
            //switch to player 2
            player1.SetPlayerInactive();
            player2.SetPlayerActive();

            isPlayerOne = false;

            OnPlayerSwitched(1);

        }
        else
		{
			player1.SetPlayerActive();
			player2.SetPlayerInactive();

			isPlayerOne = true;

            OnPlayerSwitched(0);

        }


	}
}
