using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    private KeyCode leftTurnBtn, leftTurnSignalBtn, rightTurnSignalBtn, breakBtn, klaxonBtn;
    [SerializeField] private ControllerManager controllerManager;


    void Awake()
    {
        leftTurnBtn = KeyCode.LeftArrow;
        // originally KeyCode.S
        breakBtn = KeyCode.DownArrow;
        leftTurnSignalBtn = KeyCode.A;
        rightTurnSignalBtn = KeyCode.D;
        klaxonBtn = KeyCode.Space;
    }   

    void Start()
    {
        controllerManager = GameObject.Find("Manager").transform.Find("ControllerManager").GetComponent<ControllerManager>();
    }

    void Update()
    {
        if(controllerManager != null){
            if (Input.GetKey(leftTurnBtn))
            {
                controllerManager.TurnRight();
            }
            if (Input.GetKey(breakBtn))
            {
                controllerManager.Break();   
            }
            if (Input.GetKeyDown(leftTurnSignalBtn))
            {

            }
            if (Input.GetKeyDown(rightTurnSignalBtn))
            {

            }
            if (Input.GetKeyDown(klaxonBtn))
            {

            }
        }
    }
}
