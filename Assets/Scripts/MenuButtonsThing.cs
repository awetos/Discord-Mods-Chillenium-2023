using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonsThing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;
    private Color originalColor;

    private void Awake(){
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color;
    }

    public void OnPointerEnter(PointerEventData eventData){
        //buttonImage.color = Color.gray; // Change color to indicate hover
    }

    public void OnPointerExit(PointerEventData eventData){
        //buttonImage.color = originalColor; // Change back to original color
    }
}