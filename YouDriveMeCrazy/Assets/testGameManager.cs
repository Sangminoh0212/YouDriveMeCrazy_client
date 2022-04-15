using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGameManager : MonoBehaviour
{

    public GameObject player1, player2;
    public Transform gameStartTransform;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player1, gameStartTransform.position, gameStartTransform.rotation);
        Instantiate(player2, gameStartTransform.position, gameStartTransform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
