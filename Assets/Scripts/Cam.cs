using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cam : MonoBehaviour
{
    [SerializeField] private string focusInputAxis;
    private bool focusBall = false;
    [SerializeField] private string tagBall;
    private Transform ball;
    private Transform player;
    [SerializeField] float rotationSpeed = 5f;

    void Start()
    {
        player = GetComponentInParent<Player>().transform;
        ball = GameObject.FindWithTag(tagBall).transform;
    }
    void Update()
    {
        if (Input.GetButtonDown(focusInputAxis))
        {
            focusBall = !focusBall;
        }
        
        if(focusBall)
        {
            camBall();
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void camBall()
    {
        Vector3 direction = ball.position - transform.position;


        float distNormalized = 1 - (ball.position - transform.position).magnitude / 130.0f;


        float distance = Vector3.Dot(direction, transform.forward);


        if (distance >= 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, distNormalized * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
