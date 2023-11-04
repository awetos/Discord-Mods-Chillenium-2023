using UnityEngine;

public class pausemenu : MonoBehaviour{
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject Player1GO;
	[SerializeField] private GameObject Player2GO;
	[SerializeField] Controller controller;

	void Start() {
		Time.timeScale = 1;
	}
	void Update(){
		if(controller.control.Back.triggered){
			if(pauseMenu.activeInHierarchy){
				Time.timeScale = 1;
				Camera.main.GetComponent<AudioSource>().Play();
				pauseMenu.SetActive(false);
			}
		}
        if(controller.control.Start.triggered){
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
}
