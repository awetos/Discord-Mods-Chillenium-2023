using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour{
    [SerializeField]Transform player1Position;
	[SerializeField]Transform player2Position;
	[SerializeField]private Switcher switcher;

	public bool isInRange = false;


	public float maxHealth;
	public float health;
	public bool isAttacking = false;
	public float damage;
	public float attackRate;
	private bool isDead;
	private void Start(){
		health = maxHealth;
	}
	void Update(){
		if(!isAttacking || !isDead){
			if(switcher.currentPlayer == Switcher.player.player1){
				GetComponent<NavMeshAgent>().destination = player1Position.position;
			}
			else{
				GetComponent<NavMeshAgent>().destination = player2Position.position;
			}
		}
		transform.rotation = Quaternion.identity;
	}

	public void TakeDamage(float dam){
		health -= dam;
		if(health <= 0){
		isDead = true;
			GetComponent<NavMeshAgent>().destination = transform.position;
			GetComponent<Animator>().SetTrigger("dead");
			GetComponent<NavMeshAgent>().acceleration = 0;
			GetComponent<NavMeshAgent>().speed = 0;
			Destroy(gameObject, 3);
		}
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

	IEnumerator Attack(HealthController other){
		print("attacking");
		GetComponent<NavMeshAgent>().destination = transform.position;
		GetComponent<Animator>().SetBool("attack", true);
		GetComponent<NavMeshAgent>().acceleration = 0;
		GetComponent<NavMeshAgent>().speed = 0;
		other.health -= damage;
		yield return new WaitForSeconds(attackRate);
		print("waited");
		if(isAttacking){
			StartCoroutine(Attack(other));
		}
	}

	void UnAttack(){
		isAttacking = false;
		GetComponent<NavMeshAgent>().acceleration = 8;
		GetComponent<NavMeshAgent>().speed = .7f;
		GetComponent<Animator>().SetBool("attack", false);
	}
}
