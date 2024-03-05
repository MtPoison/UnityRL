using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneWeapon : MonoBehaviour
{
    [SerializeField] private Transform drone;
    [SerializeField] private Transform ball;
    [SerializeField] private float droneSpeed;
    public Transform pivot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
/*        Vector3 targetP = pivot.position;
        transform.RotateAround(targetP, Vector3.forward, 50 * Time.deltaTime);

        Vector3 direction = (ball.position - transform.position).normalized;
        transform.Translate(direction * Time.deltaTime * droneSpeed, Space.World);*/
    }
}
