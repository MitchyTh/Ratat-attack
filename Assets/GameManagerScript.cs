using NUnit.Framework;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public bool gameStarted = false;
    public bool roundOver = false;
    public int playerScore = 0;
    public float roundTime = 420f;
    public float timeRemaining = 420f;
    public TMPro.TextMeshProUGUI timerText, scoreText;

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

    // Update is called once per frame
    void Update()
    {

        timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
        scoreText.text = playerScore.ToString();

        if (gameStarted && !roundOver)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                roundOver = true;
                // Handle end of round logic here
            }
        }
    }

    public void increaseScore(int amount)
    {
        playerScore += amount;
    }
}
