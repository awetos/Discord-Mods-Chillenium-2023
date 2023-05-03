using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour {
    public Sprite[] firstImg;
	public Sprite[] lastImg;
	[SerializeField]private AudioClip hoverSnd;
	[SerializeField]private AudioClip clickSnd;
	[SerializeField]private GameObject[] menuItems;
	[SerializeField]private int i=0;
	[SerializeField]Controller controller;

	public enum Menu{
		MainMenu,
		PauseMenu,
		DeathMenu
	}

	public Menu menu;

	void Awake(){
		menuItems[i].GetComponent<Animator>().SetBool("isHovering", true);
	}

	
	// Update is called once per frame
	void Update () {
		if(!menuItems[i].GetComponent<Animator>().GetBool("isHovering")){
			menuItems[i].GetComponent<Animator>().SetBool("isHovering", true);	
		}
		//press down
		if(controller.control.downmenu.triggered){
			menuItems[i].GetComponent<AudioSource>().clip = hoverSnd;
			menuItems[i].GetComponent<AudioSource>().Play();
			if(i<=0){
				//go to 1
				menuItems[i].GetComponent<Animator>().SetBool("isHovering", false);
				i++;
				menuItems[i].GetComponent<Animator>().SetBool("isHovering", true);
			}
		}
		//press up
		if(controller.control.upmenu.triggered){
			menuItems[i].GetComponent<AudioSource>().clip = hoverSnd;
			menuItems[i].GetComponent<AudioSource>().Play();
			if(i>=(menuItems.Length-1)){
				//go to 0
				menuItems[i].GetComponent<Animator>().SetBool("isHovering", false);
				i--;
				menuItems[i].GetComponent<Animator>().SetBool("isHovering", true);
			}
		}
		//press x
		if(controller.control.Select.triggered){
			menuItems[i].GetComponent<Animator>().enabled = false;
			menuItems[i].GetComponent<Image>().sprite = lastImg[i];
			menuItems[i].GetComponent<AudioSource>().clip = clickSnd;
			menuItems[i].GetComponent<AudioSource>().Play();
			switch (menu){
			case Menu.MainMenu:
				switch(i){
				case 0:
					StartGame();
				break;
				case 1:
					QuitGame();
				break;
				}
			break;
			case Menu.PauseMenu:
				switch(i){
				case 0:
					mm();
				break;
				case 1:
					QuitGame();
				break;
				}
			break;
			case Menu.DeathMenu:
				switch(i){
					case 0:
						mm();
					break;
					case 1:
						QuitGame();
					break;
					}
			break;
			}

		}
	}
	public void StartGame(){SceneManager.LoadScene(1);}

	public void QuitGame(){Application.Quit();}
	public void mm(){
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
		}
	public void Retry(){
		SceneManager.UnloadSceneAsync(1);
		SceneManager.LoadScene(1);
	}
}
