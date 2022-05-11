using System;
using System.Collections;
using ScoreBoard;
using UnityEngine;
using UnityEngine.Networking;

namespace Api
{
    public class Api
    {
        public static IEnumerator LoadScores(Action<Scores[]> callback)
        {
            string url = "http://localhost:8080/scores";
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();
                if (www.isDone)
                {
                    string result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    callback(JsonUtility.FromJson<ScoresResDto>(result).data);
                }
            }
        }

        public static IEnumerator InsertScore(string player1, string player2, int point, Action<Scores> callback)
        {
            string url = "http://localhost:8080/scores/" + player1 + "/" + player2 + "/" + point;
            using (UnityWebRequest www = UnityWebRequest.Post(url, ""))
            {
                yield return www.SendWebRequest();
                if (www.isDone)
                {
                    string result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    callback(JsonUtility.FromJson<Scores>(result));
                }
            }
        }
    }
}