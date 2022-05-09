using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NewInputManager : MonoBehaviourPunCallbacks
{
    public KeyCode brakeBtn, leftTurnBtn, leftTurnSignalBtn, rightTurnSignalBtn, klaxonBtn1;
    public KeyCode accelBtn, rightTurnBtn, gotoLeftWiperBtn, gotoRightWiperBtn, klaxonBtn2;

    [SerializeField]
    private GameObject carPrefab;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate(carPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
    }

    void Update()
    {
        if (NewCarController.carController != null)
        {
            // Player 1
            if (PhotonNetwork.IsMasterClient)
            {
                if (Input.GetKey(brakeBtn)) { NewCarController.carController.isBreakPressing = true; }
                else { NewCarController.carController.isBreakPressing = false; }

                if (Input.GetKey(leftTurnBtn)) { NewCarController.carController.isLeftTurnPressing = true; }
                else { NewCarController.carController.isLeftTurnPressing = false; }

                if (Input.GetKey(leftTurnSignalBtn)) { NewCarController.carController.isLeftTurnSignalPressing = true; }
                else { NewCarController.carController.isLeftTurnSignalPressing = false; }

                if (Input.GetKey(rightTurnSignalBtn)) { NewCarController.carController.isRightTurnSignalPressing = true; }
                else { NewCarController.carController.isRightTurnSignalPressing = false; }

                if (Input.GetKey(klaxonBtn1)) { NewCarController.carController.isKlaxon1Pressing = true; }
                else { NewCarController.carController.isKlaxon1Pressing = false; }
            }

            //Player 2 
            if (!PhotonNetwork.IsMasterClient)
            {
                if (Input.GetKey(accelBtn)) { NewCarController.carController.isAccelPressing = true; }
                else { NewCarController.carController.isAccelPressing = false; }

                if (Input.GetKey(rightTurnBtn)) { NewCarController.carController.isRightTurnPressing = true; }
                else { NewCarController.carController.isRightTurnPressing = false; }

                if (Input.GetKey(gotoLeftWiperBtn)) { NewCarController.carController.isGotoLeftWiperPressing = true; }
                else { NewCarController.carController.isGotoLeftWiperPressing = false; }

                if (Input.GetKey(gotoLeftWiperBtn)) { NewCarController.carController.isGotoRightWiperPressing = true; }
                else { NewCarController.carController.isGotoRightWiperPressing = false; }

                if (Input.GetKey(klaxonBtn2)) { NewCarController.carController.isKlaxon2Pressing = true; }
                else { NewCarController.carController.isKlaxon2Pressing = false; }
            }
        }
    }
}