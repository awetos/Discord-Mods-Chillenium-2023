using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour{
    public TextMeshProUGUI timerText;//ui text for timer (uses text mesh pro for some reason)
    private float startTime;//sets current time as start time
    private bool timerActive;//is timer active

    void Start(){
        timerActive = true;
    }

    void Update(){
        if (timerActive){
            float timeElapsed = Time.time - startTime;
			int hours = (int)(timeElapsed / 60 / 60);
            int minutes = (int)(timeElapsed / 60);
            int seconds = (int)(timeElapsed % 60);

            string hoursString = hours.ToString("00");
			string minutesString = minutes.ToString("00");
            string secondsString = seconds.ToString("00");

            timerText.text = hoursString + ":" + minutesString + ":" + secondsString;
        }
    }

    public void StartTimer(){
        startTime = Time.time;
        timerActive = true;
    }

    public void StopTimer(){
        timerActive = false;
    }
}