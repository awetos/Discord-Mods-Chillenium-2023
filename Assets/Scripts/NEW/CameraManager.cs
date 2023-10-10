using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour{

	[SerializeField] Transform playerOne;
	[SerializeField] Transform playerTwo;
	[SerializeField] float FOV;
	[SerializeField] float FOVDivider;
    void Update(){
		float dis = Vector3.Distance(playerOne.position, playerTwo.position)/FOVDivider;
		if(dis < FOV){
			Camera.main.orthographicSize = FOV;
		}
		else{
			Camera.main.orthographicSize = dis;
		}
    }
}
