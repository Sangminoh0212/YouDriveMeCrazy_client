using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearZone : MonoBehaviour
{
    void Start()
    {    
        //gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            GameManager.Instance.StageClear();
            this.gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
