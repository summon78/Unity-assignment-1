using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public TMP_Text scoreText;

    public void AddScoreDebugFeedback()
    {
        Debug.Log("Score added!");

    }

    void Awake()
    {
        instance = this;
    }

    public void AddScore()
    {
        score = score + 1;
        scoreText.text = "Score: " + score;

    }





}
