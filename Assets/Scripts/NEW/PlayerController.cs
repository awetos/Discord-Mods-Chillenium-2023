using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour{
	[SerializeField]private GameObject GFX;
    public float speed;//player move speed
	public bool canMove;
	public Playercontrols.PlayerActions controller;
	[SerializeField]private GameObject cursor;
	public float deg;
	public bool controllerMode;
	private Animator animator;
	private AudioSource ASS;
	[SerializeField]private GameObject HeartPrefab;
	public bool canShoot;
	public float shootDelay;
	public float attackDelay;

	void Awake(){
		ASS = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
        Playercontrols controls = new Playercontrols();
		controller = controls.Player;
    }

	private void Update() {
		if(controller.Shoot.triggered){
			if(canShoot){
				StartCoroutine(ShootHeart());
			}
		}
	}

	void FixedUpdate() {
		Vector2 movement = controller.Movement.ReadValue<Vector2>();
		Vector3 mov = new Vector3(movement.x, 0, movement.y) * speed;//set movement velocity and direction based on input

		GetComponent<Rigidbody>().velocity = mov;
		Vector3 direction = Vector3.zero;


		if(controller.controllerused.triggered)
			controllerMode = true;
		if(controller.mouseused.triggered)
			controllerMode=false;


		if(controllerMode){
			print("TEST222222222");
			Vector2 aim = controller.Aim.ReadValue<Vector2>();
			direction = new Vector3(aim.x, 0, aim.y);
			cursor.transform.SetParent(transform);
			cursor.transform.position = transform.position+direction;
		}
		else{
			print("TEST");
			Vector3 mousePos = Input.mousePosition;
			Ray ray = Camera.main.ScreenPointToRay(mousePos);
			Vector3 worldPos = Vector3.zero;
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				worldPos = hit.point;
			}
			direction = worldPos - transform.position;
			cursor.transform.SetParent(null);
			cursor.transform.position = worldPos;
		}
		deg = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
		cursor.transform.rotation = Quaternion.Euler(90, 0, -deg);
	}

	IEnumerator ShootHeart(){
		//animator.SetBool("shoot", true);
		//ASS.clip = shootSound;
		//ASS.Play();
		GameObject heart = Instantiate(HeartPrefab, transform.transform, false);//spawn bullet
		canShoot = false;
		heart.transform.SetParent(transform.parent);//move it to root
		heart.transform.rotation = Quaternion.Euler(90, 0, -deg);//fix rotation of the heart to match where player is aiming
		heart.GetComponent<HeartThrowController>().SetDirection(-heart.transform.forward);//set the direction to hearts' forward position to launch it forward
		//animator.SetBool("shoot", false);
		yield return new WaitForSeconds(shootDelay);
		canShoot=true;
    }

	private void OnEnable() {
		controller.Enable();
	}

	private void OnDisable() {
		controller.Disable();
	}
}
