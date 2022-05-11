using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Obstacles { Empty, Chicken, Doe, Fox, Horse, Man, Bus, BlueCar, Van, Truck }

public class ObstacleSpawnManager : MonoBehaviour
{
    // All Obstacle objects
    public GameObject[] animalPrefabs;

    // The obstacle object to spawn in this area
    public Obstacles spawnObstacle;

    public float timer = 15;
    public float speed = 5;
    public Transform spawnPos;
    public Transform destPos;
    private bool isSpawned;

    private void Start()
    {
        if (spawnObstacle == Obstacles.Empty)
        {
            spawnObstacle = (Obstacles)Random.Range(1, System.Enum.GetValues(typeof(Obstacles)).Length);
        }
    }

    public void SpawnRandomAnimal()
    {
        // 재생성 방지
        if (!isSpawned)
        {
            isSpawned = true;
            GameObject g = Instantiate(animalPrefabs[(int)spawnObstacle - 1], spawnPos.position, Quaternion.identity);
            g.GetComponent<ObstacleMovementController>().setObstacle(timer, speed);
            g.transform.LookAt(destPos.position,transform.up);
        }
    }
}
