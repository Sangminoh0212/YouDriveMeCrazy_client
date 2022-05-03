using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowLane : MonoBehaviour
{

    [SerializeField] private float length = 1f;

    private bool isBtnTurnOn = false;

    private void Start() {
        if(length>0){
            this.transform.localScale = new Vector3(0.25f,0.01f,length);
        }
    }

    private void OnTriggerEnter(Collider other) {

            if(other.tag == "Car"){
            // 자동차 속도 천천히 줄이기
            // gameManager.gameover()
        }

    }

}
