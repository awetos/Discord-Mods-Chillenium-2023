using Cinemachine;
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

    public float distance;
    [SerializeField] CinemachineTargetGroup ctg;
    private void Update()
    {
        CalculateDistance();
    }
    //at above this distance, start looking at both dinosaurs
    [SerializeField] float DISTANCE_CONSTANT = 5;


    void CalculateDistance()
    {
        distance = Vector3.Distance(player1.transform.position, player2.transform.position);

        /*
        if(distance / 2f > 2)
        {
            Camera.main.orthographicSize = distance / 2f;
        }
        else
        {
            Camera.main.orthographicSize = 2f;
        }
        */
              if(distance > DISTANCE_CONSTANT) //if oyu are getting too far, start prioritizing seeing the other one!
        {
            if (isPlayerOne)
            {
               
                ctg.m_Targets[1].weight = distance;
                Camera.main.orthographicSize = 3f;
            }
            else
            {
               
                ctg.m_Targets[0].weight = distance;
                Camera.main.orthographicSize = 3f;
            }
        }
        else
        {
            if (isPlayerOne)
            {
                ctg.m_Targets[0].weight = 5;
                ctg.m_Targets[1].weight = 0;
            }
            else
            {
                ctg.m_Targets[1].weight = 5;
                ctg.m_Targets[0].weight = 0;
            }
        }
      
      
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
    [SerializeField] Vector3 cameraOffset;//camera movement offset from player position
    [SerializeField] float cameraSmoothness;//camera movement smoothness
    void FixedUpdate()
    {
        Vector3 position;
        if(isPlayerOne)
        {
             position = player1.GetPosition();
        }
        else
        {
            position = player2.GetPosition();
        }
        //try cinemachine.
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, position, cameraSmoothness) + cameraOffset;//smoothly follow player

       
    }
}
