using System.Collections;
using System.Collections.Generic;
using ScoreBoard;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreBoardManager : MonoBehaviour
{
    #region private Fields

    [SerializeField] private TMP_Text scoreText;

    private Scores[] scoreList;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScores());

        Scores[] scores = {new Scores(1, "kim", "park", 100)};
        ScoresResDto scoresResDto = new ScoresResDto(scores);

        string json = JsonUtility.ToJson(scoresResDto);
        
        Debug.Log(json);
    }
// Update is called once per frame
    void Update()
    {
        
    }

    #region private methods

    private ScoresResDto JsonToScoresResDto(string json)
    {
        return JsonUtility.FromJson<ScoresResDto>(json);
    }

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

    #region Coroutine

    IEnumerator LoadScores()
    {
        string url = "http://localhost:8080/scores";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.isDone)
            {
                string result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                
                // result = "{\"Items\":" + result + "}";

                Debug.Log(result);
                Debug.Log(JsonToScoresResDto(result).data == null);

                scoreList = JsonToScoresResDto(result).data;
                
                PrintScore();
            }
        }
    }

    #endregion
}
