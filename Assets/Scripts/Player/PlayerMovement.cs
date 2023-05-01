using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour{
    public float speed;//player move speed
	[SerializeField] Vector3 cameraOffset;//camera movement offset from player position
	[SerializeField] float cameraSmoothness;//camera movement smoothness
	public bool canMove;

	Playercontrols controls;
	
	void Awake(){
		controls = new Playercontrols();
		GetComponent<PlayerInput>().SwitchCurrentControlScheme("controllers");
	}

	void FixedUpdate() {
		//print(GetComponent<PlayerInput>().currentControlScheme);
		Vector2 movement;
		movement = controls.Player.Movement.ReadValue<Vector2>();
		Vector3 mov = new Vector3(movement.x, 0, movement.y) * speed;//set movement velocity and direction based on input

		GetComponent<Rigidbody>().velocity = mov;
	}
	private void OnEnable() {
		controls.Player.Enable();
	}

	private void OnDisable() {
		controls.Player.Disable();
	}
}