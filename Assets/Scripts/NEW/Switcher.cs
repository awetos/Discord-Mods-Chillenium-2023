using UnityEngine;

public class Switcher : MonoBehaviour{
	
	public player currentPlayer;
	public GameObject playerOne;
	public GameObject playerTwo;

	public enum player{
		player1, player2
	}
	public void SwitchPlayer(){
		if(currentPlayer == player.player1){
			currentPlayer = player.player2;
			playerOne.GetComponent<Rigidbody>().velocity = Vector3.zero;
			playerOne.GetComponent<Animator>().SetBool("running", false);
			playerOne.GetComponent<CapsuleCollider>().isTrigger = true;
			playerTwo.GetComponent<CapsuleCollider>().isTrigger = false;
			playerOne.GetComponent<PlayerController>().canMove = false;
			playerOne.GetComponent<PlayerController>().canAttack = false;
			playerOne.GetComponent<PlayerController>().canShoot = false;
			playerTwo.GetComponent<PlayerController>().canMove = true;
			playerTwo.GetComponent<PlayerController>().canShoot = true;
			playerTwo.GetComponent<PlayerController>().canAttack = true;
		}
		else{
			currentPlayer = player.player1;
			playerTwo.GetComponent<Rigidbody>().velocity = Vector3.zero;
			playerTwo.GetComponent<Animator>().SetBool("running", false);
			playerOne.GetComponent<CapsuleCollider>().isTrigger = false;
			playerTwo.GetComponent<CapsuleCollider>().isTrigger = true;
			playerOne.GetComponent<PlayerController>().canMove = true;
			playerOne.GetComponent<PlayerController>().canAttack = true;
			playerOne.GetComponent<PlayerController>().canShoot = true;
			playerTwo.GetComponent<PlayerController>().canMove = false;
			playerTwo.GetComponent<PlayerController>().canShoot = false;
			playerTwo.GetComponent<PlayerController>().canAttack = false;
		}
	}
}
