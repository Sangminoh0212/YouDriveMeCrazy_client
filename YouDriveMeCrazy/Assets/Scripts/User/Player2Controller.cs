using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    private KeyCode rightTurnBtn, gotoLeftWiperBtn, gotoRightWiperBtn, accelBtn, klaxonBtn;
    [SerializeField] private ControllerManager controllerManager;


    void Awake()
    {
        rightTurnBtn = KeyCode.RightArrow;
        // originally KeyCode.S
        accelBtn = KeyCode.UpArrow;
        gotoLeftWiperBtn = KeyCode.A;
        gotoRightWiperBtn = KeyCode.D;
        klaxonBtn = KeyCode.Space;
    }

    void Start()
    {
        controllerManager = GameObject.Find("Manager").transform.Find("ControllerManager").GetComponent<ControllerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controllerManager != null)
        {
            if (Input.GetKey(rightTurnBtn))
            {
                controllerManager.TurnLeft();
            }
            if (Input.GetKey(accelBtn))
            {
                controllerManager.Accel();
            }
            if (Input.GetKeyDown(gotoLeftWiperBtn))
            {

            }
            if (Input.GetKeyDown(gotoRightWiperBtn))
            {

            }
            if (Input.GetKeyDown(klaxonBtn))
            {

            }
        }
    }
}
