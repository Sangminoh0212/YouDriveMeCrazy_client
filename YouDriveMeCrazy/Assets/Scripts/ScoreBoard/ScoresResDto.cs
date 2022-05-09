using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScoreBoard
{
    [Serializable]
    public class ScoresResDto
    {
        [SerializeField] public Scores[] data;

        public ScoresResDto(Scores[] data)
        {
            this.data = data;
        }
    }
}
