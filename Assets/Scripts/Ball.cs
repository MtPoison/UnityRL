using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    public float forceMultiplier = 10f;
    public float upwardForce = 5f;
    private Vector3 initialposition;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialposition = transform.position;
    }

    // Update is called once per frame
    public void ResetBall()
    {
        transform.position = initialposition;
        rb.velocity = Vector3.zero;
        
    }


}
