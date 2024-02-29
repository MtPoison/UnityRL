using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class GameManager : MonoBehaviour
{
    public enum PlayerEnum
    {
        PlayerA,
        PlayerB,
    }

    [SerializeField] private Ball ball;
    [SerializeField] private Player playerA;
    [SerializeField] private Player playerB;

    [SerializeField] private Transform ballStart;
    [SerializeField] private Transform playerAStart;
    [SerializeField] private Transform playerBStart;

    public int scorePlayerA { get; private set; }
    public int scorePlayerB { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        scorePlayerA = 0;
        scorePlayerB = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void KickOff()
    {
        ball.transform.position = ballStart.position;
        playerA.transform.position = playerAStart.position;
        playerB.transform.position = playerBStart.position;
        playerB.transform.rotation = playerAStart.rotation;
        playerB.transform.rotation = playerBStart.rotation;
    }

    public void Goalaso(PlayerEnum player)
    {
        switch (player)
        {
            case PlayerEnum.PlayerA:
                scorePlayerA++;
                break;
            case PlayerEnum.PlayerB:
                scorePlayerB++;
                break;
        }
    }
}