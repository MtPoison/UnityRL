using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float totalTime;
    private float timeRemaining;
    private TextMeshProUGUI countdownText;
    private bool gameEnded = false;
    void Start()
    {
        countdownText = GetComponentInChildren<TextMeshProUGUI>();
        timeRemaining = totalTime;
    }

    void Update()
    {
        if (!gameEnded)
        {
            if (timeRemaining >= 1)
            {
                timeRemaining -= Time.deltaTime;
                UpdateCountdownUI();
            }
            else
            {
                Test();
            }
        }
    }
    void Test()
    {
        gameEnded = true;
        StartCoroutine(FindObjectOfType<Win>().EndGame());
    }
    void UpdateCountdownUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        countdownText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
