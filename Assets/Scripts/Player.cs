using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedJump;
    [SerializeField] private float RotateSpeed;

    private float horizontal;
    private float vertical;

    public Transform orientation;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 15.0f;
        }
        else
        {
            moveSpeed = 10.0f;
        }
    }

    private void FixedUpdate()
    {
        GetAxisRaw();
        MovePlayer();
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void GetAxisRaw()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * vertical + orientation.right * horizontal;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        transform.Rotate(transform.up, RotateSpeed * horizontal);
    }

    private void Jump()
    {
        rb.velocity = new Vector3(0, speedJump * Time.deltaTime, 0);
        transform.up = rb.velocity;
    }

    private void playerJump() { }
}