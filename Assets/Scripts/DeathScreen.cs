using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{

    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI leaderboardTxt;

    [SerializeField] private AudioSource gameover;
    [SerializeField] private GameObject deathScreen;
	//[SerializeField] private GameObject numbersCanvas;
    private bool playedOnce = false;
    float deathDelay = 2.5f;

    // Start is called before the first frame update
    private void Start(){
	timer.StartTimer();
        leaderboardTxt.text = "Best: " + PlayerPrefs.GetString("Time");
    }
    public void ShowDeathScreen(){
		timer.StopTimer();
        //show death screen
        if (PlayerPrefs.GetFloat("HighTime") <= 0)
            PlayerPrefs.SetFloat("HighTime", 0);
        if (PlayerPrefs.GetString("Time") == null)
            PlayerPrefs.SetString("Time", "00:00:00");
        if (timer.timeElapsed > PlayerPrefs.GetFloat("HighTime"))
        {
            PlayerPrefs.SetFloat("HighTime", timer.timeElapsed);
            PlayerPrefs.SetString("Time", timer.timeTxt);
        }
        leaderboardTxt.text = "Best: " + PlayerPrefs.GetString("Time");
        timer.StopTimer();
        StartCoroutine("ShowDeathScreenAfterTime");
    }

    IEnumerator ShowDeathScreenAfterTime(){
        yield return new WaitForSeconds(deathDelay);
		//numbersCanvas.SetActive(false);
        Camera.main.GetComponent<AudioSource>().Stop();
        if (!playedOnce){
            gameover.Play();
            print(gameover.clip + " " + gameover.isPlaying);
            playedOnce = true;
        }
        deathScreen.SetActive(true);
    }
}
