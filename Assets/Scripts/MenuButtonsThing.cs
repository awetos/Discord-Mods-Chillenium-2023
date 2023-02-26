using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonsThing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    public Sprite firstImg;
	public Sprite lastImg;
	bool ishovering = false;
	[SerializeField]private AudioClip hoverSnd;
	[SerializeField]private AudioClip clickSnd;

	void Update(){
		if(Input.GetMouseButtonDown(0) && ishovering){
			GetComponent<Animator>().enabled = false;
			GetComponent<Image>().sprite = lastImg;
			GetComponent<AudioSource>().clip = clickSnd;
			GetComponent<AudioSource>().Play();
		}
		if(Input.GetMouseButtonUp(0)){
			GetComponent<Image>().sprite = firstImg;
		}
	}

    public void OnPointerEnter(PointerEventData eventData){
        GetComponent<Animator>().SetBool("isHovering", true);
		GetComponent<AudioSource>().clip = hoverSnd;
		GetComponent<AudioSource>().Play();
		ishovering = true;
    }

    public void OnPointerExit(PointerEventData eventData){
        GetComponent<Animator>().SetBool("isHovering", false);
		ishovering = false;
    }
}