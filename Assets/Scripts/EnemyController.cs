using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 22f;
    public float steerRate = 10f;
    public float m_bounceForce = 100f;

    private Transform m_target;
    private Rigidbody rb;

    void Start()
    {
        if (m_target == null)
            m_target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        #region Steering
        Vector3 _lookDirection = m_target.position - transform.position;
        Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, _rot, steerRate * Time.deltaTime);
        #endregion

        #region Acceleration
        rb.velocity = transform.forward * speed;
        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Vector3 collisionDirection = _collision.transform.position - transform.position;
            rb.AddForce(-collisionDirection * m_bounceForce);
        }
    }

}
