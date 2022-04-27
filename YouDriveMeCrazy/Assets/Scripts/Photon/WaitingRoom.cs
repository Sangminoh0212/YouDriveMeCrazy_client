using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Photon
{
    public class WaitingRoom : MonoBehaviourPunCallbacks
    {
        // State Design Pattern
        // 현재 상태를 저장하고 있음 (Not Connected / Not In Room / In Room)
        private IRoomState roomState;
    
        // 방에 최대로 입장할 수 있는 인원의 수
        [SerializeField] private byte maxPlayers = 2;
    
        // Objects
        [SerializeField] private GameObject connectingPanel;    // 서버와 연결되지 않았을 때 표시되는 화면
        [SerializeField] private GameObject enterPanel;         // 방에 입장하기 전 정보를 기입하는 화면
        [SerializeField] private GameObject roomPanel;          // 방에 입장했을 때 표시되는 화면
    
        [SerializeField] private TMP_InputField nicknameInput;  // EnterPanel   닉네임 적는 칸
        [SerializeField] private TMP_InputField roomCodeInput;  // EnterPanel   방 코드 적는 칸
        [SerializeField] private TMP_Text p1NicknameText;       // RoomPanel    플레이어 1의 닉네임이 표시되는 텍스트
        [SerializeField] private TMP_Text p2NicknameText;       // RoomPanel    플레이어 2의 닉네임이 표시되는 텍스트
        [SerializeField] private TMP_Text currentRoomCodeText;  // RoomPanel    현재 방 코드가 표시되는 텍스트
    
        #region MonoBehaviour Callbacks
    
        private void Start()
        {
            // 이 Scene 이 시작되었을 때 서버 연결이 안되어있는 상태면 roomState 를 Not Connected 로 설정
            // 서버 연결이 되어있으면 Not In Room 으로 설정
            if(PhotonNetwork.IsConnected) roomState = NotConnectedRoomState.GetInstance();
            else roomState = NotInRoomState.GetInstance();
        
            // Photon 초기 설정
            PhotonNetwork.AutomaticallySyncScene = true;    // 이게 없으면 Scene의 동기화가 안된다더라...
            PhotonNetwork.GameVersion = "1";                // 뭔지 잘 모름 TODO: 시간 나면 자세히 알아보기
            PhotonNetwork.ConnectUsingSettings();           // Master Server 에 연결하는 함수 -> OnConnectedToServer() 호출
        }

        private void Update()
        {
            // 매 프레임마다 roomState에 맞춰 Panel을 설정함
            if(roomState != null) roomState.SetPanel(connectingPanel, enterPanel, roomPanel, p1NicknameText, p2NicknameText, currentRoomCodeText);
        }

        #endregion

        #region Private Methods
    
        // 닉네임 유효성 검사
        private bool CheckNickname()
        {
            // TODO: 유효한 닉네임의 기준 정해야 함
            return nicknameInput.text.Length > 0;
        }

        #endregion

        #region Public Methods

        // New Room Button onClick
        public void NewRoom()
        {
            // TODO: 닉네임이 유효하지 않을 때 피드백
            if (!CheckNickname()) return;

            // set nickname
            PhotonNetwork.NickName = nicknameInput.text;
        
            // Create Room
            string roomCode = roomCodeInput.text;
            PhotonNetwork.CreateRoom(roomCode.Length < 1 ? null : roomCode, new RoomOptions
            {
                MaxPlayers = maxPlayers
            });
        }

        // Enter Button onClick
        public void Enter()
        {
            // TODO: 중복 코드 수정
            // TODO: 닉네임이 유효하지 않을 때 피드백
            if (!CheckNickname()) return;

            // set nickname
            PhotonNetwork.NickName = nicknameInput.text;
        
            // join room
            PhotonNetwork.JoinRoom(roomCodeInput.text);
        }
        
        // Play Button Onclick
        public void Play()
        {
            // TODO: 예외 처리 확실히
            
            if (PhotonNetwork.IsMasterClient)
            {
                if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
                {
                    // 게임 실행 코드
                }
                else
                {
                    Debug.Log("Need 2 players");
                }
            }
            else
            {
                Debug.Log("Only master client can push play");
            }
        }

        #endregion

        #region MonoBehaviourPunCallbacks

        // 방에 입장했을 때
        public override void OnJoinedRoom()
        {
            Debug.Log(PhotonNetwork.CurrentRoom.Name);
        
            // roomState를 In Room 으로 변경
            roomState = InRoomState.GetInstance();
        }

        // 방 입장에 실패했을 때
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            // TODO: 방 입장 실패 시 예외처리
            Debug.Log(returnCode);
            Debug.Log(message);
        }

        // 서버에 연결되었을 때
        private void OnConnectedToServer()
        {
            Debug.Log("Connected!");
        
            // roomState를 Not In Room 으로 변경
            roomState = NotInRoomState.GetInstance();
        }

        #endregion
    }
}
