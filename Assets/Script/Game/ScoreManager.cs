using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private Text tmpScore;
    public int score;

    void Awake()
    {
        instance = this;
        score = 0;
        UpdateScore();
    }
    

    public void SaveHighScore()
    {
        var high =  PlayerPrefs.GetInt("highScore");
        if(score > high) PlayerPrefs.SetInt("highScore", score);
    }   

    public void UpdateScore() => tmpScore.text = score.ToString();
  

  
}
