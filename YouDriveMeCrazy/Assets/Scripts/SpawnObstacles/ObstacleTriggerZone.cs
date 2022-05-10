using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTriggerZone : MonoBehaviour
{
    private ObstacleSpawnManager obstacleSpawnManager;

    private void Start()
    {
        obstacleSpawnManager = this.GetComponentInParent<ObstacleSpawnManager>();  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            obstacleSpawnManager.SpawnRandomAnimal();
        }
    }
}
