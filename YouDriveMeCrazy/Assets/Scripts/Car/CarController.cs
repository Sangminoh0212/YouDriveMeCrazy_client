using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{


    // Let's see is it needed
    public static CarController carController;  
    // Temporary variables for test.
    // Will be located in InputController
    public KeyCode leftTurnBtn, rightTurnBtn, gotoLeftWiperBtn, gotoRightWiperBtn, leftTurnSignalBtn, rightTurnSignalBtn, brakeBtn, accelBtn, klaxonBtn;

    [Header("Input")]
    public InputManager inputManager;

    public float verticalInput;
    public float horizontalInput;
    public bool isWiperPressing;
    public bool preIsWiperPressing;
    public bool isWiperLeft = false;

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

        //// disable collider for remote copy
        //frontRightCollider.enabled = false;
        //frontLeftCollider.enabled = false;
        //rearRightCollider.enabled = false;
        //rearLeftCollider.enabled = false;

    }

    void Update()
    {
        GetInput();

        UpdateWheelPhysics();
        UpdateWheelTransforms();
        UpdateWiper();
    }

    // Get input from InputController(for now ControllerManager)
    private void GetInput()
    {
        verticalInput = inputManager.GetVertical();
        horizontalInput = inputManager.GetHorizontal();
        
        
        isWiperPressing = inputManager.IsWiperPressing();
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
        //frontLeftTransform.rotation = frontWheelRot;
        //frontRightTransform.rotation = frontWheelRot;
        //rearLeftTransform.rotation = rearWheelRot;
        //rearRightTransform.rotation = rearWheelRot;
    }

    // Updates the wheel physics.
    private void UpdateWheelPhysics()
    {
        // accelerate
        rearRightCollider.motorTorque = verticalInput * engineForce;
        rearLeftCollider.motorTorque = verticalInput * engineForce;
        frontRightCollider.motorTorque = verticalInput * engineForce;
        frontLeftCollider.motorTorque = verticalInput * engineForce;

        // steer
        frontRightCollider.steerAngle = horizontalInput * steerAngle;
        frontLeftCollider.steerAngle = horizontalInput * steerAngle;

        // apply brakeTorque
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Break");
            rearRightCollider.brakeTorque = breakForce;
            rearLeftCollider.brakeTorque = breakForce;
            frontRightCollider.brakeTorque = breakForce;
            frontLeftCollider.brakeTorque = breakForce;
        }

        // reset brakeTorque
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Break stop");
            rearRightCollider.brakeTorque = 0;
            rearLeftCollider.brakeTorque = 0;
            frontRightCollider.brakeTorque = 0;
            frontLeftCollider.brakeTorque = 0;
        }
    }

    private void UpdateWiper()
    {
        if (!preIsWiperPressing && isWiperPressing)
        {
            ChangeWiperState();
        }

        preIsWiperPressing = isWiperPressing;
    }

    private void ChangeWiperState()
    {
        
    }
}