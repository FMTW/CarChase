using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomVehicleController : MonoBehaviour
{

    [Serializable]
    public class WheelSettings {

    }

    public WheelCollider leftFrontW, rightFrontW, leftRearW, rightRearW;
    public Transform leftFrontT, rightFrontT, leftRearT, rightRearT;
    public float maxSteerAngle = 30f;
    public float motorForce = 50;

    private float m_inputHorizontal, m_inputVertical;
    private float m_steeringAngle;

    private Rigidbody rb;

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();        
    }

    public void GetInput()
    {
        m_inputHorizontal = Input.GetAxis("Horizontal");
        m_inputVertical = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        m_steeringAngle = maxSteerAngle * m_inputHorizontal;
        leftFrontW.steerAngle = m_steeringAngle;
        rightFrontW.steerAngle = m_steeringAngle;
        
    }

    private void Accelerate()
    {
        leftRearW.motorTorque = motorForce * m_inputVertical;
        rightRearW.motorTorque = motorForce * m_inputVertical;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(leftFrontW, leftFrontT);
        UpdateWheelPose(rightFrontW, rightFrontT);
        UpdateWheelPose(leftRearW, leftRearT);
        UpdateWheelPose(rightRearW, rightRearT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }
}
