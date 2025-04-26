using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeLeft = 60;
    public bool timerisRunning = false;
    public TextMeshProUGUI timerText;
    private PlayerController playerController;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        timerisRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerisRunning)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                DisplayTime(timeLeft);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeLeft = 0;
                timerisRunning = false;
                GameOver();

            }
        }
        
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    void GameOver()
    {
        if (playerController != null)
        {
            playerController.GameOver();
        }
    }
    
    public void StopTimer()
    {
        timerisRunning = false;
    }

    
}
