using System.Diagnostics;
using System.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;



public static class SavingData
{
    public static string timeReocrd = "0";
    public static int presentStageNum;
    public static string player1Name;
    public static string player2Name;
}


public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    [SerializeField] private GameObject inputManager;

    #region UI
    [SerializeField] private GameObject stageClearPanel;
    [SerializeField] private GameObject gameClearPanel;
    [SerializeField] private GameObject gameOverPanel;
    #endregion

    private bool isGameEnd;
    private float currentStageClearTime;

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


    void Setup() {
        print("Stage" + SavingData.presentStageNum + " Start!");
        isGameEnd = false;
        currentStageClearTime = 0;
        
        if (stageClearPanel != null) {stageClearPanel.SetActive(false); }
        if (gameClearPanel != null) {gameClearPanel.SetActive(false); }
        if (gameOverPanel != null) {gameOverPanel.SetActive(false); }
    }

    void Update()
    {
        #region Check Cheat Mode
        Cheat.updateCheatState();
        #endregion

        if (!isGameEnd) {
            currentStageClearTime += Time.deltaTime;
        }
    }

        // by상민, clearNum==2시 서버에 클리어 시간 인서트하고, 클리어 시간 표시 구현
        // 스테이지 1 클리어 시 리브&고넥스트
        // 스테이지 2 클리어 시 개인 점수 표시하고 리브만
        // 스코어보드는 타이틀에서만 볼 수 있음
    public void StageClear(){
        if(!isGameEnd){
            isGameEnd = true;
            print("stage"+ SavingData.presentStageNum + " clear!!");
            print("You took" + currentStageClearTime + "seconds!");

            if(SavingData.presentStageNum== 2)
            {
                float Stage1ClearTime = float.Parse(SavingData.timeReocrd);
                SavingData.timeReocrd = (Stage1ClearTime + currentStageClearTime).ToString();
                StartCoroutine(CallGameClear());
            }
            else {
                SavingData.timeReocrd = currentStageClearTime.ToString();
                StartCoroutine(CallStageClear());    
            }
        } 
    }

    public void GameOver()
    {
        if(!isGameEnd){
            print("GameOver");
            isGameEnd = true;
            currentStageClearTime = 0;
            StartCoroutine(CallGameOver());
        }

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


    // by 상민, 버튼 누른 사람이 방장&&현재 참가자 두명일 때 만 게임 재시작 가능
    // photonView.RPC 를 이용해 Master, client 모두 RestartStage1Scene() 호출
    public void Next()
    {
        SavingData.presentStageNum += 1;
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2 || Cheat.cheatMode)
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
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2 || Cheat.cheatMode)
            {
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("SyncRestartStage", RpcTarget.All);
            }
        }
    }

    // by 상민, 버튼 누른 사람이 방장&&현재 참가자 두명일 때 만 게임 재시작 가능
    // photonView.RPC 를 이용해 Master, client 모두 LeaveGame() 호출
    public void Leave()
    {
        SavingData.presentStageNum = 1;
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2 || Cheat.cheatMode)
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
        PhotonNetwork.LoadLevel(1);
    }

    // by 상민, 방 나가면 자동으로 호출
    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }

    #endregion
}

