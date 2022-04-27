using TMPro;
using UnityEngine;

namespace Photon
{
    public class NotInRoomState : IRoomState
    {
        private static NotInRoomState INSTANCE = new NotInRoomState();

        public static NotInRoomState GetInstance()
        {
            return INSTANCE;
        }

        private NotInRoomState()
        { }

        public void SetPanel(GameObject connectingPanel, GameObject enterPanel, GameObject roomPanel,
            TMP_Text p1NicknameText, TMP_Text p2NicknameText, TMP_Text currentRoomCodeText)
        {
            connectingPanel.SetActive(false);
            enterPanel.SetActive(true);
            roomPanel.SetActive(false);
        }
    }
}