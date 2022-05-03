using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteLane_right : MonoBehaviour
{

    [SerializeField] private GameObject leftCollider;
    [HideInInspector] public bool isBtnTurnOn = false;

    private void OnTriggerEnter(Collider other)
    {
        print(leftCollider.GetComponent<WhiteLane_right>().isBtnTurnOn);
        if (leftCollider.GetComponent<WhiteLane_right>().isBtnTurnOn)
        {
            return;
        }
        else if (other.tag == "Car")
        {
            // 오른쪽 콜라이더 접근시 CarController의 leftBtn이 켜져있는지 꺼져있는지 판단함
            // leftBtn켜져있으면 본인 콜라이더, 왼쪽 콜라이더 모두 enabled = false;
            //if(CarController.leftBtn == true){
            if (true)
            {
                //this.GetComponent<Collider>().enabled = false;
                //leftCollider.GetComponent<Collider>().enabled = false;
                this.isBtnTurnOn = true;
            }
        }
        else
        {
            // 자동차 속도 천천히 줄이기
            // gameManager.gameover()
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //왼쪽 콜라이더 탈출 && 오른쪽 콜라이더.isBtnTurnOn==true = 오른쪽 콜라이더를 통과하고 왼쪽으로 나간것으로 판단
        if (other.tag == "Car" && leftCollider.GetComponent<WhiteLane_right>().isBtnTurnOn)
        {
            this.GetComponent<Collider>().enabled = true;
            leftCollider.GetComponent<Collider>().enabled = true;

            this.isBtnTurnOn = false;
            leftCollider.GetComponent<WhiteLane_right>().isBtnTurnOn = false;
        }
    }
}
