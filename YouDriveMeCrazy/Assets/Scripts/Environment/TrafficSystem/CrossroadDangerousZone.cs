using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossroadDangerousZone : MonoBehaviour
{
    void Start()
    {
        Vector3 referencePoint = new Vector3(0.0f,5f,0.0f);
        gameObject.GetComponent<RectTransform>().anchorMin = referencePoint;
        gameObject.GetComponent<RectTransform>().anchorMax = referencePoint;
        gameObject.GetComponent<RectTransform>().pivot = referencePoint;
        
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
