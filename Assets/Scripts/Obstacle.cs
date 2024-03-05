using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Obstacle : MonoBehaviour
{
    [SerializeField] Transform obstacle;
    List<Transform> obstacleList = new List<Transform>();
    Vector3 position;
    [SerializeField] int obstacleNB;
    [SerializeField] int minObstacleX;
    [SerializeField] int maxObstacleX;
    [SerializeField] int minObstacleZ;
    [SerializeField] int maxObstacleZ;
    [SerializeField] int distance;
    [SerializeField] int positionY;
    int test = 0;
    public void Generate()
    {
        position = new Vector3(10, positionY, 10);
        Transform item = Instantiate<Transform>(obstacle, position, Quaternion.identity);
        obstacleList.Add(item);

        for (int i = 0; i < obstacleNB; i++)
        {
            bool isValidPosition = false;

            for (int attempts = 0; attempts < 100; attempts++)
            {
                position = new Vector3(Random.Range(minObstacleX, maxObstacleX), positionY, Random.Range(minObstacleZ, maxObstacleZ));
                isValidPosition = true;

                foreach (Transform obstaclePosition in obstacleList)
                {
                    if (Vector3.Distance(position, obstaclePosition.position) < distance)
                    {
                        isValidPosition = false;
                        break;
                    }
                }
            }
            if (isValidPosition) 
            {
                item = Instantiate<Transform>(obstacle, position, Quaternion.identity);
                obstacleList.Add(item);
                
            }
            else if(!isValidPosition)
            {
                i--;
            }
        }
    }

}
