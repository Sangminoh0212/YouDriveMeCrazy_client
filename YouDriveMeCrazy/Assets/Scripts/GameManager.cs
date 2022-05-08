using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

namespace Photon{
    
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public static GameManager instance;

        #region PlayerPrefab
        [SerializeField] private GameObject _player1Prefab;
        [SerializeField] private GameObject _player2Prefab;
        #endregion

        #region UI
        public GameObject stageClearPanel;
        public GameObject gameOverPanel;
        #endregion

        public static int score { get; set; }
        public bool isGameOver { get; set; }

        [SerializeField] private GameObject policeCar;

        // Start is called before the first frame update
        void Start()
        {
            if(instance == null){
                instance = this;
            }
            else{
                Destroy(gameObject);
            }

            // by 상민, 방장일경우 player1 프리팹 부여, 아닐경우 player2 프리팹 부여
            if(PhotonNetwork.IsMasterClient){
                if(_player1Prefab!=null) PhotonNetwork.Instantiate(_player1Prefab.name, _player1Prefab.transform.position, _player1Prefab.transform.rotation);
            }
            else{
                if (_player2Prefab != null) PhotonNetwork.Instantiate(_player2Prefab.name, _player2Prefab.transform.position, _player2Prefab.transform.rotation);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void OnLeftRoom(){
            // 룸 나갈 때 자동으로 실행되는 메소드
            SceneManager.LoadScene("Lobby");
        }

        public IEnumerator gameOver(){

            isGameOver = true;
            // by 상민, 플레이어 뒤에서 경찰차 소환
            // Transform player = GameObject.FindGameObjectWithTag("Car").transform;
            // Vector3 spawnPos = new Vector3(player.localPosition.x, player.localPosition.y, player.localPosition.z);
            // Instantiate(policeCar, spawnPos, player.transform.localRotation);
            print("game over");
            yield return new WaitForSeconds(10f);

            gameOverPanel.SetActive(true);
                
        }

        public void resetGame(){
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.AutomaticallySyncScene = true;
                PhotonNetwork.LoadLevel(PhotonNetwork.CurrentRoom.Name);
            }
        }

        public void leaveGame(){

            PhotonNetwork.LeaveRoom();
        }

    }

}

