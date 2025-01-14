using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour{
	[SerializeField]private GameObject pauseMenu;
	[SerializeField]private GameObject GFX;
	[SerializeField]private GameObject cursor;//aiming cursor
	[SerializeField]private GameObject heartPrefab;//throwable heart prefab
	[SerializeField]private GameObject bulletPrefab;
    public float speed;//player move
	public float heartSpeed;
	public float bulletSpeed;
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
	[SerializeField]private Switcher switcher;
	private GameObject[] enemies;
	bool shake = false;
	Vector3 origPos;
	float time = .3f;
	[SerializeField]private AudioSource[] ASS;

	void Awake(){
		Time.timeScale = 1;
		animator = GetComponent<Animator>();
        Playercontrols controls = new Playercontrols();
		controller = controls.Player;
		Cursor.visible = false;
    }

	private void Update() {
	if(gameObject.tag == "Player"){

			//pause
			if(controller.Back.triggered){
				if(pauseMenu.activeInHierarchy){
					UnpauseFunc();
				}
			}
			if(controller.Start.triggered){
				if(pauseMenu.activeInHierarchy){
					UnpauseFunc();
				}
				else{
					PauseFunc();
				}
			}
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

	void PauseFunc(){
		Cursor.visible = true;
		canMove = false;
		canShoot = false;
		canAttack = false;
		Time.timeScale = 0;
		Camera.main.GetComponent<AudioSource>().Pause();
		pauseMenu.SetActive(true);
		pauseMenu.GetComponent<MenuButtonManager>().SetControllerMode(controllerMode);
	}
	void UnpauseFunc(){
		Time.timeScale = 1;
		Cursor.visible = false;
		canMove = true;
		canShoot = true;
		canAttack = true;
		Camera.main.GetComponent<AudioSource>().Play();
		pauseMenu.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Enemy"){
			other.gameObject.GetComponent<EnemyController>().isInRange = true;
		}
	}
	private void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.tag == "Enemy"){
			other.gameObject.GetComponent<EnemyController>().isInRange = false;
		}
	}
	private void OnCollisionEnter2D(Collision2D collision) {
		if(collision.collider.tag == "pickup"){
			ASS[0].Play();
		}
	}

	void FixedUpdate(){
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
			GetComponent<Rigidbody2D>().velocity = movement * speed;

			if(movement.sqrMagnitude > 0)
				animator.SetBool("running", true);
			else
				animator.SetBool("running", false);

			if(!shake)
				Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, transform.position+new Vector3(0,0,-7), 0.2f);
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
			direction = new Vector2(aim.x, aim.y);
			if(canMove){
				cursor.transform.SetParent(transform);
				cursor.transform.position = transform.position+direction;
			}
		}
		else{
			Vector3 mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint(mousePos);
			direction = new Vector2(
				mousePos.x - transform.position.x,
				mousePos.y - transform.position.y
			);
			cursor.transform.SetParent(null);
			cursor.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
		}
		deg = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		if(canMove)
			cursor.transform.rotation = Quaternion.AngleAxis(deg-90, Vector3.forward);
	}
	
	IEnumerator Attack(){
		canAttack = false;
		if(switcher.currentPlayer == Switcher.player.player1){
			enemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach(GameObject enemy in enemies){
				if(enemy.GetComponent<EnemyController>().isInRange)
					enemy.GetComponent<EnemyController>().TakeDamage(damage);
			}
			yield return new WaitForSeconds(attackDelay);
			animator.SetBool("attack", false);
			canAttack = true;
		}
		else{
			GameObject bullet = Instantiate(bulletPrefab, transform.transform, false);//spawn bullet
			bullet.transform.SetParent(transform.parent);//move it to root
			bullet.transform.rotation = Quaternion.AngleAxis(deg-90, Vector3.forward);//fix rotation of the heart to match where player is aiming
			bullet.GetComponent<Rigidbody2D>().velocity = cursor.transform.up*bulletSpeed;
			yield return new WaitForSeconds(attackDelay);
			animator.SetBool("attack", false);
			canAttack = true;
		}
	}

	//shoot heart with delay
	IEnumerator ShootHeart(){
		//ASS.clip = shootSound;
		//ASS.Play();
		GameObject heart = Instantiate(heartPrefab, transform.position, transform.rotation);//spawn bullet
		canShoot = false;
		heart.transform.SetParent(null);//move it to root
		heart.transform.rotation = Quaternion.AngleAxis(deg-90, Vector3.forward);
		heart.GetComponent<Rigidbody2D>().velocity = cursor.transform.up*heartSpeed;
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
