using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreTxt;
    public TMP_Text timerTxt;
    public Canvas gameCanvas;
    public Canvas winCanvas;
    public Canvas loseCanvas;
    public TMP_Text winScore;
    public TMP_Text winTimer;
    public TMP_Text lives;
    public TMP_Text level;
    private int score;
    private int seconds;
    private int minutes;
    private float timeTracker;
    public GameObject wall;
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
        seconds = Mathf.FloorToInt(timeTracker % 60);
        minutes = Mathf.FloorToInt(timeTracker / 60);
        timerTxt.text = ("Time: " + string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    public void UpdateLevel(int newLevel) 
    {
        level.text = $"Level: {newLevel}";
    }

    public void UpdateLives(int newLives) 
    {
        lives.text = $"x {newLives}";
    }

    public void WinGame()
    {
        wall.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
        winCanvas.gameObject.SetActive(true);
        winScore.text = ("Score: " + string.Format("{0:0000000}", score));
        winTimer.text = ("Time: " + string.Format("{0:00}:{1:00}", minutes, seconds));
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().paused = true;
    }

    public void LoseGame()
    {
        gameCanvas.gameObject.SetActive (false);
        loseCanvas.gameObject.SetActive(true);
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().paused = true;
    }

    public void ReturnToMenu()
    {
        FindAnyObjectByType<LivesManager>().Reset();
        SceneManager.LoadScene(0);
    }
}
