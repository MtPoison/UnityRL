using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedJump;
    [SerializeField] private float RotateSpeed;
    [SerializeField] private string horizontalInputAxis;
    [SerializeField] private string verticalInputAxis;

    private float horizontalPlayerGlobal;
    private float verticalPlayerGlobal;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetAxisRawGlobal();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 15.0f;
        }
        else
        {
            moveSpeed = 70.0f;
        }
    }

    private void FixedUpdate()
    {
        MovePlayers();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void GetAxisRawGlobal()
    {
        horizontalPlayerGlobal = Input.GetAxisRaw(horizontalInputAxis);
        verticalPlayerGlobal = Input.GetAxisRaw(verticalInputAxis);
    }

    private void MovePlayers()
    {
        rb.AddForce(transform.forward * moveSpeed * verticalPlayerGlobal);
        
        transform.Rotate(transform.up, RotateSpeed * horizontalPlayerGlobal);
    }

    private void Jump()
    {
        rb.velocity = new Vector3(0, speedJump * Time.deltaTime, 0);
        rb.velocity *= 5;
    }

    private void playerJump() { }
}