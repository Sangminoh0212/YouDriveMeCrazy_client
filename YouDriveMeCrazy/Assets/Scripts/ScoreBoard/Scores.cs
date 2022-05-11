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
        [SerializeField] private int points;

        public Scores(long scoresId, string player1, string player2, int points)
        {
            this.scoresId = scoresId;
            this.player1 = player1;
            this.player2 = player2;
            this.points = points;
        }

        public override string ToString()
        {
            return scoresId + " " + player1 + " " + player2 + " " + points;
        }
    }
}