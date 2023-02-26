using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour{
	[SerializeField]private GameObject pauseMenu;
	[SerializeField]private GameObject Player1GO;
	[SerializeField]private GameObject Player2GO;

	void Start() {
		Time.timeScale = 1;
	}
	void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
			if(pauseMenu.activeInHierarchy){
				Time.timeScale = 1;
				Camera.main.GetComponent<AudioSource>().Play();
				pauseMenu.SetActive(false);
			}
			else{
				Time.timeScale = 0;
				Camera.main.GetComponent<AudioSource>().Pause();
				pauseMenu.SetActive(true);
			}
		}
    }
	public void mm(){SceneManager.LoadScene(0);}
	public void QuitGame(){Application.Quit();}
	public void Retry(){
		SceneManager.UnloadSceneAsync(1);
		SceneManager.LoadScene(1);
	}
}
