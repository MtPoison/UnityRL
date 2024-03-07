using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeNaviguation : MonoBehaviour
{
    [SerializeField] private List<Vector3> PosDrone;
    [SerializeField] private bool enableTargetBall;
    [SerializeField] private bool exitDistanceTarget;
    private bool noDouble = false;
    private bool noDoubleRandom = false;
    private bool activeTirBullet = false;
    private Vector3 destination;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float finishDistanceTarget;

    private int targetIndex;
    private int randomAttack;
    private int randomDropY;

    [SerializeField] private Transform ball;
    [SerializeField] private Rigidbody ballRigidbody;
    [SerializeField] private ModeNaviguation modeNaviguation;
    public GameObject instancePrefab;
    public GameObject destroyPrefab;

    //Getters
    public bool GetActiveTirBullet() { return activeTirBullet; }
    public float GetFinishDistanceTarget() { return finishDistanceTarget; }
    public float GetMoveSpeed() { return moveSpeed; }

    //Setters
    public bool SetActiveTirBullet (bool activeTirBullet) { return activeTirBullet; }

    private void Start()
    {
        targetIndex = 0;
        SetDestination(PosDrone[targetIndex]);
    }

    private void Update()
    {
        MoveDrone();
        GetBallWithDrone();
    }

    private void MoveDrone()
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
                if (randomAttack == 2)
                {
                    // ne pas le dupliquer + ne lance pas le bullet si le drone est en train de chercher la balle
                    if (!noDouble && !enableTargetBall)
                    {
                        activeTirBullet = true;
                        instancePrefab.GetComponent<BulletDrone>().ball = ball;
                        instancePrefab.GetComponent<BulletDrone>().modeNaviguationRigidbody = ballRigidbody;
                        instancePrefab.GetComponent<BulletDrone>().modeNaviguation = modeNaviguation;
                        destroyPrefab = Instantiate(instancePrefab, transform.position, Quaternion.identity);
                        Debug.Log("active tir");
                        noDouble = true;
                    }
                }
                else
                {
                    SetDestination(PosDrone[targetIndex]);
                    noDouble = false;
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
            // trajectoire normal du drone entre ces points
            if (!enableTargetBall)
            {
                Vector3 direction = (destination - transform.position).normalized;
                transform.Translate(direction * Time.deltaTime * moveSpeed, Space.World);
            }

            // trajectoire quand le drone à la balle
            if (exitDistanceTarget)
            {
                if (!noDoubleRandom)
                {
                    randomDropY = Random.Range(0, 2);
                    noDoubleRandom = true;
                }
                if (randomDropY == 0)
                {
                    Vector3 direction = (destination - transform.position);
                    direction.y = 0;
                    transform.Translate(direction.normalized * Time.deltaTime * moveSpeed, Space.World);
                    ball.position = transform.position;
                }
                else
                {
                    Vector3 direction = (destination - transform.position).normalized;
                    transform.Translate(direction * Time.deltaTime * moveSpeed, Space.World);
                    ball.position = transform.position;
                }
            }
        }
    }

    private void GetBallWithDrone()
    {
        if (enableTargetBall)
        {
            // Check si le drone à attrapé la balle
            if (Vector3.Distance(transform.position, ball.position) <= finishDistanceTarget)
            {
                ballRigidbody.isKinematic = true;
                SetDestination(PosDrone[targetIndex]);
                // enclenche le nouveau déplacement du drone
                exitDistanceTarget = true;
                // si le drone choisit de drop en bas alors ne pas prendre en compte le Y
                if (randomDropY == 0)
                {
                    destination.y = transform.position.y;
                }
                // Check si le drone à atteint sa destination pour drop la balle
                if (Vector3.Distance(transform.position, destination) <= finishDistanceTarget)
                {
                    Debug.Log("arrivé");
                    ball.position = transform.position;
                    enableTargetBall = false;
                    ballRigidbody.isKinematic = false;
                    // le drone reprend sa trajectoire habituelle
                    exitDistanceTarget = false;
                    noDoubleRandom = false;
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
