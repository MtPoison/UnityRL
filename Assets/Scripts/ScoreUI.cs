using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{

    private TextMeshProUGUI scoreText;
    private GameManager gameManager;
    void Start()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        scoreText.text = $"{gameManager.scorePlayerA}             {gameManager.scorePlayerB}";
    }
}
