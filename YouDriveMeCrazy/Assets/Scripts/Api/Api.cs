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

        public static IEnumerator InsertScore(string player1, string player2, string clearTime, Action<Scores> callback)
        {
            string url = "http://localhost:8080/scores";

            Scores scores = new Scores(0l, player1, player2, float.Parse(clearTime));

            using (UnityWebRequest www = UnityWebRequest.Post(url, JsonUtility.ToJson(scores)))
            {
                byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(JsonUtility.ToJson(scores));
                www.uploadHandler = new UploadHandlerRaw(jsonToSend);
                www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");
                
                yield return www.SendWebRequest();
                if (www.isDone)
                {
                    string result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    callback(JsonUtility.FromJson<Scores>(result));
                }
            }
        }
        
        public static IEnumerator Record(RecordDto recordDto, Action<RecordResultDto> callback)
        {
            string url = "http://localhost:8080/record";

            string json = JsonUtility.ToJson(recordDto);

            using (UnityWebRequest www = UnityWebRequest.Post(url, json))
            {
                byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
                www.uploadHandler = new UploadHandlerRaw(jsonToSend);
                www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");
                
                yield return www.SendWebRequest();
                if (www.isDone)
                {
                    string result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    callback(JsonUtility.FromJson<RecordResultDto>(result));
                }
            }
        }
    }
}