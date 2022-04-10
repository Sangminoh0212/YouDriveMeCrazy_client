using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoOptions : MonoBehaviour
{
    // Start is called before the first frame update
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
