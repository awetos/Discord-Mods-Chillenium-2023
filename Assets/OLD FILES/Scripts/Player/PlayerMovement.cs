using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    public float speed;//player move speed
	[SerializeField] Vector3 cameraOffset;//camera movement offset from player position
	[SerializeField] float cameraSmoothness;//camera movement smoothness
	public bool canMove;
	[SerializeField] Controller controller;
	

	void FixedUpdate() {
		//print(GetComponent<PlayerInput>().currentControlScheme);
		Vector2 movement;
		movement = controller.control.Movement.ReadValue<Vector2>();
		Vector3 mov = new Vector3(movement.x, 0, movement.y) * speed;//set movement velocity and direction based on input

		GetComponent<Rigidbody>().velocity = mov;
	}
}