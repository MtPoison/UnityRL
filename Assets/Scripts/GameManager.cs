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
    [SerializeField] private Obstacle obstacle1;
    [SerializeField] private Obstacle obstacle2;

    public int scorePlayerA { get; private set; }
    public int scorePlayerB { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        obstacle1.Generate();
        obstacle2.Generate();
        scorePlayerA = 0;
        scorePlayerB = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void KickOff()
    {
        FindObjectOfType<Obstacle>();
        ball.transform.position = ballStart.position;
        playerA.transform.position = playerAStart.position;
        playerB.transform.position = playerBStart.position;
        playerA.transform.rotation = playerAStart.rotation;
        playerB.transform.rotation = playerBStart.rotation;
    }

    public void AntiUnderMapBall()
    {
        ball.transform.position = ballStart.position;
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