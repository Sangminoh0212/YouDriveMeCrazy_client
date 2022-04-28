using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    private float h,v = 0;
    private float speed = 0.01f;
    private float turnSpeed = 0.01f;
    private CarController carController;

    public void Start(){
        //carController = GameObject.Find("Car").GetComponent<CarController>();
    }

    public void TurnLeft(){
        if (h >= -1 && h <= 1) h += turnSpeed;
        h = Mathf.Clamp(h,0,1);
        //carController.H = h;
    }

    public void TurnRight(){
        if (h <= 1 && h >= -1) h -= turnSpeed;
        h = Mathf.Clamp(h, -1, 0);
        //carController.H = h;
    }

    public void Accel(){
        if (v <= 1 && v >= -1) v += speed;
        v = Mathf.Clamp(v, 0, 1);
        //carController.V = v;
    }

    public void Break(){
        if (v <= 1 && v >= -1) v -= speed;
        v = Mathf.Clamp(v, -1, 0);
        //carController.V = v;
    }


}
