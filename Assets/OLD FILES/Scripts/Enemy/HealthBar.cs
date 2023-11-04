using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    Vector3 localLocation;
    Vector3 originalOffset;


    // Start is called before the first frame update
    void Start()
    {
        originalOffset = transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        LockHealthBar();
    }

    [SerializeField] SpriteRenderer mainBar_renderer;
    [SerializeField] SpriteRenderer redBar_renderer;

    public void MakeBarAppear()
    {
        mainBar_renderer.enabled = true;
        redBar_renderer.enabled = true;
        redBar_renderer.transform.position = new Vector3(1,0,0);
    }
    public void DecreaseHealth(float newPercent)
    {
        //if it's 90%, move the bar to 0.9
        redBar_renderer.transform.localPosition = new Vector3(newPercent, 0, 0);
    }

    public void Disappear()
    {
        mainBar_renderer.enabled = false;
        redBar_renderer.enabled = false;
    }
    public void ResetHealthBar()
    {
        redBar_renderer.transform.position = new Vector3(1, 0, 0);
    }

    void LockHealthBar()
    {
        localLocation = transform.parent.position + originalOffset;

        transform.position = localLocation;
        transform.eulerAngles = Vector3.zero + new Vector3 (90f,0,0);
    }
}
