using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{
	[SerializeField]private GameObject mm;
	[SerializeField]private GameObject lb;
    
	public void StartGame(){SceneManager.LoadScene(1);}

	public void Leaderboard(){
		mm.SetActive(false);
		lb.SetActive(true);
	}

	public void QuitGame(){Application.Quit();}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab)){
			if(lb.activeInHierarchy){
				lb.SetActive(false);
				mm.SetActive(true);
			}
		}
	}

}
