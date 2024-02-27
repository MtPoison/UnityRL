using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField, Range(0, 200)] private float intensityJump;
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
            moveSpeed = 50.0f;
        }
        else
        {
            moveSpeed = 15.0f;
        }

        if (Input.GetButtonDown("Jump"))
        {
            PlayerJump();
        }
/*        if (rb.velocity.y < 1)
        {
            Debug.Log("t'es au sol");
        }else
        {
            Debug.Log("t'es en l'air");
        }*/
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