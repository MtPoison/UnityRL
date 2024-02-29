using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Win : MonoBehaviour
{
    private GameManager gameManager;
    private TextMeshProUGUI endText;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        endText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame()
    {
        if (gameManager.scorePlayerA > gameManager.scorePlayerB)
        {
            endText.text = ($"<color=#87CEEB>Player A a gagné avec {gameManager.scorePlayerA} but");
            
        }
        else if (gameManager.scorePlayerA < gameManager.scorePlayerB)
        {
            Debug.Log("ok");
            endText.text = ($"<color=#FF0000>Player B a gagné avec {gameManager.scorePlayerB} but");
        }
        else if (gameManager.scorePlayerA == gameManager.scorePlayerB)
        {
            endText.text = ("Egalité");
        }
        else
        {
            endText.text = ("");
        }
    }
}
