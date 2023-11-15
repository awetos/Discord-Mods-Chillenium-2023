using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BulletController : MonoBehaviour{
	public float damage;

	private void Start(){
		Destroy(gameObject,2);
	}

	void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Enemy" && other.GetComponent<NavMeshAgent>().enabled){
			print("took an arrow to the knee");
			other.GetComponent<EnemyController>().TakeDamage(damage);
			Destroy(gameObject);
		}
	}
}
