using Pathfinding;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour{

	public Transform target;
	public float speed;
	public float nextWaypointDist;
	Path path;
	int currentWaypoint = 0;
	bool reachedDest = false;
	Seeker seeker;
	Rigidbody2D rb;
	public Transform enemyGFX;
	public bool isInRange = false;
	private bool isDead;
	public bool isAttacking = false;
	public float damage;
	public float attackRate;
	public float maxHealth;
	public float health;
	private bool diedOnce = false;
	[SerializeField]private GameObject HealthBar;
	[SerializeField]GameObject dropItem;
	public float dropRate = 0.5f;
	private bool canMove = true;
	Vector2 direction;
	Vector2 force;

	private void Start(){
		//navigation
        seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();
		InvokeRepeating("UpdatePath", 0f, 0.5f);
		seeker.StartPath(rb.position, target.position, OnPathComplete);
		//end navifation

		//health = maxHealth;
	}
	void Update(){
		
		target = GameObject.FindGameObjectWithTag("Player").transform;
		//navigation
		if(canMove){
			if(path == null)
				return;

			if(currentWaypoint >= path.vectorPath.Count){
				reachedDest = true;
				return;
			}
			else{
				reachedDest = false;
			}
			direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
			force = direction * speed * Time.deltaTime;

			rb.AddForce(force);

			float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
			if(distance < nextWaypointDist){
				currentWaypoint++;
			}
		}
		if(force.x >= 0.1f){
			enemyGFX.GetComponent<SpriteRenderer>().flipX = true;
		}
		else if(force.x >= 0.1f){
			enemyGFX.GetComponent<SpriteRenderer>().flipX = false;
		}
		//end navigation

		if(health <= 0){
			isDead = true;
			if(!diedOnce){
				canMove = false;
				enemyGFX.GetComponent<Animator>().SetTrigger("dead");
				GetComponent<BoxCollider2D>().enabled = false;
				HealthBar.SetActive(false);
				float rand = Random.Range(0, 1);
				if(rand < dropRate){
					GameObject tube = Instantiate(dropItem, transform, false);
					tube.transform.localPosition = new Vector3(0, 0, 0);
					tube.transform.parent = null;
					tube.transform.rotation = Quaternion.identity;
				}
				diedOnce = true;
				Destroy(gameObject, 3);
			}
		}
	}

		//navigation
		void UpdatePath(){
			if(seeker.IsDone())
				seeker.StartPath(rb.position, target.position, OnPathComplete);
		}

		void OnPathComplete(Path p){
			if(!p.error){
				path = p;
				currentWaypoint = 0;
			}
		}
		//end navigation

	public void TakeDamage(float dam){
		StartCoroutine(TakeDamageTimed(dam));
	}

	public IEnumerator TakeDamageTimed(float dam){
		HealthBar.SetActive(true);
		isAttacking = true;
		health -= dam;
		HealthBar.transform.GetChild(0).transform.localScale = new Vector3(health/maxHealth*2, 1, 1);
		canMove = false;
		GetComponent<Rigidbody2D>().AddForce(-direction * speed * Time.deltaTime * 100);
		yield return new WaitForSeconds(.5f);
		canMove = true;
		isAttacking = false;
	}
	private void OnCollisionEnter2D(Collision2D col){
		if(col.collider.tag == "Player" && !isDead){
			print("collided");
			isAttacking = true;
			StartCoroutine(Attack(col.gameObject.GetComponent<HealthController>()));
		}
	}
	private void OnCollisionExit2D(Collision2D col){
		if(col.collider.tag == "Player" && !isDead){
			print("uncollided");
			UnAttack();
		}
	}

	
	IEnumerator Attack(HealthController other){
		print("attacking");
		canMove = false;
		enemyGFX.GetComponent<Animator>().SetBool("attack", true);
		if(other.health > damage)
			other.health -= damage;
		else
			other.health = 0;
		yield return new WaitForSeconds(attackRate);
		print("waited");
		if(isAttacking){
			StartCoroutine(Attack(other));
		}
	}

	void UnAttack(){
		isAttacking = false;
		canMove = true;
		GetComponent<Animator>().SetBool("attack", false);
	}
}
