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

	void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // The other collider belongs to a player
            if (other.gameObject.GetComponent<PlayerMovement>().enabled == false)
            {
                Camera.main.GetComponent<CameraScript>().switchPlayer();
                Destroy(this.gameObject);
            }
            return;
        }
        else if (other.CompareTag("Enemy"))
        {

            Camera.main.GetComponent<HealthReferences>().TakeDamage(30);
            Destroy(this.gameObject);
        }
        else
        {
            if (other.CompareTag("NavMeshObstacle"))
            {
               
                //hit a navmesh obstacle.
                Debug.Log("encountered obstacle.");
                directionToTravel = directionToTravel * -1;
            }
        }
    }
}
