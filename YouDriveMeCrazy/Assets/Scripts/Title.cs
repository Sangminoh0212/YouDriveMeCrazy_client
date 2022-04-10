using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{
     public string SceneToLoad;
    
     void Start() {
        
    }
     void Update() {
        
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
// public void QuitGame()
//     {
//         Application.quitting;
//     }
}
