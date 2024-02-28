using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{

    private TextMeshProUGUI scoreText;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"{gameManager.scorePlayerA} -- {gameManager.scorePlayerB}";
    }
}
