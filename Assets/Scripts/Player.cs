
using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField] private AnimationCurve curveJetpack;
    [SerializeField] private AnimationCurve curveSprint;
    [SerializeField] private Slider StaminaSlider;

    private float horizontalPlayerGlobal;
    private float verticalPlayerGlobal;
    private float capacityPlayerGlobal;
    private float timer;
    private float timerMax = 3f;

    private bool canJump;
    private bool isRemoveStamina = false;
    private bool isRemoveStaminaJetpack = false;

    private LineRenderer trail;
    private Rigidbody rb;
    private Camera cam;

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

        // Stamina partie - Jetpack & Sprint //
        ManageJetpackSlider();
        ManageSprintSlider();
        AddStamina();
    }

    private void FixedUpdate()
    {
        MovePlayers();

        if (Input.GetAxisRaw(jumpInputAxis) != 0 && canJump)
        {
            PlayerJump();
        }
    }

    private float Timer()
    {
        timer += Time.deltaTime;

        if (timer > timerMax)
        {
            timer = timerMax;
        }
        float timerRatio = timer / timerMax;

        return timerRatio;
    }

    private void ManageJetpackSlider()
    {
        if (Input.GetAxisRaw(jetpackInputAxis) != 0)
        {
            if (StaminaSlider.value > 0f)
            {
                Timer();
                PlayerJetpack();
                if (!isRemoveStaminaJetpack)
                {
                    StaminaSlider.value -= curveJetpack.Evaluate(Timer()) * Time.deltaTime;
                }
            }
        }
        else
        {
            isRemoveStaminaJetpack = false;
        }
    }

    private void ManageSprintSlider()
    {
        if (Input.GetAxisRaw(sprintInputAxis) != 0)
        {
            if (StaminaSlider.value > 0.0f)
            {
                SprintOn();
                if (!isRemoveStamina)
                {
                    StaminaSlider.value -= curveSprint.Evaluate(Timer()) * Time.deltaTime;
                }
            }
        }
        else
        {
            SprintOff();
            isRemoveStamina = false;
        }
    }

    private void AddStamina()
    {
        if (Input.GetAxisRaw(jetpackInputAxis) == 0 && Input.GetAxisRaw(sprintInputAxis) == 0)
        {
            timer = 0f;
            StaminaSlider.value += curveSprint.Evaluate(1) * Time.deltaTime;
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

    }

    private void PlayerJump()
    {
        rb.AddForce(intensityJump * Time.deltaTime * 1000 * Vector3.up);
    }

    public void PlayerJetpack()
    {
        rb.AddForce(intensityJump * Time.deltaTime * intensityJetpack * Vector3.up);
    }
}