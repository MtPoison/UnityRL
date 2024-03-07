using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLook : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;
    private Transform LookAtTarget { get { return m_Target; } }

    void Update()
    {
        transform.LookAt(LookAtTarget);
    }
}
