using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeartAnimationManager : MonoBehaviour
{
    [SerializeField] private Sprite[] activeSprites;
    [SerializeField] private Sprite[] boneySprites;


    int currentSprite;

    int totalSize;
    bool heartIsActive;


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
        totalSize = activeSprites.Length; 
        currentSprite = 0;
        //Debug.Log("total size is..." + totalSize);
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

    public void UpdateCurrentSpriteFromPercent(float percent) //called when health manager changes.
    {
        currentSprite = totalSize - Mathf.FloorToInt(percent * totalSize);
        if (currentSprite >= totalSize)
        {
            currentSprite = totalSize - 1;
        }
        UpdateSprite();
    }

    void UpdateSprite()
    {
        if (heartIsActive)
        {
            UpdateActive();
        }
        else
        {
            UpdateDecay();
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
        UpdateActive();
        heartIsActive = true;
    }
    void SwitchToDecay()
    {
        UpdateDecay();
        heartIsActive = false;
    }

    void UpdateActive()
    {
        myImage.sprite = activeSprites[currentSprite];

    }

    void UpdateDecay()
    {
        myImage.sprite = boneySprites[currentSprite];

    }
}
