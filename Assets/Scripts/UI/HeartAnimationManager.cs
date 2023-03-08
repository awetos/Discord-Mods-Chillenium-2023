using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeartAnimationManager : MonoBehaviour
{
    [SerializeField] private Sprite[] activeSprites;
    [SerializeField] private Sprite[] boneySprites;


    int currentSprite;
    [SerializeField] Image myImage;
    [SerializeField] int myHeartID;

    private void OnEnable()
    {
        CameraScript.OnPlayerSwitched += OnPlayerSwitched;
    }
    private void OnDisable()
    {
        CameraScript.OnPlayerSwitched -= OnPlayerSwitched;
    }

    private void Start()
    {
        if (Camera.main.GetComponent<CameraScript>().isPlayerOne == true)
        {
            if(myHeartID == 0)
            {
                SwitchToActive();
            }
            else
            {
                SwitchToDecay();
            }
        }


    }


    void OnPlayerSwitched(int playerID)
    {
        if(playerID == myHeartID)
        {
            SwitchToActive();
        }
        else
        {
            SwitchToDecay();
        }
    }
    // Start is called before the first frame update
   
    void SwitchToActive()
    {
        myImage.sprite = activeSprites[0];
    }
    void SwitchToDecay()
    {
        myImage.sprite = boneySprites[0];
    }


}
