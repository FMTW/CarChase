using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParcelDropoff : MonoBehaviour
{
    [SerializeField] private string m_Address;
    private GameObject m_GameManager;

    private void Start()
    {
        m_GameManager = GameObject.FindGameObjectWithTag("GameController");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Parcel"))
        {
            if (other.GetComponent<Parcel>().GetAddress() == m_Address)
            {
                Debug.Log("Address match.");
                m_GameManager.GetComponent<GameManager>().CompleteDelivery(other.GetComponent<Parcel>().GetValue());
            }
            else
            {
                Debug.Log("Address does not match.");
            }

        }
    }
}
