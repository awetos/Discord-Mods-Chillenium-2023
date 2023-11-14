using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartThrowController : MonoBehaviour{

	private void Start(){
		Destroy(gameObject,2);
	}

	void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "NonPlayer"){
			Camera.main.GetComponent<Switcher>().SwitchPlayer();
			Destroy(gameObject);
		}
	}
}
