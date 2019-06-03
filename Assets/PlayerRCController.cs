using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRCController : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float maxSteerRate = 90f;
    public float m_bounceForce = 10f;

    public Transform frontLeftT, frontRightT;

    private float m_inputHorizontal, m_inputVertical;

    private float m_currentAngle;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        GetInput();
        Move();
        //UpdateWheelPoses();
    }

    public void GetInput()
    {
        m_inputHorizontal = Input.GetAxis("Horizontal");
        m_inputVertical = Input.GetAxis("Vertical");
    }

    private void Move()
    {
        #region Steering
        transform.Rotate(Vector3.up * maxSteerRate * m_inputHorizontal * Time.deltaTime);
        #endregion

        #region Acceleration
        rb.velocity = transform.forward * maxSpeed * m_inputVertical;
        #endregion

    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftT);
        UpdateWheelPose(frontRightT);
    }

    private void UpdateWheelPose(Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Vector3 collisionDirection = _collision.transform.position - transform.position;
            rb.AddForce(-collisionDirection * m_bounceForce);
        }
    }
}
