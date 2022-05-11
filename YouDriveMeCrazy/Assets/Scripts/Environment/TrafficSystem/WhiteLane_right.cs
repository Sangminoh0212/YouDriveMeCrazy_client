using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteLane_right : MonoBehaviour
{

    [SerializeField ] private GameObject leftCollider;
    [HideInInspector] public bool isBtnTurnOn = false;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car"){
            if (CarController.carController.isLeftTurnSignalPressing)
            {
                this.isBtnTurnOn = true;
                print("오른쪽에서 진입");
            }
            else if (leftCollider.GetComponent<WhiteLane_left>().isBtnTurnOn)
            {
                print("왼쪽에서 오른쪽 깜빡이 키고 진입한거라 괜찮다.");
                return;
            }
            else
            {
                // by 상민, 자동차 속도 천천히 줄이기 필요
                GameManager.Instance.GameOver();
                this.GetComponent<Collider>().enabled = false;
                leftCollider.GetComponent<Collider>().enabled = false;
                //print("오른쪽 콜라이더에서 신호위반 걸림. 게임 오버");
            }
        }
    }

    // by 상민, 왼쪽 콜라이더 탈출 && 오른쪽 콜라이더.isBtnTurnOn==true = 오른쪽 콜라이더를 통과하고 왼쪽으로 나간것으로 판단
    private void OnTriggerExit(Collider other)
    {
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
