using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parcel : MonoBehaviour
{
    [SerializeField] private string m_Address;
    [SerializeField] private int m_Value;

    [SerializeField] private float m_Speed = 5f;
    [SerializeField] private Transform m_TargetPosition;
    private Rigidbody m_RB;

    private void Start()
    {
        m_RB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (m_TargetPosition != null)
        {
            m_RB.velocity = (m_TargetPosition.position - transform.position) * m_Speed * Time.deltaTime;
        }
    }
    
    public void SetTargetPosition(Transform _targetPosition) { m_TargetPosition = _targetPosition; }
    public string GetAddress() { return m_Address; }
    public int GetValue() { return m_Value; }
}
