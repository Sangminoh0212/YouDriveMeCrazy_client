using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreBoardManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScores());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
                
                Debug.Log(result);
            }
        }
    }

    #endregion
}
