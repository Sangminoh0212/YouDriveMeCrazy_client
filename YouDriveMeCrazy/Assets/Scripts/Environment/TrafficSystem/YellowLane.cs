using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowLane : MonoBehaviour
{

    private void Start()
    {
        // by 상민, 노란선은 투명하게 안할거면 이 부분 지우면 됩니다.
        //gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Car"){
            // by 상민, 자동차 속도 천천히 줄이기 필요
            GameManager.Instance.GameOver();
            this.GetComponent<Collider>().enabled = false;
            // print("노란선 접근. 게임 오버") ;
        }

    }

}
