
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class InputPlayer
{
    public string focusInputAxis;
    public string jumpInputAxis;
    public string horizontalInputAxis;
    public string verticalInputAxis;
    public string sprintInputAxis;
    public string jetpackInputAxis;
    public string capacityInputAxis;
}

[System.Serializable]
public class CurveStamina
{
    public AnimationCurve curveJetpack;
    public AnimationCurve curveSprint;
}

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField, Range(0, 100)] private float intensityJump;
    [SerializeField] private float RotateSpeed;
    [SerializeField] private float intensityJetpack;
    [SerializeField] private int additionalForce;
    [SerializeField] private string tagBall;

    [SerializeField] private InputPlayer inputPlayer;
    [SerializeField] private CurveStamina curveStamina;
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

        if (Input.GetAxisRaw(inputPlayer.jumpInputAxis) != 0 && canJump)
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
        if (Input.GetAxisRaw(inputPlayer.jetpackInputAxis) != 0)
        {
            if (StaminaSlider.value > 0f)
            {
                Timer();
                PlayerJetpack();
                if (!isRemoveStaminaJetpack)
                {
                    StaminaSlider.value -= curveStamina.curveJetpack.Evaluate(Timer()) * Time.deltaTime;
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
        if (Input.GetAxisRaw(inputPlayer.sprintInputAxis) != 0)
        {
            if (StaminaSlider.value > 0.0f)
            {
                SprintOn();
                if (!isRemoveStamina)
                {
                    StaminaSlider.value -= curveStamina.curveSprint.Evaluate(Timer()) * Time.deltaTime;
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
        if (Input.GetAxisRaw(inputPlayer.jetpackInputAxis) == 0 && Input.GetAxisRaw(inputPlayer.sprintInputAxis) == 0)
        {
            timer = 0f;
            StaminaSlider.value += curveStamina.curveSprint.Evaluate(1) * Time.deltaTime;
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
        horizontalPlayerGlobal = Input.GetAxisRaw(inputPlayer.horizontalInputAxis);
        verticalPlayerGlobal = Input.GetAxisRaw(inputPlayer.verticalInputAxis);
        capacityPlayerGlobal = Input.GetAxisRaw(inputPlayer.capacityInputAxis);
    }

    private void MovePlayers()
    {
        rb.AddForce(transform.forward * moveSpeed * verticalPlayerGlobal);

        transform.Rotate(transform.up, RotateSpeed * horizontalPlayerGlobal);

    }

    private void PlayerJump()
    {
        if (StaminaSlider.value > 0f)
        {
            StaminaSlider.value -= 0.1f;
            rb.AddForce(intensityJump * Time.deltaTime * 1000 * Vector3.up);
        }
    }

    public void PlayerJetpack()
    {
        rb.AddForce(intensityJump * Time.deltaTime * intensityJetpack * Vector3.up);
    }
}