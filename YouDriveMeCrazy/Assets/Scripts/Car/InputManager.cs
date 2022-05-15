using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InputManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public KeyCode brakeBtn, leftTurnBtn,  leftTurnSignalBtn, rightTurnSignalBtn, klaxonBtn1;
    public KeyCode accelBtn, rightTurnBtn, gotoLeftWiperBtn, gotoRightWiperBtn, klaxonBtn2;

    // Player1
    [HideInInspector] public bool isBreakPressing;
    [HideInInspector] public bool isLeftTurnPressing;
    [HideInInspector] public bool isLeftTurnSignalPressing;
    [HideInInspector] public bool isRightTurnSignalPressing;
    [HideInInspector] public bool isKlaxon1Pressing;

    //Player2
    [HideInInspector] public bool isAccelPressing;
    [HideInInspector] public bool isRightTurnPressing;
    [HideInInspector] public bool isGotoLeftWiperPressing;
    [HideInInspector] public bool isGotoRightWiperPressing;
    [HideInInspector] public bool isKlaxon2Pressing;


    // Update is called once per frame
    void Update()
    {
        // Player 1
        if (PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKeyDown(brakeBtn)) { isBreakPressing = true; }
            else if (Input.GetKeyUp(brakeBtn)) { isBreakPressing = false; }

            if (Input.GetKeyDown(leftTurnBtn)) { isLeftTurnPressing = true; }
            else if (Input.GetKeyUp(leftTurnBtn)) { isLeftTurnPressing = false; }

            if (Input.GetKeyDown(leftTurnSignalBtn)) { isLeftTurnSignalPressing = true; }
            else if (Input.GetKeyUp(leftTurnSignalBtn)) { isLeftTurnSignalPressing = false; }

            if (Input.GetKeyDown(rightTurnSignalBtn)) { isRightTurnSignalPressing = true; }
            else if (Input.GetKeyUp(rightTurnSignalBtn)) { isRightTurnSignalPressing = false; }

            if (Input.GetKeyDown(klaxonBtn1)) { isKlaxon1Pressing = true; }
            else if (Input.GetKeyUp(klaxonBtn1)) { isKlaxon1Pressing = false; }


            #region Cheat mode
            if (Cheat.cheatMode) {
                if (Input.GetKeyDown(accelBtn)) { isAccelPressing = true; }
                else if (Input.GetKeyUp(accelBtn)) { isAccelPressing = false; }

                if (Input.GetKeyDown(rightTurnBtn)) { isRightTurnPressing = true; }
                else if (Input.GetKeyUp(rightTurnBtn)) { isRightTurnPressing = false; }

                if (Input.GetKeyDown(KeyCode.Q)) { isGotoLeftWiperPressing = true; }
                else if (Input.GetKeyUp(KeyCode.Q)) { isGotoLeftWiperPressing = false; }

                if (Input.GetKeyDown(KeyCode.E)) { isGotoRightWiperPressing = true; }
                else if (Input.GetKeyUp(KeyCode.E)) { isGotoRightWiperPressing = false; }

                if (Input.GetKeyDown(klaxonBtn2)) { isKlaxon2Pressing = true; }
                else if (Input.GetKeyUp(klaxonBtn2)) { isKlaxon2Pressing = false; }

                CarController.carController.isAccelPressing = this.isAccelPressing;
                CarController.carController.isRightTurnPressing = this.isRightTurnPressing;
                CarController.carController.isGotoLeftWiperPressing = this.isGotoLeftWiperPressing;
                CarController.carController.isGotoRightWiperPressing = this.isGotoRightWiperPressing;
                CarController.carController.isKlaxon2Pressing = this.isKlaxon2Pressing;
                CarController.carController.isBreakPressing = this.isBreakPressing;
                CarController.carController.isLeftTurnPressing = this.isLeftTurnPressing;
                CarController.carController.isLeftTurnSignalPressing = this.isLeftTurnSignalPressing;
                CarController.carController.isRightTurnSignalPressing = this.isRightTurnSignalPressing;
                CarController.carController.isKlaxon1Pressing = this.isKlaxon1Pressing;
            }
            #endregion
        }

        //Player 2 
        if (!PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKeyDown(accelBtn)) {  isAccelPressing = true; }
            else if(Input.GetKeyUp(accelBtn)) {  isAccelPressing = false; }

            if (Input.GetKeyDown(rightTurnBtn)) {  isRightTurnPressing = true; }
            else if(Input.GetKeyUp(rightTurnBtn)){  isRightTurnPressing = false; }

            if (Input.GetKeyDown(gotoLeftWiperBtn)) {  isGotoLeftWiperPressing = true; }
            else if(Input.GetKeyUp(gotoLeftWiperBtn)) {  isGotoLeftWiperPressing = false; }

            if (Input.GetKeyDown(gotoRightWiperBtn)) {  isGotoRightWiperPressing = true; }
            else if(Input.GetKeyUp(gotoRightWiperBtn)) {  isGotoRightWiperPressing = false; }

            if (Input.GetKeyDown(klaxonBtn2)) {  isKlaxon2Pressing = true; }
            else if(Input.GetKeyUp(klaxonBtn2)) {  isKlaxon2Pressing = false; }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (stream.IsReading)
            {
                this.isAccelPressing = (bool)stream.ReceiveNext();
                this.isRightTurnPressing = (bool)stream.ReceiveNext();
                this.isGotoLeftWiperPressing = (bool)stream.ReceiveNext();
                this.isGotoRightWiperPressing = (bool)stream.ReceiveNext();
                this.isKlaxon2Pressing = (bool)stream.ReceiveNext();

                CarController.carController.isAccelPressing = this.isAccelPressing;
                CarController.carController.isRightTurnPressing = this.isRightTurnPressing;
                CarController.carController.isGotoLeftWiperPressing = this.isGotoLeftWiperPressing;
                CarController.carController.isGotoRightWiperPressing = this.isGotoRightWiperPressing;
                CarController.carController.isKlaxon2Pressing = this.isKlaxon2Pressing;
            }

            if (stream.IsWriting)
            {
                stream.SendNext(this.isBreakPressing);
                stream.SendNext(this.isLeftTurnPressing);
                stream.SendNext(this.isLeftTurnSignalPressing);
                stream.SendNext(this.isRightTurnSignalPressing);
                stream.SendNext(this.isKlaxon1Pressing);

                CarController.carController.isBreakPressing = this.isBreakPressing;
                CarController.carController.isLeftTurnPressing = this.isLeftTurnPressing;
                CarController.carController.isLeftTurnSignalPressing = this.isLeftTurnSignalPressing;
                CarController.carController.isRightTurnSignalPressing = this.isRightTurnSignalPressing;
                CarController.carController.isKlaxon1Pressing = this.isKlaxon1Pressing;
            }
        }

        if (!PhotonNetwork.IsMasterClient)
        {
            if (stream.IsReading)
            {
                this.isBreakPressing = (bool)stream.ReceiveNext();
                this.isLeftTurnPressing = (bool)stream.ReceiveNext();
                this.isLeftTurnSignalPressing = (bool)stream.ReceiveNext();
                this.isRightTurnSignalPressing = (bool)stream.ReceiveNext();
                this.isKlaxon1Pressing = (bool)stream.ReceiveNext();

                CarController.carController.isBreakPressing = this.isBreakPressing;
                CarController.carController.isLeftTurnPressing = this.isLeftTurnPressing;
                CarController.carController.isLeftTurnSignalPressing = this.isLeftTurnSignalPressing;
                CarController.carController.isRightTurnSignalPressing = this.isRightTurnSignalPressing;
                CarController.carController.isKlaxon1Pressing = this.isKlaxon1Pressing;
            }

            if (stream.IsWriting)
            {
                stream.SendNext(this.isAccelPressing);
                stream.SendNext(this.isRightTurnPressing);
                stream.SendNext(this.isGotoLeftWiperPressing);
                stream.SendNext(this.isGotoRightWiperPressing);
                stream.SendNext(this.isKlaxon2Pressing);

                CarController.carController.isAccelPressing = this.isAccelPressing;
                CarController.carController.isRightTurnPressing = this.isRightTurnPressing;
                CarController.carController.isGotoLeftWiperPressing = this.isGotoLeftWiperPressing;
                CarController.carController.isGotoRightWiperPressing = this.isGotoRightWiperPressing;
                CarController.carController.isKlaxon2Pressing = this.isKlaxon2Pressing;
            }
        }
    }
}
