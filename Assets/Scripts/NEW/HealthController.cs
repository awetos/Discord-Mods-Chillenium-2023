using System.Collections;
using UnityEngine;

public class HealthController : MonoBehaviour{
	[SerializeField]private Switcher.player player;
	[SerializeField]private Switcher switcher;
	public int maxHealth;
	public int health;
	public int decayAmount;
	public bool isDecaying = true;
	public float percent;

	void Start() {
		if(switcher.currentPlayer == player){
			isDecaying = true;
		}
		else{
			isDecaying = false;
		}
	}
	void Update(){
		if(isDecaying){
			StartCoroutine(reduceHealth());
		}
	}
	IEnumerator reduceHealth(){
		health -= decayAmount;
		isDecaying=false;
		if (health <= 0){
			isDecaying=false;
			GetComponent<Animator>().SetBool("dead", true);
		}
		percent = ((float)health) / ((float)maxHealth);
		yield return new WaitForSeconds(1f);
		isDecaying=true;
    }
}
