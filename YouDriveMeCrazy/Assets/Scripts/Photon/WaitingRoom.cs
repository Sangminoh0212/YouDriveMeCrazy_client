using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private byte maxPlayers = 4;

    [SerializeField] private InputField roomNameText;
    [SerializeField] private InputField roomNameInput;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    // New Room Button onClick
    public void newRoom()
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions
        {
            MaxPlayers = 4
        });
    }
    
    // Enter Button onClick
    public void Enter()
    {
        PhotonNetwork.JoinRoom(roomNameInput.text);
    }

    public override void OnJoinedRoom()
    {
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }

    private void OnConnectedToServer()
    {
        Debug.Log("Connected!");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Fail");
    }
}
