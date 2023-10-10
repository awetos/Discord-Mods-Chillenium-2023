using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour{

	public int healthToAdd;
	public float secToDisapp;

	private void Start() {
		Destroy(gameObject, secToDisapp);
	}

	private void OnTriggerEnter(Collider other){
        if (other.tag=="Player"){
            if(other.GetComponent<HealthController>().health < other.GetComponent<HealthController>().maxHealth-healthToAdd)
				other.GetComponent<HealthController>().health += healthToAdd;
			else
				other.GetComponent<HealthController>().health = other.GetComponent<HealthController>().maxHealth;
            Destroy(gameObject);
        }
    }
}
