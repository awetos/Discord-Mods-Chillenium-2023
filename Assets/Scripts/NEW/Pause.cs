using UnityEngine;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour{
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject Player1GO;
	[SerializeField] private GameObject Player2GO;
	public Playercontrols.PlayerActions controller;//player input
	public bool controllerMode;//is using controller
	
    public Sprite firstImg;
	public Sprite lastImg;
	bool ishovering = false;
	[SerializeField]private AudioClip hoverSnd;
	[SerializeField]private AudioClip clickSnd;

	void Start() {
		Time.timeScale = 1;
        Playercontrols controls = new Playercontrols();
		controller = controls.Player;
	}
	void Update(){
		if(controller.controllerused.triggered)
			controllerMode = true;
		if(controller.mouseused.triggered)
			controllerMode=false;


		if(controllerMode){
			if(controller.Back.triggered){
				UnpauseFunc();
			}
			if(controller.Start.triggered){
				UnpauseFunc();
			}
		}

		else{
			if(ishovering){
				GetComponent<Animator>().SetBool("isHovering", true);
				GetComponent<AudioSource>().clip = hoverSnd;
				GetComponent<AudioSource>().Play();
			}
			else{
				GetComponent<Animator>().SetBool("isHovering", false);
			}
		}
    }
	void PauseFunc(){
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
	void UnpauseFunc(){
		if(pauseMenu.activeInHierarchy){
			Time.timeScale = 1;
			Camera.main.GetComponent<AudioSource>().Play();
			pauseMenu.SetActive(false);
		}
	}

	//to enable controls
	private void OnEnable() {
		controller.Enable();
	}

	private void OnDisable() {
		controller.Disable();
	}

	public void OnPointerEnter(PointerEventData eventData){
		ishovering = true;
    }

    public void OnPointerExit(PointerEventData eventData){
		ishovering = false;
    }
}
