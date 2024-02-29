using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float totalTime = 3f;
    private float timeRemaining;
    public TextMeshProUGUI countdownText;

    void Start()
    {
        countdownText = GetComponentInChildren<TextMeshProUGUI>();
        timeRemaining = totalTime;
    }

    void Update()
    {
        if (timeRemaining >= 1)
        {
            timeRemaining -= Time.deltaTime;
            UpdateCountdownUI();
        }
        else
        {
            FindObjectOfType<Win>().EndGame();
            Time.timeScale = 0f;
            FindObjectOfType<GameManager>().KickOff();
            Debug.Log("Countdown finished!");
        }
    }

    void UpdateCountdownUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        countdownText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
