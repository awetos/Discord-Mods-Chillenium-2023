using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour{

	public int healthToAdd;
	public float secToDisapp;

	private void Start() {
		Destroy(gameObject, secToDisapp);
	}

	private void OnCollisionEnter(Collision other){
        if (other.collider.tag=="Player"){
            if(other.collider.GetComponent<HealthController>().health < other.collider.GetComponent<HealthController>().maxHealth-healthToAdd)
				other.collider.GetComponent<HealthController>().health += healthToAdd;
			else
				other.collider.GetComponent<HealthController>().health = other.collider.GetComponent<HealthController>().maxHealth;
            Destroy(gameObject);
        }
    }
}
