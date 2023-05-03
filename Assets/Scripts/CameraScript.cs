using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour{

    //is received by the heart animator.
	public delegate void SwitchPlayer(int playerID);
	public static event SwitchPlayer OnPlayerSwitched;
	public bool isPlayerOne;

	[SerializeField] Controller controller;

    [SerializeField] private PlayerProfile player1;
    [SerializeField] private PlayerProfile player2;


    public float distance;
    [SerializeField] CinemachineTargetGroup ctg;
    [SerializeField] float MAX_WEIGHT = 50f;


	private void Awake() {
		controller.DSGamepad.SetLightBarColor(controller.player1Color);
	}
	private void Update()
    {
        CalculateDistance();
    }
    //at above this distance, start looking at both dinosaurs
    [SerializeField] float DISTANCE_CONSTANT = 5;

	void CalculateDistance()
    {
        distance = Vector3.Distance(player1.transform.position, player2.transform.position);

        if(distance > DISTANCE_CONSTANT) //if oyu are getting too far, start prioritizing seeing the other one!
        {
            StopCoroutine("SlowlyZoomIn");
            StartCoroutine("SlowlyPanCamera");
        }
        else
        {
            //Debug.Log("considering zoom in");
            StopCoroutine("SlowlyPanCamera");
            if(PlayerSwitchInProgress == false)
            {
                //Debug.Log(" zoom in initiated.");
                StartCoroutine("SlowlyZoomIn");

            }
           
        }
       
      
    }
    //add weight to the active player target.
    IEnumerator SlowlyZoomIn()
    {
        while(distance < DISTANCE_CONSTANT && PlayerSwitchInProgress == false)
        {


            if (isPlayerOne)
            {
                ctg.m_Targets[1].weight -= 0.1f;
                if (ctg.m_Targets[1].weight < 1)
                {
                    ctg.m_Targets[1].weight = 1f;
                }


                ctg.m_Targets[0].weight += 0.1f;
                if(ctg.m_Targets[0].weight > MAX_WEIGHT)
                {
                    ctg.m_Targets[0].weight = MAX_WEIGHT;
                }
            }
            else
            {
                ctg.m_Targets[0].weight -= 0.1f;
                
                if(ctg.m_Targets[0].weight < 1)
                {
                    ctg.m_Targets[0].weight = 1f;
                }

                ctg.m_Targets[0].weight -= 0.1f;


                if (ctg.m_Targets[1].weight > MAX_WEIGHT)
                {
                    ctg.m_Targets[1].weight = MAX_WEIGHT;
                }
            }


            Camera.main.orthographicSize = DISTANCE_MULTIPLIER_CAMERA * distance;
            if (Camera.main.orthographicSize < 2)
            {
                Camera.main.orthographicSize = 2;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    [SerializeField]bool PlayerSwitchInProgress;
    [SerializeField] float DISTANCE_MULTIPLIER_CAMERA = 0.7f;
    //add weight to the further target.
    IEnumerator SlowlyPanCamera()
    {
        while (distance > DISTANCE_CONSTANT)
        {
            //add 0.1 to the weight
            if (isPlayerOne)
            {
                ctg.m_Targets[1].weight += 0.1f; //slowly add to player 2
                if (ctg.m_Targets[1].weight > MAX_WEIGHT)
                {
                    ctg.m_Targets[1].weight = MAX_WEIGHT;
                }
               

            }
            else
            {
                ctg.m_Targets[0].weight += 0.1f; //slowly add to player 1
                if(ctg.m_Targets[0].weight > MAX_WEIGHT)
                {
                    ctg.m_Targets[0].weight = MAX_WEIGHT;
                }


            }
          

            Camera.main.orthographicSize = DISTANCE_MULTIPLIER_CAMERA * distance;
            if(Camera.main.orthographicSize > 4)
            {
                Camera.main.orthographicSize = 4;
            }
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }
    public void switchPlayer() {
		Gamepad.current.SetMotorSpeeds(0, 0);
		if (isPlayerOne)
		{
            //switch to player 2
			controller.DSGamepad.SetLightBarColor(controller.player2Color);
            player1.SetPlayerInactive();
            player2.SetPlayerActive();

            isPlayerOne = false;

            OnPlayerSwitched(1);

        }
        else
		{
			controller.DSGamepad.SetLightBarColor(controller.player1Color);
			player1.SetPlayerActive();
			player2.SetPlayerInactive();

			isPlayerOne = true;

            OnPlayerSwitched(0);

        }

        StopAllCoroutines();
        PlayerSwitchInProgress = true;
        StartCoroutine("QuicklyZoomIn");



    }
    [SerializeField] Vector3 cameraOffset;//camera movement offset from player position
    [SerializeField] float cameraSmoothness;//camera movement smoothness
   

    IEnumerator QuicklyZoomIn()
    {
        //make the weight of the active player five times that of the other.
        for (int i = 0; i < 20; i++)
        {
            if (isPlayerOne)
            {
                ctg.m_Targets[0].weight += 1f;
                ctg.m_Targets[1].weight = 1f;

            }
            else
            {

                ctg.m_Targets[0].weight = 1f;
                ctg.m_Targets[1].weight += 1f; ;

            }
            yield return new WaitForEndOfFrame();
            //Debug.Log("i"+i);
        }
      
        PlayerSwitchInProgress = false;
        yield return new WaitForEndOfFrame();


    }
}
