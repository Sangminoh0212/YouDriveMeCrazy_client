using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossroadDangerousZone : MonoBehaviour
{
    void Start()
    {    
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            // 게임오버
            // gameManager.gameover()
        }
    }
}
