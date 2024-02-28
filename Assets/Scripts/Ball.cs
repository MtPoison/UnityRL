using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    LineRenderer trail;
    public float forceMultiplier = 10f;
    public float upwardForce = 5f;
    private Vector3 initialposition;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialposition = transform.position;

        trail = GetComponentInChildren<LineRenderer>();
        trail.enabled = false;

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        trail = GetComponentInChildren<LineRenderer>();

        Init();
    }

    public void Init()
    {
        rb.velocity = Vector3.zero;
        trail.enabled = false;
    }
    void FixedUpdate()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            trail.enabled = true;
            trail.startWidth = rb.velocity.magnitude / 10f;
            trail.endWidth = trail.startWidth;
        }
        else
        {
            trail.enabled = false;
        }
    }


    public void ResetBall()
    {
        transform.position = initialposition;
        rb.velocity = Vector3.zero;
        trail.enabled = false;
    }
}
