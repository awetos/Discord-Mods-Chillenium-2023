using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    public float speed;//player move speed
	[SerializeField] Vector3 cameraOffset;//camera movement offset from player position
	[SerializeField] float cameraSmoothness;//camera movement smoothness
	public bool canMove;
	
	void FixedUpdate() {


		float hor = Input.GetAxisRaw("Horizontal");//get left right movement input
		float ver = Input.GetAxisRaw("Vertical");//get up down movement input

		Vector3 mov = new Vector3(hor, 0, ver).normalized * speed;//set movement velocity and direction based on input
		
		GetComponent<Rigidbody>().velocity = mov;
	}
}