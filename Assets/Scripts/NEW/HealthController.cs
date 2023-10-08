using System.Collections;
using UnityEngine;

public class HealthController : MonoBehaviour{
	[SerializeField]private Switcher.player player;
	[SerializeField]private Switcher switcher;
	public float maxHealth;
	public float health;
	public float decayAmount;
	public bool isDecaying = false;
	public float percent;

	void Start() {
		if(switcher.currentPlayer == player){
			isDecaying = false;
		}
		else{
			isDecaying = true;
		}
	}
	void Update(){
		if(isDecaying){
			StartCoroutine(reduceHealth());
		}
		if (health <= 0){
			StartCoroutine(Death());
		}
	}
	IEnumerator reduceHealth(){
		health -= decayAmount;
		isDecaying=false;
		percent = ((float)health) / ((float)maxHealth);
		yield return new WaitForSeconds(1f);
		if(switcher.currentPlayer != player)
			isDecaying=true;
    }

	IEnumerator Death(){
		isDecaying=false;
		GetComponent<Animator>().SetBool("dead", true);
		gameObject.tag = "NonPlayer";
		GetComponent<PlayerController>().enabled = false;
		yield return new WaitForSeconds(2);
		//put up game over screen
	}
}
