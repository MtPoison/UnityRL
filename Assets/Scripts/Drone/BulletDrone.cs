using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDrone : MonoBehaviour
{
    [SerializeField] public Transform ball;
    [SerializeField] public ModeNaviguation modeNaviguation;
    [SerializeField] public Rigidbody modeNaviguationRigidbody;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float prushForce;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (modeNaviguation.GetActiveTirBullet())
        {
            if (Vector3.Distance(transform.position, ball.position) <= modeNaviguation.GetFinishDistanceTarget())
            {
                transform.position = ball.position;
                modeNaviguationRigidbody.AddForce(prushForce * Time.deltaTime * 1000 * Vector3.forward);
                Debug.Log("arrivé");
                Destroy(modeNaviguation.destroyPrefab);
                modeNaviguation.SetActiveTirBullet(false);
            }
            else
            {
                // déplace la bullet vers la balle
                Vector3 direction = (ball.position - transform.position).normalized;
                transform.Translate(direction * Time.deltaTime * bulletSpeed, Space.World);
                transform.LookAt(ball);
            }
        }
        else
        {
            Vector3 direction = (modeNaviguation.transform.position - transform.position).normalized;
            transform.Translate(direction * Time.deltaTime * bulletSpeed, Space.World);
        }
    }
}
