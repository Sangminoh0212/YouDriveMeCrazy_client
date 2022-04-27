using TMPro;
using UnityEngine;

namespace Photon
{
    public interface IRoomState
    {
        // 상태에 따라 Panel 데이터를 다르게 설정해주는 함수
        public void SetPanel(GameObject connectingPanel, GameObject enterPanel, GameObject roomPanel,
            TMP_Text p1NicknameText, TMP_Text p2NicknameText, TMP_Text currentRoomCodeText);
    }
}