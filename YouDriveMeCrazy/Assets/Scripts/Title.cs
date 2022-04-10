using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{
     public string SceneToLoad;
     public string SceneToLoad2;
    public string SceneToLoad3;

     void Start() {
        
    }
     void Update() {
        
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
    public void LoadOptions()
    {
        SceneManager.LoadScene(SceneToLoad2);
    }
    public void LoadOScoreBoard()
    {
        SceneManager.LoadScene(SceneToLoad3);
    }
    public void GameExit()
    {
        Application.Quit();
    }

// public void QuitGame()
//     {
//         Application.quitting;
//     }
}
