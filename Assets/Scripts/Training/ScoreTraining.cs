using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTraining : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        var foundMovingTargetObjects = FindObjectsByType<MovingTarget>(FindObjectsSortMode.None);
        int stockGoalPlayerA = 0;
        int stockGoalPlayerB = 0;
        // parcours chaque target et ajoute le score du player
        foreach (var item in foundMovingTargetObjects)
        {
            stockGoalPlayerA += item.GetScorePlayerA();
            stockGoalPlayerB += item.GetScorePlayerB();
            
        }
        scoreText.text = $"  {stockGoalPlayerA}  {stockGoalPlayerB}";
    }
}
