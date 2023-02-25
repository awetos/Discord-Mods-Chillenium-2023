using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour{
	public float speed;
	[SerializeField] Transform player1Position;
	[SerializeField] Transform player2Position;

	void Update() {
		if(Camera.main.GetComponent<CameraScript>().isPlayerOne){
			GetComponent<NavMeshAgent>().destination = player1Position.position;
		}
		else{
			GetComponent<NavMeshAgent>().destination = player2Position.position;
		}
	}
}
