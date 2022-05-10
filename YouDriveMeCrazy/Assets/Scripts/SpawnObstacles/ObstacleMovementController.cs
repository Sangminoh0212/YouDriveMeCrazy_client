using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovementController : MonoBehaviour
{
    private float speed = 10;
    private float timer;
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
            // 나중에 충돌 효과 추가?
            StartCoroutine(GameManager.Instance.GameOver());
        }
    }
}
