using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour{
	public float speed;
	[SerializeField] Transform playerPosition;

	void Update() {
		GetComponent<NavMeshAgent>().destination = playerPosition.position;
		//GetComponent<NavMeshAgent>().Move(GetComponent<NavMeshAgent>().destination);
	}
}
