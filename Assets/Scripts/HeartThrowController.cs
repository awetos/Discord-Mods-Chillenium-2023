using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartThrowController : MonoBehaviour{
	private Vector3 directionToTravel;
    public float speed;

	private void Start(){
		Destroy(gameObject,2);
	}
	public void SetDirection(Vector3 direction){
        directionToTravel = direction; //it's going to be too faster even if the magnitude is 1?
    }
	private void FixedUpdate(){
		transform.Translate(directionToTravel*speed*Time.deltaTime);//move heart to forward
    }

	void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "NonPlayer"){
			Camera.main.GetComponent<Switcher>().SwitchPlayer();
			Destroy(gameObject);
		}
	}
}
