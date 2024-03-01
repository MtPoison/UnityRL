using System.Collections;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private Ball ball;
    [SerializeField] private GameManager.PlayerEnum player;
    private ParticleSystem part;
    private void Start()
    {
        part = GetComponentInChildren<ParticleSystem>();
        part.Stop();
        ball = FindObjectOfType<Ball>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Ball"))
        {

            StartCoroutine(checkGoal());
        }
    }

    private IEnumerator checkGoal()
    {
        Debug.Log("vu");
        part.Play();
        for (float i = 1; i >= 0.2; i -= 0.1f)
        {
            ball.transform.localScale *= i;
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(2);
        FindObjectOfType<GameManager>().KickOff();
        Debug.Log("okk");
        ball.GetComponent<Rigidbody>().isKinematic = true;
        FindObjectOfType<GameManager>().Goalaso(player);
        ball.GetComponent<Rigidbody>().isKinematic = false;
        ball.Init();
        part.Stop();
    }
}
