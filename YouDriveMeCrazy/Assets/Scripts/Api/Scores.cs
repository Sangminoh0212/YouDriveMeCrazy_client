using System;
using UnityEngine;

namespace ScoreBoard
{
    [Serializable]
    public class Scores
    {
        [SerializeField] private long scoresId;

        [SerializeField] private string player1;
        [SerializeField] private string player2;

        // To be string
        [SerializeField] private float clearTime;

        public Scores(long scoresId, string player1, string player2, float clearTime)
        {
            this.scoresId = scoresId;
            this.player1 = player1;
            this.player2 = player2;
            this.clearTime = clearTime;
        }

        public override string ToString()
        {
            return player1 + "\t" + player2 + "\t" + clearTime;
        }
    }
}