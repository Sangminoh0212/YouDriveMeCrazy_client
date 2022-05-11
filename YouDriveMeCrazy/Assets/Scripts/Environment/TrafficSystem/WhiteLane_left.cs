using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteLane_left : MonoBehaviour
{

    [SerializeField ] private GameObject rightCollider;
    [HideInInspector] public bool isBtnTurnOn = false;

    private void Start() {
        //transform.parent.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }


    // by 상민, 왼쪽 콜라이더 접근시 CarController의 Btn이 켜져있는지 꺼져있는지 판단함
    // rightBtn켜져있으면 본인 콜라이더, 오른쪽 콜라이더 모두 enabled = false;
    private void OnTriggerEnter(Collider other) {
        if (other.tag=="Car")
        {
            if(CarController.carController.isRightTurnSignalPressing){
                this.isBtnTurnOn = true;
                print("왼쪽에서 진입");
            }
            else if (rightCollider.GetComponent<WhiteLane_right>().isBtnTurnOn)
            {
                print("오른쪽에서 왼쪽 깜빡이 키고 진입한거라 괜찮다.");
                return;
            }
            else{
                // by 상민, 자동차 속도 천천히 줄이기 필요
                GameManager.Instance.GameOver();
                this.GetComponent<Collider>().enabled = false;
                rightCollider.GetComponent<Collider>().enabled = false;
                //print("왼쪽 콜라이더에서 신호위반 걸림. 게임 오버");
            }
        }
    }

    // by 상민, 왼쪽 콜라이더 탈출 && 오른쪽 콜라이더.isBtnTurnOn==true = 오른쪽 콜라이더를 통과하고 왼쪽으로 나간것으로 판단
    private void OnTriggerExit(Collider other) {
        if (other.tag=="Car" && rightCollider.GetComponent<WhiteLane_right>().isBtnTurnOn){
            this.GetComponent<Collider>().enabled = true;
            rightCollider.GetComponent<Collider>().enabled = true;
            
            this.isBtnTurnOn = false;
            rightCollider.GetComponent<WhiteLane_right>().isBtnTurnOn = false;
            print("왼쪽으로 탈출");
        }  
    }
}
