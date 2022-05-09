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
        if (!PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKey(brakeBtn)) {  isBreakPressing = true; }
            else {  isBreakPressing = false; }

            if (Input.GetKey(leftTurnBtn)) {  isLeftTurnPressing = true; }
            else {  isLeftTurnPressing = false; }

            if (Input.GetKey(leftTurnSignalBtn)) {  isLeftTurnSignalPressing = true; }
            else {  isLeftTurnSignalPressing = false; }

            if (Input.GetKey(rightTurnSignalBtn)) {  isRightTurnSignalPressing = true; }
            else {  isRightTurnSignalPressing = false; }

            if (Input.GetKey(klaxonBtn1)) {  isKlaxon1Pressing = true; }
            else {  isKlaxon1Pressing = false; }
        }

        //Player 2 
        if (PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKey(accelBtn)) {  isAccelPressing = true; }
            else {  isAccelPressing = false; }

            if (Input.GetKey(rightTurnBtn)) {  isRightTurnPressing = true; }
            else{  isRightTurnPressing = false; }

            if (Input.GetKey(gotoLeftWiperBtn)) {  isGotoLeftWiperPressing = true; }
            else {  isGotoLeftWiperPressing = false; }

            if (Input.GetKey(gotoRightWiperBtn)) {  isGotoRightWiperPressing = true; }
            else {  isGotoRightWiperPressing = false; }

            if (Input.GetKey(klaxonBtn2)) {  isKlaxon2Pressing = true; }
            else {  isKlaxon2Pressing = false; }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            if (stream.IsReading)
            {
                this.isAccelPressing = (bool)stream.ReceiveNext();
                this.isRightTurnPressing = (bool)stream.ReceiveNext();
                this.isGotoLeftWiperPressing = (bool)stream.ReceiveNext();
                this.isGotoRightWiperPressing = (bool)stream.ReceiveNext();
                this.isKlaxon2Pressing = (bool)stream.ReceiveNext();
            }

            if (stream.IsWriting)
            {
                stream.SendNext(this.isBreakPressing);
                stream.SendNext(this.isLeftTurnPressing);
                stream.SendNext(this.isLeftTurnSignalPressing);
                stream.SendNext(this.isRightTurnSignalPressing);
                stream.SendNext(this.isKlaxon1Pressing);
            }
        }

        if (PhotonNetwork.IsMasterClient)
        {
            if (stream.IsReading)
            {
                this.isBreakPressing = (bool)stream.ReceiveNext();
                this.isLeftTurnPressing = (bool)stream.ReceiveNext();
                this.isLeftTurnSignalPressing = (bool)stream.ReceiveNext();
                this.isRightTurnSignalPressing = (bool)stream.ReceiveNext();
                this.isKlaxon1Pressing = (bool)stream.ReceiveNext();
            }

            if (stream.IsWriting)
            {
                stream.SendNext(this.isAccelPressing);
                stream.SendNext(this.isRightTurnPressing);
                stream.SendNext(this.isGotoLeftWiperPressing);
                stream.SendNext(this.isGotoRightWiperPressing);
                stream.SendNext(this.isKlaxon2Pressing);
            }
        }

        CarController.carController.isBreakPressing = this.isBreakPressing;
        CarController.carController.isLeftTurnPressing = this.isLeftTurnPressing;
        CarController.carController.isLeftTurnSignalPressing = this.isLeftTurnSignalPressing;
        CarController.carController.isRightTurnSignalPressing = this.isRightTurnSignalPressing;
        CarController.carController.isKlaxon1Pressing = this.isKlaxon1Pressing;

        CarController.carController.isAccelPressing = this.isAccelPressing;
        CarController.carController.isRightTurnPressing = this.isRightTurnPressing;
        CarController.carController.isGotoLeftWiperPressing = this.isGotoLeftWiperPressing;
        CarController.carController.isGotoRightWiperPressing = this.isGotoRightWiperPressing;
        CarController.carController.isKlaxon2Pressing = this.isKlaxon2Pressing;
    }
}
