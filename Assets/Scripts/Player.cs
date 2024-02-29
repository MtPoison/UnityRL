using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

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

    private float horizontalPlayerGlobal;
    private float verticalPlayerGlobal;
    public float sprintPlayerGlobal;
    public float jetpackPlayerGlobal;

    private bool canJump;
    
    LineRenderer trail;
    Rigidbody rb;
    Camera cam;
    SliderManager sliderManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        trail = GetComponentInChildren<LineRenderer>();
        sliderManager = GetComponentInChildren<SliderManager>();
        trail.enabled = false;
    }

    private void Update()
    {
        GetAxisRawGlobal();
    }

    private void FixedUpdate()
    {
        MovePlayers();

        if (Input.GetAxisRaw(jumpInputAxis) != 0 && canJump)
        {
            PlayerJump();
        }

/*        if (sliderManager != null)
        {
            if (Input.GetAxisRaw(jetpackInputAxis) != 0 && sliderManager.GetSlidersValue("JetpackJauge") > 0.0f)
            {
                PlayerJetpack();
            }   
        }*/

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