using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour{
	[SerializeField] Transform player1Position;
	[SerializeField] Transform player2Position;
	private void Start()
	{
		if(player1Position == null)
		{
            player1Position = GameObject.FindGameObjectsWithTag("Player")[0].transform;
            player2Position = GameObject.FindGameObjectsWithTag("Player")[1].transform;
        }
	}
	void Update() {

		if(Camera.main.GetComponent<CameraScript>().isPlayerOne){
			GetComponent<NavMeshAgent>().destination = player1Position.position;
		}
		else{
			GetComponent<NavMeshAgent>().destination = player2Position.position;
		}
	}
}
