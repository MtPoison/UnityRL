using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLook : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;
    private Transform LookAtTarget { get { return m_Target; } }

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
