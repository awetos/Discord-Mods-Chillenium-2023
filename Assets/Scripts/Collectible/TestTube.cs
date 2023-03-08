using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTube : MonoBehaviour
{
    public int healthToAdd;

    private void Start()
    {
        StartCoroutine("FadeAfterSeconds");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            Debug.Log("Add Health");
            GameObject.FindObjectOfType<NumbersCanvas>().CreateCoinText(this.transform,100);
            AddHealth();
            StartCoroutine("StartDisappearing");
        }
    }

    float fadeOutTime = 5f;
    IEnumerator FadeAfterSeconds()
    {
        yield return new WaitForSeconds(fadeOutTime);
        Disappear();
    }

    IEnumerator StartDisappearing()
    {
        
        yield return new WaitForSeconds(0.1f);
        Disappear();
    }

    void AddHealth()
    {
        Camera.main.GetComponent<HealthReferences>().AddHealth(healthToAdd) ;
    }
    void Disappear()
    {
        Destroy(this.gameObject);
    }


}
