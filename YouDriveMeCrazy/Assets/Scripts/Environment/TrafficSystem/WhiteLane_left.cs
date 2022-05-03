using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteLane_left : MonoBehaviour
{

    [SerializeField ] public GameObject rightCollider;
    [HideInInspector] public bool isBtnTurnOn = false;

    private void OnTriggerEnter(Collider other) {
        
        if (rightCollider.GetComponent<WhiteLane_right>().isBtnTurnOn)
        {
            return;
        }
        else if (other.tag=="Car")
        {
            // 왼쪽 콜라이더 접근시 CarController의 Btn이 켜져있는지 꺼져있는지 판단함
            // rightBtn켜져있으면 본인 콜라이더, 오른쪽 콜라이더 모두 enabled = false;
            //if(CarController.rightBtn == true){
            if(true){
                //this.GetComponent<Collider>().enabled = false;
                //rightCollider.GetComponent<Collider>().enabled= false;
                this.isBtnTurnOn = true;
            }
        }
        else
        {
            // 자동차 속도 천천히 줄이기
            // gameManager.gameover()
        }
    }

    private void OnTriggerExit(Collider other) {
        //왼쪽 콜라이더 탈출 && 오른쪽 콜라이더.isBtnTurnOn==true = 오른쪽 콜라이더를 통과하고 왼쪽으로 나간것으로 판단
        if (other.tag=="Car" && rightCollider.GetComponent<WhiteLane_right>().isBtnTurnOn){
            this.GetComponent<Collider>().enabled = true;
            rightCollider.GetComponent<Collider>().enabled = true;
            
            this.isBtnTurnOn = false;
            rightCollider.GetComponent<WhiteLane_right>().isBtnTurnOn = false;
        }  
    }
}