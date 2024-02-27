using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Goal : MonoBehaviour
{
    ParticleSystem part;
    private void Start()
    {
        part = GetComponentInChildren<ParticleSystem>();
        part.Stop();
    }
    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            part.Play();
            other.gameObject.GetComponent<Ball>().ResetBall();

            yield return new WaitForSeconds(2);

            part.Stop();
        }
    }
}
