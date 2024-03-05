using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeNaviguation : MonoBehaviour
{
    [SerializeField] private List<Vector3> PosDrone;
    [SerializeField] private bool enableTargetBall;
    [SerializeField] private bool exitDistanceTarget;
    private bool noDouble = false;
    private bool activeTirBullet = false;
    private Vector3 destination;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float finishDistanceTarget;

    private int targetIndex;
    private int randomAttack;

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
        CheckDrone();
    }

    private void MoveDrone()
    {
        if (Vector3.Distance(transform.position, destination) <= finishDistanceTarget)
        {
            targetIndex++;
            randomAttack = Random.Range(0, 3);
            if (targetIndex < PosDrone.Count)
            {
                if (randomAttack == 1 && !activeTirBullet)
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
                    // direction Y < 1
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
