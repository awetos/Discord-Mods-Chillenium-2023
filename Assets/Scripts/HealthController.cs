using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour{
	[SerializeField]private Switcher.player player;
	[SerializeField]private Switcher switcher;
	[SerializeField]private Image healthUI;
	[SerializeField]private Sprite[] healthUIImages;
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
		percent = health / maxHealth;
		healthUI.sprite = healthUIImages[(int)((1-percent)*(healthUIImages.Length-1))];
	}

	IEnumerator reduceHealth(){
		if(health > decayAmount){
			health -= decayAmount;
		}
		else{
			health = 0;
		}
		isDecaying=false;
		yield return new WaitForSeconds(1f);
		if(switcher.currentPlayer != player)
			isDecaying=true;
    }

	IEnumerator Death(){
		isDecaying=false;
		GetComponent<Animator>().SetBool("dead", true);
		GetComponent<Rigidbody2D>().isKinematic = true;
		gameObject.tag = "NonPlayer";
		GetComponent<PlayerController>().enabled = false;
		yield return new WaitForSeconds(2);
		GetComponent<DeathScreen>().ShowDeathScreen();
	}
}
