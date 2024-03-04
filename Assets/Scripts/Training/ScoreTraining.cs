using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTraining : MonoBehaviour
{

    private TextMeshProUGUI scoreText;
    private MovingTarget movingTarget;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
        movingTarget = FindAnyObjectByType<MovingTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $" {movingTarget.GetScorePlayerA()}   {movingTarget.GetScorePlayerB()}";
    }
}
