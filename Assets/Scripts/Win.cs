using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

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

    public IEnumerator EndGame()
    {
        
            if (gameManager.scorePlayerA > gameManager.scorePlayerB)
            {
                endText.text = ($"<color=#87CEEB>Player A a gagné avec {gameManager.scorePlayerA} but");

            }
            else if (gameManager.scorePlayerA < gameManager.scorePlayerB)
            {
                endText.text = ($"<color=#FF0000>Player B a gagné avec {gameManager.scorePlayerB} but");
            }
            else if (gameManager.scorePlayerA == gameManager.scorePlayerB)
            {
                endText.text = ("<color=#000000>Egalité");
            }
            else
            {
                endText.text = ("");
            }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
