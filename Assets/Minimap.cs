using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] private Transform m_PlayerPosition;

    void Start()
    {
        m_PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        this.transform.position = m_PlayerPosition.position;
    }
}
