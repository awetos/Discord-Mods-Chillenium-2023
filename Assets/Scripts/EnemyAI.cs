using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour{
	bool songlooped = false;
	[SerializeField] Transform player1Position;
	[SerializeField] Transform player2Position;

	void Update() {

		//StartCoroutine(MusicCounter());

		if(Camera.main.GetComponent<CameraScript>().isPlayerOne){
			GetComponent<NavMeshAgent>().destination = player1Position.position;
		}
		else{
			GetComponent<NavMeshAgent>().destination = player2Position.position;
		}
	}
	IEnumerator MusicCounter(){
		yield return new WaitForSeconds(31);
		if(!songlooped)
			GetComponent<NavMeshAgent>().speed = 8f;
		yield return new WaitForSeconds(62);
		GetComponent<NavMeshAgent>().speed = 3.5f;
		songlooped = true;
	}
}
