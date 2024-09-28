using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; 

    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;

    public CharacterMovement characterMovement;
    public GemFallScript gemSpawner;

    private float remainingTime;
    public int score = 0;

    private bool isGameOver = false; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        remainingTime = 60f;
        StartCoroutine(CountdownTimer());
        gameOverPanel.SetActive(false);
        UpdateScoreText();
    }

    void Update()
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score + " | Time: " + Mathf.CeilToInt(remainingTime);
    }

    private IEnumerator CountdownTimer()
    {
        while (remainingTime > 0 && !isGameOver)
        {
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        if (!isGameOver) 
        {
            GameOver();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Điểm hiện tại: " + score);
        UpdateScoreText();
    }

    public void DecreaseScore(int amount)
    {
        score -= amount;
        Debug.Log("Điểm hiện tại sau khi trừ: " + score);
        UpdateScoreText();

        if (score < 0)
        {
            Debug.Log("Điểm hiện tại là số âm!");
        }
    }

    public void GameOver()
    {
        isGameOver = true; 
        gameOverPanel.SetActive(true);
        gameOverText.text = "Game Over!\nScore: " + score;

        if (characterMovement != null)
        {
            characterMovement.enabled = false;
        }

        if (gemSpawner != null)
        {
            gemSpawner.enabled = false;
        }

        ObstacleMover[] obstacles = FindObjectsOfType<ObstacleMover>();
        foreach (var obstacle in obstacles)
        {
            obstacle.StopMovement();
        }

        Debug.Log("Game Over! Final Score: " + score);
    }
}
