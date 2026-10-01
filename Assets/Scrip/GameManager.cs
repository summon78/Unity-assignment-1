using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Over UI")]
    //show the result panel when game over
    public GameObject resultPanel;
    //show the survive time and score when game over
    public TextMeshProUGUI timerText;
    //show the final score when game over
    public TextMeshProUGUI finalScoreText; 

    private float startTime;
    private bool isGameOver = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        startTime = Time.time;
        //when the game starts, the result panel should be hidden
        if (resultPanel != null) resultPanel.SetActive(false); 
    }

    void Update()
    {
        
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // when game over the game pauses
        Time.timeScale = 0f;

        // when game over show the result panel
        if (resultPanel != null) resultPanel.SetActive(true);

        // count the survive time and display it
        float surviveTime = Time.time - startTime;
        if (timerText != null)
        {
            timerText.text = "Survived: " + surviveTime.ToString("F1") + "s";
        }

        // count the final score and display it
        if (finalScoreText != null && ScoreManager.instance != null)
        {
            finalScoreText.text = "Score: " + ScoreManager.instance.score;
        }
    }
}