using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowLane : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {

            if(other.tag == "Car"){
            // 자동차 속도 천천히 줄이기
            // gameManager.gameover()
            print("노란선 접근. 게임 오버");
            this.GetComponent<Collider>().enabled = false;
        }

    }

}
