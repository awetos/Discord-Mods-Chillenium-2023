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
    private bool playedOnce = false;

    // Start is called before the first frame update
    private void Start()
    {


        leaderboardTxt.text = "Best: " + PlayerPrefs.GetString("Time");
    }
    public void ShowDeathScreen()
    {


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

    float deathDelay = 2.5f;
    IEnumerator ShowDeathScreenAfterTime()
    {
        yield return new WaitForSeconds(deathDelay);
        Camera.main.GetComponent<AudioSource>().Stop();
        if (!playedOnce)
        {
            gameover.Play();
            print(gameover.clip + " " + gameover.isPlaying);
            playedOnce = true;
        }
        deathScreen.SetActive(true);
    }
}
