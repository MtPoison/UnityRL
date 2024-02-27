using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField, Range(0, 100)] private float intensityJump;
    [SerializeField] private float RotateSpeed;
    [SerializeField] private string horizontalInputAxis;
    [SerializeField] private string verticalInputAxis;

    private float horizontalPlayerGlobal;
    private float verticalPlayerGlobal;
    LineRenderer trail;

    Rigidbody rb;
    Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        trail = GetComponentInChildren<LineRenderer>();
        trail.enabled = false;
    }

    private void Update()
    {
        GetAxisRawGlobal();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 50.0f;
            cam.fieldOfView = 90.0f;
            trail.enabled = true;
        }
        else
        {
            moveSpeed = 15.0f;
            cam.fieldOfView = 60.0f;
            trail.enabled = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            PlayerJump();
        }

    }

    private void FixedUpdate()
    {
        MovePlayers();
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

    private void PlayerJump()
    {
        rb.AddForce(intensityJump * Time.deltaTime * 1000 * Vector3.up);
    }
}