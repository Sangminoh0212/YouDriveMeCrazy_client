using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InputManager : MonoBehaviour
{
    public KeyCode brakeBtn, leftTurnBtn,  leftTurnSignalBtn, rightTurnSignalBtn, klaxonBtn1;
    public KeyCode accelBtn, rightTurnBtn, gotoLeftWiperBtn, gotoRightWiperBtn, klaxonBtn2;


    // Update is called once per frame
    void Update()
    {
        // Player 1
        if (PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKey(brakeBtn)) { CarController.carController.isBreakPressing = true; }
            else { CarController.carController.isBreakPressing = false; }

            if (Input.GetKey(leftTurnBtn)) { CarController.carController.isLeftTurnPressing = true; }
            else { CarController.carController.isLeftTurnPressing = false; }

            if (Input.GetKey(leftTurnSignalBtn)) { CarController.carController.isLeftTurnSignalPressing = true; }
            else { CarController.carController.isLeftTurnSignalPressing = false; }

            if (Input.GetKey(rightTurnSignalBtn)) { CarController.carController.isRightTurnSignalPressing = true; }
            else { CarController.carController.isRightTurnSignalPressing = false; }

            if (Input.GetKey(klaxonBtn1)) { CarController.carController.isKlaxon1Pressing = true; }
            else { CarController.carController.isKlaxon1Pressing = false; }
        }

        //Player 2 
        if (!PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKey(accelBtn)) { CarController.carController.isAccelPressing = true; }
            else { CarController.carController.isAccelPressing = false; }

            if (Input.GetKey(rightTurnBtn)) { CarController.carController.isRightTurnPressing = true; }
            else{ CarController.carController.isRightTurnPressing = false; }

            if (Input.GetKey(gotoLeftWiperBtn)) { CarController.carController.isGotoLeftWiperPressing = true; }
            else { CarController.carController.isGotoLeftWiperPressing = false; }

            if (Input.GetKey(gotoLeftWiperBtn)) { CarController.carController.isGotoRightWiperPressing = true; }
            else { CarController.carController.isGotoRightWiperPressing = false; }

            if (Input.GetKey(klaxonBtn2)) { CarController.carController.isKlaxon2Pressing = true; }
            else { CarController.carController.isKlaxon2Pressing = false; }
        }
    }
}
