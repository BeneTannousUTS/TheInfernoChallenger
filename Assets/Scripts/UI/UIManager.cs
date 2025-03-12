using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreTxt;
    private int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //test += 1;
        //UpdateScore(test);
    }

    //Pass in the points earned by the player to add to score and display in UI
    public void UpdateScore(int points)
    {
        score += points;
        scoreTxt.text = ("Score: " + string.Format("{0:0000000}", score));
    }
}
