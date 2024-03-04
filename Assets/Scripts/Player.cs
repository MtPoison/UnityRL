
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField, Range(0, 100)] private float intensityJump;
    [SerializeField] private float RotateSpeed;
    [SerializeField] private float intensityJetpack;

    [SerializeField] private string focusInputAxis;
    [SerializeField] private string jumpInputAxis;
    [SerializeField] private string horizontalInputAxis;
    [SerializeField] private string verticalInputAxis;
    [SerializeField] private string sprintInputAxis;
    [SerializeField] private string jetpackInputAxis;
    [SerializeField] private string capacityInputAxis;
    [SerializeField] private string tagBall;
    [SerializeField] private int additionalForce;

    private Transform player;
    private Transform ball;
    Animator animator;
    private int IsWalkingHash;

    private float rotationSpeed = 5f;
    private bool focusBall = false;

    private float horizontalPlayerGlobal;
    private float verticalPlayerGlobal;
    private float capacityPlayerGlobal;

    private bool canJump;

    private LineRenderer trail;
    private Rigidbody rb;
    private Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        trail = GetComponentInChildren<LineRenderer>();
        trail.enabled = false;

        player = GetComponent<Player>().transform;
        ball = GameObject.FindWithTag(tagBall).transform;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        GetAxisRawGlobal();

        if (Input.GetButtonDown(focusInputAxis))
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
        cam.fieldOfView = Mathf.Lerp(60 , 120, rb.velocity.magnitude / 50);
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
        capacityPlayerGlobal = Input.GetAxisRaw(capacityInputAxis);
    }

    private void MovePlayers()
    {
        rb.AddForce(transform.forward * moveSpeed * verticalPlayerGlobal);

        transform.Rotate(transform.up, RotateSpeed * horizontalPlayerGlobal);

        IsWalkingHash = Animator.StringToHash("isWalking");
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