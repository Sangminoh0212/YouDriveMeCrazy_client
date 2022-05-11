using System;
using System.Collections;
using System.Collections.Generic;
using ScoreBoard;
using TMPro;
using UnityEngine;
using Api;
using UnityEngine.Networking;

public class ScoreBoardManager : MonoBehaviour
{
    #region private Fields

    [SerializeField] private TMP_Text scoreText;

    private Scores[] scoreList;

    #endregion
    
    void Start()
    {
        StartCoroutine(Api.Api.LoadScores((data) =>
        {
            scoreList = data;
            
            PrintScore();
        }));

        Scores[] scores = {new Scores(1, "kim", "park", 100)};
        ScoresResDto scoresResDto = new ScoresResDto(scores);

        string json = JsonUtility.ToJson(scoresResDto);
        
        Debug.Log(json);
    }

    #region private methods

    private void PrintScore()
    {
        string txt = "";

        for(int i=0; i<scoreList.Length; i++)
        {
            Scores score = scoreList[i];

            txt += score.ToString();
        }
        
        scoreText.SetText(txt);
    }

    #endregion

    #region Public Methods

    public void InsertScore()
    {
        StartCoroutine(Api.Api.InsertScore("Lee", "Choi", 150, scores =>
        {
            Debug.Log(scores.ToString());
        }));
    }

    #endregion
}
