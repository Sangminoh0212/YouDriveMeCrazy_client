using TMPro;
using UnityEngine;

namespace Photon
{
    public class NotConnectedRoomState : IRoomState
    {
        private static NotConnectedRoomState INSTANCE = new NotConnectedRoomState();

        public static NotConnectedRoomState GetInstance()
        {
            return INSTANCE;
        }

        private NotConnectedRoomState()
        { }

        public void SetPanel(GameObject connectingPanel, GameObject enterPanel, GameObject roomPanel,
            TMP_Text p1NicknameText, TMP_Text p2NicknameText, TMP_Text currentRoomCodeText)
        {
            connectingPanel.SetActive(true);
            enterPanel.SetActive(false);
            roomPanel.SetActive(false);
        }
    }
}