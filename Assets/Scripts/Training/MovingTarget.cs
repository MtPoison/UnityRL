using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;
    private Vector3 destination;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float finishDistanceTarget;

    private int scorePlayerA;
    private int scorePlayerB;

    private bool startMovingEnd = true;

    // Getters
    public int GetScorePlayerA() { return scorePlayerA; }
    public int GetScorePlayerB() { return scorePlayerB; }

    private void Start()
    {
        destination = endPos;

        scorePlayerA = 0;
        scorePlayerB = 0;
    }

    private void Update()
    {
        MoveTarget();
    }

    private void MoveTarget()
    {
        // Check si le target à atteint sa destination
        if (Vector3.Distance(transform.localPosition, destination) <= finishDistanceTarget)
        {
            if (startMovingEnd) { 
                destination = startPos;
            }
            else { 
                destination = endPos;
            }
                
            startMovingEnd = !startMovingEnd;
        }

        // Déplacement du target
        Vector3 direction = (destination - transform.localPosition).normalized;
        transform.Translate(direction * Time.deltaTime * moveSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Vérifie si la collision est avec la balle
        if (collision.gameObject.CompareTag("BallTrainPlayerA"))
        {
            scorePlayerA++;
        }
        else if (collision.gameObject.CompareTag("BallTrainPlayerB"))
        {
            scorePlayerB++;
        }
    }
}
