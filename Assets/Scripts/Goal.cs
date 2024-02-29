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
<<<<<<< Updated upstream
            other.gameObject.GetComponent<Ball>().ResetBall();

=======
            for (float i = 1; i >= 0.2 ; i -= 0.1f)
            {
                ball.transform.localScale *= i;
                yield return new WaitForSeconds(.1f);
            }
>>>>>>> Stashed changes
            yield return new WaitForSeconds(2);

            part.Stop();
        }
    }
}
