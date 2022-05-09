using System.Diagnostics;
using System.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    #region PlayerPrefab
    [SerializeField] private GameObject _player1Prefab;
    [SerializeField] private GameObject _player2Prefab;
    #endregion

    #region UI
    public GameObject stageClearPanel;
    public GameObject gameOverPanel;
    #endregion

    private static int _score;
    private bool _isGameOver;
    private bool _isGameRestart;

    #region getter,setter
    public int score{
        get {return _score;}
        set {_score = value;}
    }
    
    public bool isGameOver{
        get {return _isGameOver;}
        set {_isGameOver = value;}
    }

    public bool isGameRestart{
        get {return _isGameRestart;}
        set {_isGameRestart = value;}
    }
    #endregion
    
    [SerializeField] private GameObject policeCar;

    // Start is called before the first frame update

    void Awake(){
        if (Instance != null)
        {
            if(Instance != this) Destroy(gameObject);
        }
        else {
            Instance = this;
        }

    }

    void Start()
    {
        Setup();

        // by 상민, 방장일경우 player1 프리팹 부여, 아닐경우 player2 프리팹 부여
        // if (PhotonNetwork.IsMasterClient)
        // {
        //     if (_player1Prefab != null) PhotonNetwork.Instantiate(_player1Prefab.name, _player1Prefab.transform.position, _player1Prefab.transform.rotation);
        // }
        // else
        // {
        //     if (_player2Prefab != null) PhotonNetwork.Instantiate(_player2Prefab.name, _player2Prefab.transform.position, _player2Prefab.transform.rotation);
        // }
    }


    void Setup() {
        isGameOver = false;
        if(gameOverPanel != null) gameOverPanel.SetActive(false);
        if(stageClearPanel != null) stageClearPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameOver)
        {
            StartCoroutine(GameOver());
            isGameOver = false;
        }
    }


    public IEnumerator GameOver()
    {
        // by 상민, 플레이어 뒤에서 경찰차 소환
        // Transform player = GameObject.FindGameObjectWithTag("Car").transform;
        // Vector3 spawnPos = new Vector3(player.localPosition.x, player.localPosition.y, player.localPosition.z);
        // Instantiate(policeCar, spawnPos, player.transform.localRotation);
        print("game over!!");
        yield return new WaitForSeconds(2f);

        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        if (PhotonNetwork.IsMasterClient){
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2){
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("Restart", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    public void Restart(){
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer.ActorNumber);
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveGame()
    {
        if (PhotonNetwork.IsMasterClient){
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2){
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("Leave", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    public void Leave(){
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer.ActorNumber);
        PhotonNetwork.LoadLevel(0);
    }

    
    // by 상민, 방 나가면 자동으로 호출
    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }

}




