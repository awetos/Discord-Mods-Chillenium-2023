using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour{
	[SerializeField]private GameObject GFX;
	[SerializeField]private GameObject cursor;//aiming cursor
	[SerializeField]private GameObject heartPrefab;//throwable heart prefab
	[SerializeField]private GameObject bulletPrefab;
    public float speed;//player move
	public float damage;
	public float deg;//aim angle
	public float shootDelay;//delay between each heart throw
	public float attackDelay;//delay between each attack
	public bool canMove;//can player move
	public bool controllerMode;//is using controller
	public bool canShoot;//can shoot heart
	public bool canAttack;
	public Playercontrols.PlayerActions controller;//player input
	private Animator animator;//player animator
	private AudioSource ASS;//player sound source
	[SerializeField]private Switcher switcher;
	private GameObject[] enemies;
	bool shake = false;
	Vector3 origPos;
	float time = .3f;

	void Awake(){
		ASS = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
        Playercontrols controls = new Playercontrols();
		controller = controls.Player;
    }

	private void Update() {
	if(gameObject.tag == "Player"){
			//shoot heart
			if(controller.Shoot.triggered){
				if(canShoot){
					StartCoroutine(ShootHeart());
				}
			}
			//attack
			if(controller.Attack.triggered){
				if(canAttack){
					if(switcher.currentPlayer == Switcher.player.player1){
						shake = true;
						origPos = Camera.main.transform.position;
						time = .3f;
					}
					animator.SetBool("attack", true);
					StartCoroutine(Attack());
				}
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Enemy"){
			other.gameObject.GetComponent<EnemyController>().isInRange = true;
		}
	}
	private void OnTriggerExit(Collider other) {
		if(other.gameObject.tag == "Enemy"){
			other.gameObject.GetComponent<EnemyController>().isInRange = false;
		}
	}

	IEnumerator Attack(){
		canAttack = false;
		if(switcher.currentPlayer == Switcher.player.player1){
			enemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach(GameObject enemy in enemies){
				if(enemy.GetComponent<EnemyController>().isInRange)
					StartCoroutine(enemy.GetComponent<EnemyController>().TakeDamage(damage));
			}
			yield return new WaitForSeconds(attackDelay);
			animator.SetBool("attack", false);
			canAttack = true;
		}
		else{
			GameObject bullet = Instantiate(bulletPrefab, transform.transform, false);//spawn bullet
			bullet.transform.SetParent(transform.parent);//move it to root
			bullet.transform.rotation = Quaternion.Euler(90, 0, -deg);//fix rotation of the heart to match where player is aiming
			bullet.GetComponent<BulletController>().SetDirection(-bullet.transform.forward);//set the direction to hearts' forward position to launch it forward
			yield return new WaitForSeconds(attackDelay);
			animator.SetBool("attack", false);
			canAttack = true;
		}
	}
	void FixedUpdate() {
		if(canMove){
			//MOVEMENT
			Vector2 movement = controller.Movement.ReadValue<Vector2>();
			if(movement.x > 0)
				GFX.GetComponent<SpriteRenderer>().flipX = false;
			else if(movement.x < 0)
				GFX.GetComponent<SpriteRenderer>().flipX = true;

			if(movement.y > 0.4f){
				animator.SetBool("facing forward", false);
				animator.SetBool("facing backward",true);
			}
			else if(movement.y < -0.4f){
				animator.SetBool("facing forward", true);
				animator.SetBool("facing backward",false);
				
			}
			if((movement.x > 0 || movement.x < 0) && (movement.y > -0.4f || movement.y < 0.4f)){
				animator.SetBool("facing forward", false);
				animator.SetBool("facing backward",false);
			}
			Vector3 mov = new Vector3(movement.x, 0, movement.y) * speed;//set movement velocity and direction based on input
			GetComponent<Rigidbody>().velocity = mov;

			if(mov.sqrMagnitude > 0)
				animator.SetBool("running", true);
			else
				animator.SetBool("running", false);

			if(!shake)
				Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, transform.position+new Vector3(0,5,0), 0.2f);
			else{
				if(time > 0){
					Camera.main.transform.localPosition = origPos+Random.insideUnitSphere*0.2f;
					time -= Time.deltaTime;
				}
				else{
					time = 0; 
					shake = false;
				}
			}
			
		}


			//AIMING
			Vector3 direction = Vector3.zero;
			if(controller.controllerused.triggered)
				controllerMode = true;
			if(controller.mouseused.triggered)
				controllerMode=false;


			if(controllerMode){
				Vector2 aim = controller.Aim.ReadValue<Vector2>();
				direction = new Vector3(aim.x, 0, aim.y);
				if(canMove){
					cursor.transform.SetParent(transform);
					cursor.transform.position = transform.position+direction;
				}
			}
			else{
				Vector3 mousePos = Input.mousePosition;
				Ray ray = Camera.main.ScreenPointToRay(mousePos);
				Vector3 worldPos = Vector3.zero;
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit)){
					worldPos = hit.point;
				}
				if(canMove){
					direction = worldPos - transform.position;
					cursor.transform.SetParent(null);
					cursor.transform.position = worldPos;
				}
			}
			if(canMove){
				deg = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
				cursor.transform.rotation = Quaternion.Euler(90, 0, -deg);
		}
	}

	//shoot heart with delay
	IEnumerator ShootHeart(){
		//ASS.clip = shootSound;
		//ASS.Play();
		GameObject heart = Instantiate(heartPrefab, transform.transform, false);//spawn bullet
		canShoot = false;
		heart.transform.SetParent(transform.parent);//move it to root
		heart.transform.rotation = Quaternion.Euler(90, 0, -deg);//fix rotation of the heart to match where player is aiming
		heart.GetComponent<HeartThrowController>().SetDirection(-heart.transform.forward);//set the direction to hearts' forward position to launch it forward
		//animator.SetBool("shoot", false);
		yield return new WaitForSeconds(shootDelay);
		canShoot=true;
    }

	//to enable controls
	private void OnEnable() {
		controller.Enable();
	}

	private void OnDisable() {
		controller.Disable();
	}
}
