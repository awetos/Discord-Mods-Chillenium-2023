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

    /*[SerializeField]Transform player1Position;
	[SerializeField]Transform player2Position;
	[SerializeField]private Switcher switcher;
	[SerializeField]GameObject dropItem;
	public float dropRate = 0.5f;


	public float maxHealth;
	public float health;
	private bool diedOnce = false;
	Vector3 direction;
	[SerializeField]private GameObject HealthBar;*/
	private void Start(){
		//navigation
        seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();
		InvokeRepeating("UpdatePath", 0f, 0.5f);
		//end navifation

		//health = maxHealth;
	}
	void Update(){
		//navigation
		target = GameObject.FindGameObjectWithTag("Player").transform;
		seeker.StartPath(rb.position, target.position, OnPathComplete);
        if(path == null)
			return;

		if(currentWaypoint >= path.vectorPath.Count){
			reachedDest = true;
			return;
		}
		else{
			reachedDest = false;
		}
		Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
		Vector2 force = direction * speed * Time.deltaTime;

		rb.AddForce(force);

		float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
		if(distance < nextWaypointDist){
			currentWaypoint++;
		}
		if(force.x >= 0.1f){
			enemyGFX.GetComponent<SpriteRenderer>().flipX = true;
		}
		else if(force.x >= 0.1f){
			enemyGFX.GetComponent<SpriteRenderer>().flipX = false;
		}
		//end navigation
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

		/*if(GetComponent<NavMeshAgent>().enabled){
			if(switcher.currentPlayer == Switcher.player.player1){
				direction = player1Position.position - transform.position;
				GetComponent<NavMeshAgent>().destination = player1Position.position;
			}
			else{
				direction = player2Position.position - transform.position;
				GetComponent<NavMeshAgent>().destination = player2Position.position;
			}
		}
		transform.rotation = Quaternion.Euler(90, 0, 0);
		transform.GetChild(0).transform.rotation = Quaternion.Euler(90, 0, Mathf.Atan2(direction.x, direction.z) * -Mathf.Rad2Deg);
		if(health <= 0){
			isDead = true;
			if(!diedOnce){
				GetComponent<NavMeshAgent>().enabled = false;
				GetComponent<Animator>().SetTrigger("dead");
				GetComponent<BoxCollider>().enabled = false;
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
		}*/

	public void TakeDamage(float dam){
		//StartCoroutine(TakeDamageTimed(dam));
	}

	/*public IEnumerator TakeDamageTimed(float dam){
		HealthBar.SetActive(true);
		isAttacking = true;
		health -= dam;
		HealthBar.transform.GetChild(0).transform.localScale = new Vector3(health/maxHealth*2, 1, 1);
		GetComponent<NavMeshAgent>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = false;
		GetComponent<Rigidbody>().AddForce(-transform.GetChild(0).up*1.5f, ForceMode.Impulse);
		yield return new WaitForSeconds(.5f);
		GetComponent<NavMeshAgent>().enabled = true;
		GetComponent<Rigidbody>().isKinematic = true;
		isAttacking = false;
	}
	private void OnCollisionEnter(Collision col){
		if(col.collider.tag == "Player" && !isDead){
			print("collided");
			isAttacking = true;
			StartCoroutine(Attack(col.gameObject.GetComponent<HealthController>()));
		}
	}
	private void OnCollisionExit(Collision col){
		if(col.collider.tag == "Player" && !isDead){
			print("uncollided");
			UnAttack();
		}
	}

	*/
	IEnumerator Attack(HealthController other){
		print("attacking");
		GetComponent<NavMeshAgent>().enabled = false;
		GetComponent<Animator>().SetBool("attack", true);
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
		GetComponent<NavMeshAgent>().enabled = true;
		GetComponent<Animator>().SetBool("attack", false);
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.collider.tag == "Player" && !isDead){
			print("collided");
			isAttacking = true;
			StartCoroutine(Attack(col.gameObject.GetComponent<HealthController>()));
		}
	}
	void OnCollisionExit2D(Collision2D col){
		if(col.collider.tag == "Player" && !isDead){
			print("uncollided");
			UnAttack();
		}
	}
}
