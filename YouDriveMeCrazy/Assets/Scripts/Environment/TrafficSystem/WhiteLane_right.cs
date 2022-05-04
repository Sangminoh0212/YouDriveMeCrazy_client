using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteLane_right : MonoBehaviour
{

    [SerializeField ] private GameObject leftCollider;
    [HideInInspector] public bool isBtnTurnOn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ClickMovement>().leftBtn)
        {
            //this.GetComponent<Collider>().enabled = false;
            //rightCollider.GetComponent<Collider>().enabled= false;
            this.isBtnTurnOn = true;
            print("오른쪽에서 진입");
        }
        else if (leftCollider.GetComponent<WhiteLane_left>().isBtnTurnOn)
        {
            print("왼쪽에서 오른쪽 깜빡이 키고 진입한거라 ㄱㅊ");
            return;
        }
        else
        {
            // 자동차 속도 천천히 줄이기
            // gameManager.gameover()
            print("오른쪽 콜라이더에서 신호위반 걸림. 게임 오버");
            this.GetComponent<Collider>().enabled = false;
            leftCollider.GetComponent<Collider>().enabled = false;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //왼쪽 콜라이더 탈출 && 오른쪽 콜라이더.isBtnTurnOn==true = 오른쪽 콜라이더를 통과하고 왼쪽으로 나간것으로 판단
        if (other.tag == "Car" && leftCollider.GetComponent<WhiteLane_left>().isBtnTurnOn)
        {
            this.GetComponent<Collider>().enabled = true;
            leftCollider.GetComponent<Collider>().enabled = true;

            this.isBtnTurnOn = false;
            leftCollider.GetComponent<WhiteLane_left>().isBtnTurnOn = false;
            print("오른쪽으로 탈출");
        }
    }
}
