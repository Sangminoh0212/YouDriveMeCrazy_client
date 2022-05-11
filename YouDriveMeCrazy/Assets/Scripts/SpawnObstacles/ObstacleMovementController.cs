using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovementController : MonoBehaviour
{
    private float speed = 3;
    private float timer = 100000;
    private float passedTime;


    public void setObstacle(float timer, float speed)
    {
        this.timer = timer;
        this.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;
        if (passedTime > timer)
        {
            Destroy(gameObject);
        }
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Car"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
