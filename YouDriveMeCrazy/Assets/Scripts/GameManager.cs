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

    [SerializeField] private GameObject inputManager;

    #region UI
    private GameObject ui;
    [SerializeField] private GameObject stageClearPanel;
    [SerializeField] private GameObject gameClearPanel;
    [SerializeField] private GameObject gameOverPanel;
    #endregion

    private static int clearNum;

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
        if (stageClearPanel != null) {stageClearPanel.SetActive(false); }
        if (gameClearPanel != null) {gameClearPanel.SetActive(false); }
        if (gameOverPanel != null) {gameOverPanel.SetActive(false); }
    }
    
    public void StageClear(){
        clearNum++;
        print("stage"+ clearNum + " clear!!");
        // by상민, clearNum==2시 스코어보드 불러야함
        if(clearNum==2) { StartCoroutine(CallGameClear()); } else { StartCoroutine(CallStageClear()); }
    }

    public void GameOver()
    {
        print("game over!!");
        StartCoroutine(CallGameOver());
    }

    // by 상민, 플레이어 뒤에서 경찰차 소환 추가 필요
    // 다른 클래스에서 GameManager.Instance.GameOver() 호출해서 게임 오버 시키면 경찰차 등장 후 @초 후에 GameOverPanel 활성화
    public IEnumerator CallStageClear()
    {
        yield return new WaitForSeconds(2f);
        if (stageClearPanel != null)
        { stageClearPanel.SetActive(true); }
    }

    public IEnumerator CallGameClear()
    {
        yield return new WaitForSeconds(2f);
        if (gameClearPanel != null)
        { gameClearPanel.SetActive(true); }
    }
    
    public IEnumerator CallGameOver()
    {
        // by 상민, 경찰차 소환 필요
        yield return new WaitForSeconds(2f);
        if(gameOverPanel != null) 
            gameOverPanel.SetActive(true);
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
    public void Next()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("SyncNextStage", RpcTarget.All);
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
                photonView.RPC("SyncRestartStage", RpcTarget.All);
            }
        }
    }

    // by 상민, 버튼 누른 사람이 방장&&현재 참가자 두명일 때 만 게임 재시작 가능
    // photonView.RPC 를 이용해 Master, client 모두 LeaveGame() 호출
    public void Leave()
    {
        clearNum = 0;
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("SyncLeaveStage", RpcTarget.All);
            }
        }
    }


    #region ChangeScene
    // by 상민, 새로운 씬 로드하기 전 현재 오브젝트 제거
    [PunRPC]
    private void SyncNextStage()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer.ActorNumber);
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex+1);
    }

    // by 상민, 새로운 씬 로드하기 전 현재 오브젝트 제거
    [PunRPC]
    private void SyncRestartStage()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer.ActorNumber);
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().name);
    }

    // by 상민, 새로운 씬 로드하기 전 현재 오브젝트 제거
    [PunRPC]
    private void SyncLeaveStage(){
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




