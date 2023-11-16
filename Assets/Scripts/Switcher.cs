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
			//disable player 1
			playerOne.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			playerOne.GetComponent<Animator>().SetBool("running", false);
			playerOne.GetComponent<CapsuleCollider2D>().isTrigger = true;
			playerOne.GetComponent<Rigidbody2D>().isKinematic = true;
			playerOne.GetComponent<PlayerController>().canMove = false;
			playerOne.GetComponent<PlayerController>().canAttack = false;
			playerOne.GetComponent<PlayerController>().canShoot = false;
			playerOne.GetComponent<HealthController>().isDecaying = true;
			playerOne.tag = "NonPlayer";
			////disable animator parameters
				playerOne.GetComponent<Animator>().SetBool("attack",false);
				playerOne.GetComponent<Animator>().SetBool("running",false);
			//enable player 2
			playerTwo.GetComponent<CapsuleCollider2D>().isTrigger = false;
			playerTwo.GetComponent<Rigidbody2D>().isKinematic = false;
			playerTwo.GetComponent<PlayerController>().canMove = true;
			playerTwo.GetComponent<PlayerController>().canShoot = true;
			playerTwo.GetComponent<PlayerController>().canAttack = true;
			playerTwo.GetComponent<HealthController>().isDecaying = false;
			playerTwo.tag = "Player";
		}
		else{
			currentPlayer = player.player1;
			//disable player 2
			playerTwo.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			playerTwo.GetComponent<Animator>().SetBool("running", false);
			playerTwo.GetComponent<CapsuleCollider2D>().isTrigger = true;
			playerTwo.GetComponent<Rigidbody2D>().isKinematic = true;
			playerTwo.GetComponent<PlayerController>().canMove = false;
			playerTwo.GetComponent<PlayerController>().canShoot = false;
			playerTwo.GetComponent<PlayerController>().canAttack = false;
			playerTwo.GetComponent<HealthController>().isDecaying = true;
			playerTwo.tag = "NonPlayer";
			////disable animator parameters
				playerTwo.GetComponent<Animator>().SetBool("attack",false);
				playerTwo.GetComponent<Animator>().SetBool("running",false);
			//enable player 1
			playerOne.GetComponent<CapsuleCollider2D>().isTrigger = false;
			playerOne.GetComponent<Rigidbody2D>().isKinematic = false;
			playerOne.GetComponent<PlayerController>().canMove = true;
			playerOne.GetComponent<PlayerController>().canAttack = true;
			playerOne.GetComponent<PlayerController>().canShoot = true;
			playerOne.GetComponent<HealthController>().isDecaying = false;
			playerOne.tag = "Player";
		}
	}
}
