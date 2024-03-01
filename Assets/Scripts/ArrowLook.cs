using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLook : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;
    public Transform LookAtTarget { get { return m_Target; } }

    [SerializeField]
    public Transform m_Spinner;
    public Transform Spinner { get { return m_Spinner; } }

    [SerializeField]
    private Transform m_Scaler;
    public Transform Scaler { get { return m_Scaler; } }

    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(LookAtTarget);
    }
}
