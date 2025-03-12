using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreTxt;
    public TMP_Text timerTxt;
    private int score;
    private float timeTracker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //test += 1;
        //UpdateScore(test);
        UpdateTimer();
    }

    //Pass in the points earned by the player to add to score and display in UI
    public void UpdateScore(int points)
    {
        score += points;
        scoreTxt.text = ("Score: " + string.Format("{0:0000000}", score));
    }

    private void UpdateTimer()
    {
        timeTracker += Time.deltaTime;
        int seconds = Mathf.FloorToInt(timeTracker % 60);
        int minutes = Mathf.FloorToInt(timeTracker / 60);
        timerTxt.text = ("Time: " + string.Format("{0:00}:{1:00}", minutes, seconds));
    }
}
