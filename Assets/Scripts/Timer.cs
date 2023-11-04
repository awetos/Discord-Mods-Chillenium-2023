using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour{
    public TextMeshProUGUI timerText;//ui text for timer (uses text mesh pro for some reason)
    private float startTime;//sets current time as start time
    private bool timerActive;//is timer active
	public float timeElapsed;
	public string timeTxt;

    void Awake(){
		timeElapsed = 0;
        StartTimer();
    }

    void Update(){
        if (timerActive){
            timeElapsed = Time.time - startTime;
			int hours = (int)(timeElapsed / 60 / 60);
            int minutes = (int)(timeElapsed / 60);
            int seconds = (int)(timeElapsed % 60);

            string hoursString = hours.ToString("00");
			string minutesString = minutes.ToString("00");
            string secondsString = seconds.ToString("00");

            timeTxt = hoursString + ":" + minutesString + ":" + secondsString;

            timerText.text = timeTxt;
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