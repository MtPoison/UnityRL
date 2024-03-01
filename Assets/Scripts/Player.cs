
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField, Range(0, 100)] private float intensityJump;
    [SerializeField] private float RotateSpeed;
    [SerializeField] private float intensityJetpack;

    [SerializeField] private string jumpInputAxis;
    [SerializeField] private string horizontalInputAxis;
    [SerializeField] private string verticalInputAxis;
    [SerializeField] public string sprintInputAxis;
    [SerializeField] public string jetpackInputAxis;
    [SerializeField] public string capacityInputAxis;
    [SerializeField] public int additionalForce;

    private Transform player;
    private Transform ball;

    public float rotationSpeed = 5f;
    private bool focusBall = false;

    private float horizontalPlayerGlobal;
    private float verticalPlayerGlobal;
    public float sprintPlayerGlobal;
    public float jetpackPlayerGlobal;
    public float capacityPlayerGlobal;

    private bool canJump;
    
    LineRenderer trail;
    Rigidbody rb;
    Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        trail = GetComponentInChildren<LineRenderer>();
        trail.enabled = false;

        player = GetComponent<Player>().transform;
        ball = GameObject.FindWithTag("Ball").transform;
    }

    private void Update()
    {
        GetAxisRawGlobal();

        if (Input.GetKeyDown(KeyCode.F))
        {
            focusBall = !focusBall;
        }

        if (focusBall)
        {
            Vector3 direction = ball.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        MovePlayers();

        if (Input.GetAxisRaw(jumpInputAxis) != 0 && canJump)
        {
            PlayerJump();
        }
    }

    public void SprintOn()
    {
        moveSpeed = 50.0f;
        cam.fieldOfView = 90.0f;
        trail.enabled = false;
    }

    public void SprintOff()
    {
        moveSpeed = 15.0f;
        cam.fieldOfView = 60.0f;
        trail.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        canJump = true;
        if(capacityPlayerGlobal != 0)
        {
            if (collision.collider.CompareTag("Ball"))
            {
                Vector3 vec = (collision.transform.position - transform.position).normalized;
                collision.rigidbody.AddForce(vec * additionalForce);
            }
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        canJump = false;
    }

    private void GetAxisRawGlobal()
    {
        horizontalPlayerGlobal = Input.GetAxisRaw(horizontalInputAxis);
        verticalPlayerGlobal = Input.GetAxisRaw(verticalInputAxis);
        sprintPlayerGlobal = Input.GetAxisRaw(sprintInputAxis);
        jetpackPlayerGlobal = Input.GetAxisRaw(jetpackInputAxis);
        capacityPlayerGlobal = Input.GetAxisRaw(capacityInputAxis);
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

    public void PlayerJetpack()
    {
        transform.Rotate(transform.up, RotateSpeed * horizontalPlayerGlobal);
        rb.AddForce(intensityJump * Time.deltaTime * intensityJetpack * Vector3.up);
    }
}