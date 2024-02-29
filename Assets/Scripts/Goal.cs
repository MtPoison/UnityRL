using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Goal : MonoBehaviour
{
    private Ball ball;
    [SerializeField] private GameManager.PlayerEnum player;
    ParticleSystem part;
    private void Start()
    {
        part = GetComponentInChildren<ParticleSystem>();
        part.Stop();
        ball = FindObjectOfType<Ball>();
    }
    private IEnumerator OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Ball"))
        {
            
            part.Play();

            ball.IsDestroyed();
            yield return new WaitForSeconds(2);
            FindObjectOfType<GameManager>().KickOff();
            FindObjectOfType<GameManager>().Goalaso(player);
            ball.Init();
            part.Stop();
        }
    }
}
