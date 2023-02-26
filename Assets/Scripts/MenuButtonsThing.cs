using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonsThing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    public Sprite firstImg;
	public Sprite lastImg;
	bool ishovering = false;

	void Update(){
		if(Input.GetMouseButtonDown(0) && ishovering){
			GetComponent<Animator>().enabled = false;
			GetComponent<Image>().sprite = lastImg;
		}
		if(Input.GetMouseButtonUp(0)){
			GetComponent<Image>().sprite = firstImg;
		}
	}

    public void OnPointerEnter(PointerEventData eventData){
        GetComponent<Animator>().SetBool("isHovering", true);
		ishovering = true;
    }

    public void OnPointerExit(PointerEventData eventData){
        GetComponent<Animator>().SetBool("isHovering", false);
		ishovering = false;
    }
}