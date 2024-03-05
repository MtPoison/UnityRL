using UnityEngine;

public class DroneWeapon : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private int numDrones = 4;
    [SerializeField] private float droneSpeed;
    public float radius = 5f;

    private float[] angles;

    void Start()
    {
        angles = new float[numDrones];
        // permet de calculer l'angle de chaque drone en fonction de sa position autour de la balle
        float angleInterval = 360f / numDrones;

        // set la position de l'angle de chaque drone 
        for (int i = 0; i < numDrones; i++)
        {
            angles[i] = i * angleInterval;
        }
    }

    void Update()
    {
        for (int i = 0; i < numDrones; i++)
        {
            // set l'angle en fonction du speed et du temps
            angles[i] += droneSpeed * Time.deltaTime;

            // normalisé entre 0 et 360
            angles[i] %= 360;

            // calcule de position des bullets en fonction de l'angle et du radius
            float posX = ball.position.x + Mathf.Cos(Mathf.Deg2Rad * angles[i]) * radius;
            float posZ = ball.position.z + Mathf.Sin(Mathf.Deg2Rad * angles[i]) * radius;
            Vector3 targetPosition = new Vector3(posX, transform.position.y, posZ);

            // rotation et déplacement
            transform.RotateAround(ball.position, Vector3.up, droneSpeed * Time.deltaTime);
            transform.position = targetPosition;
        }
    }
}
