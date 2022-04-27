using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Photon
{
    public class InRoomState : IRoomState
    {
        private static InRoomState INSTANCE = new InRoomState();

        public static InRoomState GetInstance()
        {
            return INSTANCE;
        }

        private InRoomState()
        { }

        public void SetPanel(GameObject connectingPanel, GameObject enterPanel, GameObject roomPanel,
            TMP_Text p1NicknameText, TMP_Text p2NicknameText, TMP_Text currentRoomCodeText)
        {
            // Room Panel 의 세부 데이터 수정
            Player[] playerList = PhotonNetwork.PlayerList;
            p1NicknameText.SetText(playerList[0].NickName);
            if(playerList.Length == 2) p2NicknameText.SetText(playerList[1].NickName);
            else p2NicknameText.SetText("");
            
            currentRoomCodeText.SetText(PhotonNetwork.CurrentRoom.Name);
            
            connectingPanel.SetActive(false);
            enterPanel.SetActive(false);
            roomPanel.SetActive(true);
        }
    }
}