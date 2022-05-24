using System;

namespace Api
{
    [Serializable]
    public class RecordDto
    {
        private int userId;
        
        /**
         * 게임이 종료된 원인
         * 0 - 클리어
         * 1 - 동물 킬
         * 2 - 보행자 킬
         * 3 - 차 사고
         * 4 - 깜빡이 없이 차선 변경
         * 5 - 신호 위반(신호등)
         * 6 - 중앙선 침범
         * 7 - 길 벗어남
         */
        private byte type;

        private int maxSpeed;
        private int wiperCount;
        private int klaxonCount;
        private float clearTime;

        public RecordDto(int userId, byte type, int maxSpeed, int wiperCount, int klaxonCount, float clearTime)
        {
            this.userId = userId;
            this.type = type;
            this.maxSpeed = maxSpeed;
            this.wiperCount = wiperCount;
            this.klaxonCount = klaxonCount;
            this.clearTime = clearTime;
        }
    }
}