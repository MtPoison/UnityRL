using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    LineRenderer trail;
    public float forceMultiplier = 10f;
    public float upwardForce = 5f;
    [SerializeField] int scale;
    private Vector3 initialposition;
   

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialposition = transform.position;

<<<<<<< Updated upstream
        trail = GetComponentInChildren<LineRenderer>();
=======
        trail = GetComponentInChildren<TrailRenderer>();
        trail.enabled = false;

        hue = 0.0f;

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        trail = GetComponentInChildren<TrailRenderer>();

        Init();
    }

    public void Init()
    {
        transform.localScale = new Vector3(scale, scale, scale);
        rb.velocity = Vector3.zero;

>>>>>>> Stashed changes
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
