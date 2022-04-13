using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoBackButton : MonoBehaviour
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
}
