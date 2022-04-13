using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public static CarController carController;
    public KeyCode leftTurnBtn, rightTurnBtn, gotoLeftWiperBtn, gotoRightWiperBtn, leftTurnSignalBtn, rightTurnSignalBtn, brakeBtn, accelBtn, klaxonBtn;

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

    // Update is called once per frame
    void Update()
    {
        

        UpdateWheelPhysics();
        UpdateWheelTransforms();
        if (Input.GetKeyDown(leftTurnBtn))
        {

        }
        if (Input.GetKeyDown(rightTurnBtn))
        {

        }
        if (Input.GetKeyDown(gotoLeftWiperBtn))
        {

        }
        if (Input.GetKeyDown(gotoRightWiperBtn))
        {

        }
        if (Input.GetKeyDown(leftTurnSignalBtn))
        {

        }
        if (Input.GetKeyDown(rightTurnSignalBtn))
        {

        }
        if (Input.GetKeyDown(brakeBtn))
        {

        }
        if (Input.GetKeyDown(accelBtn))
        {

        }
        if (Input.GetKeyDown(klaxonBtn))
        {

        }
    }
    /// <summary>
    /// Updates the wheel transforms.
    /// </summary>
    void UpdateWheelTransforms()
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

    /// <summary>
    /// Updates the wheel physics.
    /// </summary>
    void UpdateWheelPhysics()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        // accelerate
        rearRightCollider.motorTorque = v * engineForce;
        rearLeftCollider.motorTorque = v * engineForce;
        frontRightCollider.motorTorque = v * engineForce;
        frontLeftCollider.motorTorque = v * engineForce;

        // steer
        frontRightCollider.steerAngle = h * steerAngle;
        frontLeftCollider.steerAngle = h * steerAngle;

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
}

