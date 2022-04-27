using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private float vertical;
    private float horizontal;
    private bool isWiperPressing = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        isWiperPressing = Input.GetKey(KeyCode.W);
    }

    public float GetVertical()
    {
        return vertical;
    }

    public float GetHorizontal()
    {
        return horizontal;
    }

    public bool IsWiperPressing()
    {
        return isWiperPressing;
    }
}
