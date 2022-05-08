using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CarController : MonoBehaviourPunCallbacks, IPunObservable
{
    public static CarController carController;

    #region Player Input
    // These bool variables should be "private" and have "get", "set" method later
    
    // Player1
    [HideInInspector] public bool isBreakPressing;
    [HideInInspector] public bool isLeftTurnPressing;
    [HideInInspector] public bool isLeftTurnSignalPressing;
    [HideInInspector] public bool isRightTurnSignalPressing;
    [HideInInspector] public bool isKlaxon1Pressing;

    //Player2
    [HideInInspector] public bool isAccelPressing;
    [HideInInspector] public bool isRightTurnPressing;
    [HideInInspector] public bool isGotoLeftWiperPressing;
    [HideInInspector] public bool isGotoRightWiperPressing;
    [HideInInspector] public bool isKlaxon2Pressing;
    

    #region Calculated Input Values
    public float keyMomentum = 3f;  // Key Input goes 1 in 1/3f seconds
    private float accelValue;
    private float turnValue;
    private bool wasBreakPressed;
    #endregion

    #endregion

    [Header("Wheel Control")]
    #region Wheel Control
    public float engineForce = 900f;
    public float steerAngle = 30f;
    public float breakForce = 30000f;

    public WheelCollider frontRightCollider;
    public WheelCollider frontLeftCollider;
    public WheelCollider rearRightCollider;
    public WheelCollider rearLeftCollider;

    public Transform frontRightTransform;
    public Transform frontLeftTransform;
    public Transform rearRightTransform;
    public Transform rearLeftTransform;

    // used for lowering the center of mass of the car
    public Transform centerOfMass;

    // expose wheel rotate for the generic tracker
    public Quaternion frontWheelRot;
    public Quaternion rearWheelRot;
    #endregion


    private void Start()
    {
        carController = this;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
    }

    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1.0f)
        {
            timer = 0;
            print(isLeftTurnPressing);
            print(isRightTurnPressing);
            print(isAccelPressing);
            print(isBreakPressing);
        }


        calculateInput();

        UpdateWheelPhysics();
        UpdateWheelTransforms();
        //UpdateWiper();
    }

    private void calculateInput()
    {
        // Map isPressing variables to -1 ~ +1 float values
        accelValue += isAccelPressing ? keyMomentum * Time.deltaTime : -keyMomentum * Time.deltaTime;
        accelValue = Mathf.Clamp(accelValue, 0, 1);

        turnValue += isLeftTurnPressing ? -keyMomentum * Time.deltaTime : keyMomentum * Time.deltaTime;
        turnValue += isRightTurnPressing ? keyMomentum * Time.deltaTime : -keyMomentum * Time.deltaTime;
        turnValue = Mathf.Clamp(turnValue, -1, 1);
    }

    // Updates the wheel transforms.
    private void UpdateWheelTransforms()
    {
        Quaternion rotation;
        Vector3 position;

        frontLeftCollider.GetWorldPose(out position, out rotation);
        frontLeftTransform.position = position;
        frontLeftTransform.rotation = rotation;

        frontRightCollider.GetWorldPose(out position, out rotation);
        frontRightTransform.position = position;
        frontRightTransform.rotation = rotation;

        // update the front wheel rotation for generic tracker
        frontWheelRot = rotation;

        rearLeftCollider.GetWorldPose(out position, out rotation);
        rearLeftTransform.position = position;
        rearLeftTransform.rotation = rotation;

        rearRightCollider.GetWorldPose(out position, out rotation);
        rearRightTransform.position = position;
        rearRightTransform.rotation = rotation;

        // update the rear wheel rotation for generic tracker
        rearWheelRot = rotation;

        //// apply the rotation updates for remote copies
        frontLeftTransform.rotation = frontWheelRot;
        frontRightTransform.rotation = frontWheelRot;
        rearLeftTransform.rotation = rearWheelRot;
        rearRightTransform.rotation = rearWheelRot;
    }

    // Updates the wheel physics.
    private void UpdateWheelPhysics()
    {
        // accelerate
        rearRightCollider.motorTorque = accelValue * engineForce;
        rearLeftCollider.motorTorque = accelValue * engineForce;
        frontRightCollider.motorTorque = accelValue * engineForce;
        frontLeftCollider.motorTorque = accelValue * engineForce;

        // steer
        frontRightCollider.steerAngle = turnValue * steerAngle;
        frontLeftCollider.steerAngle = turnValue * steerAngle;

        // apply brakeTorque
        if (isBreakPressing && !wasBreakPressed)
        {
            wasBreakPressed = true;
            Debug.Log("Break");
            rearRightCollider.brakeTorque = breakForce;
            rearLeftCollider.brakeTorque = breakForce;
            frontRightCollider.brakeTorque = breakForce;
            frontLeftCollider.brakeTorque = breakForce;
        }

        // reset brakeTorque
        if (!isBreakPressing && wasBreakPressed)
        {
            wasBreakPressed = false;
            Debug.Log("Break stop");
            rearRightCollider.brakeTorque = 0;
            rearLeftCollider.brakeTorque = 0;
            frontRightCollider.brakeTorque = 0;
            frontLeftCollider.brakeTorque = 0;
        }
    }

    private void UpdateWiper()
    {
        //if (!preIsWiperPressing && isWiperPressing)
        //{
        //    ChangeWiperState();
        //}

        //preIsWiperPressing = isWiperPressing;
    }

    private void ChangeWiperState()
    {

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if(stream.IsReading)
            {
                this.isAccelPressing = (bool)stream.ReceiveNext();
                this.isRightTurnPressing = (bool)stream.ReceiveNext();
                this.isGotoLeftWiperPressing = (bool)stream.ReceiveNext();
                this.isGotoRightWiperPressing = (bool)stream.ReceiveNext();
                this.isKlaxon2Pressing = (bool)stream.ReceiveNext();
            }
        }
        else
        {
            if (stream.IsWriting)
            {
                stream.SendNext(this.isAccelPressing);
                stream.SendNext(this.isRightTurnPressing);
                stream.SendNext(this.isGotoLeftWiperPressing);
                stream.SendNext(this.isGotoRightWiperPressing);
                stream.SendNext(this.isKlaxon2Pressing);
            }
        }
    }
}