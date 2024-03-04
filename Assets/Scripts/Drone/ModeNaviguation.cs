using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeNaviguation : MonoBehaviour
{
    [SerializeField] private List<Vector3> PosDrone;
    [SerializeField] private bool isMooving;
    [SerializeField] private bool enableTargetBall;
    [SerializeField] private bool exitDistanceTarget;
    private Vector3 destination;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float finishDistanceTarget;

    private int targetIndex;
    private int randomAttack;

    [SerializeField] private Transform ball;
    [SerializeField] private Rigidbody ballRigidbody;

    private void Start()
    {
        targetIndex = 0;
        SetDestination(PosDrone[targetIndex]);
    }

    private void Update()
    {
        MoveDrone();
        CheckDrone();
    }

    private void MoveDrone()
    {
        if (isMooving)
        {
            if (Vector3.Distance(transform.position, destination) <= finishDistanceTarget)
            {
                targetIndex++;
                randomAttack = Random.Range(0, 3);
                if (targetIndex < PosDrone.Count)
                {
                    if (randomAttack == 1)
                    {
                        Debug.Log("target la ball");
                        destination = ball.position;
                        enableTargetBall = true;
                    }
                    else
                    {
                        SetDestination(PosDrone[targetIndex]);
                    }
                }
                else
                {
                    targetIndex = 0;
                    SetDestination(PosDrone[targetIndex]);
                }
            }
            else
            {
                if (!enableTargetBall)
                {
                    Vector3 direction = (destination - transform.position).normalized;
                    transform.Translate(direction * Time.deltaTime * moveSpeed, Space.World);
                }

                if (exitDistanceTarget)
                {
                    Vector3 direction = (destination - transform.position).normalized;
                    transform.Translate(direction * Time.deltaTime * moveSpeed, Space.World);
                }

                if (enableTargetBall && Vector3.Distance(transform.position, ball.position) <= finishDistanceTarget)
                {
                    ball.position = transform.position;
                }
            }
        }
    }

    private void CheckDrone()
    {
        if (enableTargetBall)
        {
            if (Vector3.Distance(transform.position, ball.position) <= finishDistanceTarget)
            {
                ballRigidbody.isKinematic = true;
                Debug.Log("chopé");
                SetDestination(PosDrone[targetIndex]);
                exitDistanceTarget = true;
                if (Vector3.Distance(transform.position, destination) <= finishDistanceTarget)
                {
                    Debug.Log("arrivé");
                    ball.position = transform.position;
                    enableTargetBall = false;
                    ballRigidbody.isKinematic = false;
                    exitDistanceTarget = false;
                }
            }
            else
            {
                Vector3 direction = (ball.position - transform.position).normalized;
                transform.Translate(direction * Time.deltaTime * moveSpeed, Space.World);
            }
        }
    }

    private void SetDestination(Vector3 destination)
    {
        this.destination = destination;
    }
}
