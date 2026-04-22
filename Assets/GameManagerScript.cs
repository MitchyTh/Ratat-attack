using NUnit.Framework;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public bool gameStarted = false;
    public bool roundOver = false;
    public int playerScore = 0;
    public float roundTime = 420f;
    public float timeRemaining = 420f;
    public TMPro.TextMeshProUGUI timerText, scoreText, winText;
    public int winScore = 2000;
    public bool ratWon = false, chefWon = false;


    public static GameManagerScript Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void startGame()
    {
        gameStarted = true;
        roundOver = false;
        playerScore = 0;
        timeRemaining = roundTime;
        chefWon = false;
        ratWon = false;
    }

    // Update is called once per frame
    void Update()
    {

        timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
        scoreText.text = playerScore.ToString();

        if (gameStarted)
        {
            timeRemaining -= Time.deltaTime;
            if (playerScore >= winScore)
            {
                roundOver = true;
                chefWon = true;
                winText.text = "Chef wins!";
                gameStarted = false;
            }

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                roundOver = true;

                ratWon = true;
                winText.text = "Rat wins!";
                gameStarted = false;
            }
        }
        else
        {
            timeRemaining = 0;
            playerScore = 0;
            chefWon = false;
            ratWon = false;
        }
    }

    public void increaseScore(int amount)
    {
        playerScore += amount;
    }
}
