using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour{

	[SerializeField] Transform playerOne;
	[SerializeField] Transform playerTwo;
	[SerializeField] float FOV;
    void Update(){
        Bounds bounds = new Bounds(playerOne.position, Vector3.zero);
		bounds.Encapsulate(playerTwo.position);

		float screenAspect = (float)Screen.width / (float)Screen.height;
		float targetAspect = bounds.size.x / bounds.size.z;
		float calculatedOrthographicSize;

		if(screenAspect < targetAspect ){
			calculatedOrthographicSize = bounds.size.z / FOV;
		}
		else{
			float difference = targetAspect / screenAspect;
			calculatedOrthographicSize = bounds.size.z /FOV * difference;
		}
		GetComponent<Camera>().orthographicSize = Mathf.Max(calculatedOrthographicSize, 1.7f);

		Vector3 camPos = bounds.center;
		camPos.y = transform.position.y;
		transform.position = camPos;
    }
}
