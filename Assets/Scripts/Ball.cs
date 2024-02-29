using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    TrailRenderer trail;
    [SerializeField] private float forceMultiplier = 10f;
    [SerializeField] private float upwardForce = 5f;
    [SerializeField] int scale;
    private Vector3 initialposition;

    private Rigidbody rb;

    public float speed = 1.0f;
    private float hue;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialposition = transform.position;


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


        trail.enabled = false;

        hue = 0.0f;

    }
    void FixedUpdate()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            trail.enabled = true;
            trail.startWidth = rb.velocity.magnitude / 3.0f;
            trail.endWidth = trail.startWidth;
        }
        else
        {
            trail.enabled = false;
        }
    }

    private void Update()
    {
        hue += Time.deltaTime * speed;
        hue = Mathf.Repeat(hue, 1.0f);

        Color color = Color.HSVToRGB(hue, 1.0f, 1.0f);

        trail.startColor = color;
        trail.endColor = color;
    }


    public void ResetBall()
    {
        transform.position = initialposition;
        rb.velocity = Vector3.zero;
        trail.enabled = false;
    }
}
