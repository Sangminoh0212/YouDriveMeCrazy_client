using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class GameManager_ForCar : MonoBehaviourPunCallbacks
{
    public GameObject inputManager;

    void Start()
    {
        PhotonNetwork.Instantiate(inputManager.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
    }

    void Update()
    {
        
    }
}
