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

    // #region PlayerPrefab
    // [SerializeField] private GameObject _player1Prefab;
    // [SerializeField] private GameObject _player2Prefab;
    // #endregion

    #region UI
    private GameObject stageClearPanel;
    private GameObject gameOverPanel;
    #endregion

    private static int _score;

    #region getter,setter
    public int score{
        get {return _score;}
        set {_score = value;}
    }
    #endregion

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


    // by 상민, stageClear, gameOver Panel 모두 SetAvtive(false)
    void Setup() {
        if(GameObject.FindWithTag("StageClearPanel")){
            stageClearPanel = GameObject.FindWithTag("StageClearPanel");
            stageClearPanel.SetActive(false);
        }

        if(GameObject.FindWithTag("GameOverPanel")){
            gameOverPanel = GameObject.FindWithTag("GameOverPanel");
            gameOverPanel.SetActive(false);
        }
    }
    

    // by 상민, 플레이어 뒤에서 경찰차 소환 추가 필요
    // 다른 클래스에서 GameManager.Instance.GameOver() 호출해서 게임 오버 시키면 경찰차 등장 후 @초 후에 GameOverPanel 활성화
    public IEnumerator GameOver()
    {
        print("game over!!");
        yield return new WaitForSeconds(2f);

        if(gameOverPanel != null) gameOverPanel.SetActive(true);
    }


    // by 상민, 버튼 누른 사람이 방장&&현재 참가자 두명일 때 만 게임 재시작 가능
    // photonView.RPC 를 이용해 Master, client 모두 RestartStage1Scene() 호출
    public void RestartStage1()
    {
        if (PhotonNetwork.IsMasterClient){
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2){
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("RestartStage1Scene", RpcTarget.All);
            }
        }
    }


    // by 상민, 새로운 씬 로드하기 전 현재 오브젝트 제거
    [PunRPC]
    public void RestartStage1Scene(){
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer.ActorNumber);
        PhotonNetwork.LoadLevel(1);
    }


    // by 상민, 버튼 누른 사람이 방장&&현재 참가자 두명일 때 만 게임 재시작 가능
    // photonView.RPC 를 이용해 Master, client 모두 RestartStage2Scene() 호출
    public void RestartStage2()
    {
        if (PhotonNetwork.IsMasterClient){
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2){
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("RestartStage2Scene", RpcTarget.All);
            }
        }
    }


    // by 상민, 새로운 씬 로드하기 전 현재 오브젝트 제거
    [PunRPC]
    public void RestartStage2Scene(){
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer.ActorNumber);
        PhotonNetwork.LoadLevel(2);
    }


    // by 상민, 버튼 누른 사람이 방장&&현재 참가자 두명일 때 만 게임 재시작 가능
    // photonView.RPC 를 이용해 Master, client 모두 Leave() 호출
    public void LeaveGame()
    {
        if (PhotonNetwork.IsMasterClient){
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2){
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("Leave", RpcTarget.All);
            }
        }
    }


    // by 상민, 새로운 씬 로드하기 전 현재 오브젝트 제거
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




