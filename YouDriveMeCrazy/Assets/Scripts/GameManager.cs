using System.Diagnostics;
using System.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public enum GameState {Clear, Restart, Leave}

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    [SerializeField] private InputManager inputManager;

    #region UI
    private GameObject ui;
    private GameObject stageClearPanel;
    private GameObject gameOverPanel;
    #endregion

    private int time;

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
        PhotonNetwork.Instantiate(inputManager.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
    }


    // by 상민, stageClear, gameOver Panel 모두 SetAvtive(false)
    void Setup() {
        if(GameObject.Find("InputManager")) inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

        if(GameObject.Find("UI")){
            stageClearPanel = GameObject.Find("UI").transform.GetChild(0).gameObject;
            gameOverPanel = GameObject.Find("UI").transform.GetChild(1).gameObject;
        }
        if(stageClearPanel != null) stageClearPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }
    
    public void GameOver(){
        print("game over!!");
        StartCoroutine(CallPolice());
    }

    // by 상민, 플레이어 뒤에서 경찰차 소환 추가 필요
    // 다른 클래스에서 GameManager.Instance.GameOver() 호출해서 게임 오버 시키면 경찰차 등장 후 @초 후에 GameOverPanel 활성화
    private IEnumerator CallPolice()
    {
        // by 상민, 경찰차 소환 필요
        yield return new WaitForSeconds(2f);
        if(gameOverPanel != null) gameOverPanel.SetActive(true);
    }


    // by 상민 내일 민호형한테 물어보기
    public void MoveScene(GameState gameState)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC(gameState.ToString(), RpcTarget.All);
            }
        }
    }

    // by 상민, 버튼 누른 사람이 방장&&현재 참가자 두명일 때 만 게임 재시작 가능
    // photonView.RPC 를 이용해 Master, client 모두 RestartStage1Scene() 호출
    public void Clear()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("ClearStage", RpcTarget.All);
            }
        }
    }
    
    // by 상민, 버튼 누른 사람이 방장&&현재 참가자 두명일 때 만 게임 재시작 가능
    // photonView.RPC 를 이용해 Master, client 모두 RestartStage1Scene() 호출
    public void Restart()
    {
        if (PhotonNetwork.IsMasterClient){
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2){
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("RestartStage", RpcTarget.All);
            }
        }
    }

    // by 상민, 버튼 누른 사람이 방장&&현재 참가자 두명일 때 만 게임 재시작 가능
    // photonView.RPC 를 이용해 Master, client 모두 LeaveGame() 호출
    public void Leave()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("LeaveStage", RpcTarget.All);
            }
        }
    }

    #region ChangeScene
    // by 상민, 새로운 씬 로드하기 전 현재 오브젝트 제거
    [PunRPC]
    private void ClearStage()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer.ActorNumber);
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex+1);
    }

    // by 상민, 새로운 씬 로드하기 전 현재 오브젝트 제거
    [PunRPC]
    private void RestartStage()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer.ActorNumber);
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().name);
    }

    // by 상민, 새로운 씬 로드하기 전 현재 오브젝트 제거
    [PunRPC]
    private void LeaveStage(){
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer.ActorNumber);
        PhotonNetwork.LoadLevel(0);
    }

    // by 상민, 방 나가면 자동으로 호출
    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }

    #endregion

}




