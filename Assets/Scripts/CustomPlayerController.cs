using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayerController : MonoBehaviour
{


    private Rigidbody rb;

    #region Player Object Interaction Setting
    [Header("Player Object Interaction Setting")]
    [SerializeField] private GameObject intereactionText;
    private Transform playerCameraTransform;
    private RaycastHit playerRaycastHit;
    [SerializeField] private float playerRaycastDistance = 2f;
    public LayerMask m_IntereactableObject;
    [SerializeField] private string m_RaycastHitObjectTag;

    #region Parcel Setting
    [Header("Parcel Setting")]
    private bool m_IsPlayerHoldingParcel = false;
    private GameObject m_Parcel;
    [SerializeField] private Transform m_HeldItemPosition;
    #endregion

    #region Vehicle Setting
    [Header("Vehicle Setting")]
    private bool m_IsPlayerInVehicle = false;
    private GameObject m_PlayerVehicle;
    private Transform m_DriverSeat;
    #endregion

    #endregion
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.TransformDirection(Vector3.forward), out playerRaycastHit, playerRaycastDistance, m_IntereactableObject))
        {
            Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.TransformDirection(Vector3.forward) * playerRaycastHit.distance, Color.red);
            intereactionText.SetActive(true);
            m_RaycastHitObjectTag = playerRaycastHit.collider.transform.tag;
            
        }
        else
        {
            Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.TransformDirection(Vector3.forward) * playerRaycastDistance, Color.green);
            intereactionText.SetActive(false);
            m_RaycastHitObjectTag = null;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (m_RaycastHitObjectTag)
            {
                case "PlayerVehicle":
                    m_PlayerVehicle = playerRaycastHit.transform.gameObject;
                    m_DriverSeat = m_PlayerVehicle.GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>().GetDriverSeatTransform();

                    if (!m_IsPlayerInVehicle)
                        GetInVehicle();
                    else
                        GetOutVehicle();
                    break;

                case "Parcel":
                    m_Parcel = playerRaycastHit.transform.gameObject;
                    if (!m_IsPlayerHoldingParcel && m_Parcel != null)
                        PickupParcel();
                    else
                        DropParcel();
                    break;

                case "VehicleCargo":
                    if (m_Parcel.activeSelf && m_IsPlayerHoldingParcel)
                    {
                        Debug.Log("Parcel stored in cargo.");
                        m_Parcel.SetActive(false);
                    }
                    else
                    {
                        Debug.Log("Parcel takeout from cargo.");
                        m_Parcel.SetActive(true);
                    }
                    break;

                default:
                    Debug.Log("No matched interaction found.");
                    break;
            }
        }
    }

    #region Pickup and Dropoff Parcel
    private void PickupParcel()
    {
        Debug.Log("Parcel picked up");
        m_IsPlayerHoldingParcel = true;

        m_Parcel.GetComponent<Rigidbody>().isKinematic = true;
        m_Parcel.transform.position = Vector3.Lerp(m_Parcel.transform.position, m_HeldItemPosition.position, 100);
        m_Parcel.transform.SetParent(this.gameObject.transform);

    }

    private void DropParcel()
    {
        Debug.Log("Parcel dropped");
        m_IsPlayerHoldingParcel = false;

        m_Parcel.GetComponent<Rigidbody>().isKinematic = false;
        m_Parcel.transform.SetParent(null);
    }
    #endregion

    #region GetIn and GetOut Vehicle Functions
    private void GetInVehicle()
    {
        Debug.Log("Got in playerVehicle");
        m_IsPlayerInVehicle = true;

        #region Do stuffs to player
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;                                           // Set player to kinematic
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;                                        // Disable player collider
        this.gameObject.GetComponent<FirstPersonAIO>().playerCanMove = false;                                   // Set player's playerCanMove to false
        this.gameObject.GetComponent<FirstPersonAIO>().useHeadbob = false;                                      // Set player's useHeadbob to false
        this.gameObject.transform.position = m_DriverSeat.position;                                             // Move player to driverSeat
        this.gameObject.transform.rotation = m_DriverSeat.rotation;                                             // Rotate player to driverSeat direaction
        this.gameObject.transform.SetParent(m_PlayerVehicle.transform);                                         // Parent player under playerVehicle
        #endregion

        #region Do stuffs to playerVehicle
        m_PlayerVehicle.GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>().ToggleDrivable();       // Enable playerVehicle drivable
        #endregion

    }

    private void GetOutVehicle()
    {
        Debug.Log("Got out playerVehicle");
        m_IsPlayerInVehicle = false;

        #region Do stuffs to player
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;                                          // Set player to not kinematic
        this.gameObject.GetComponent<CapsuleCollider>().enabled = true;                                         // Enable player collider
        this.gameObject.GetComponent<FirstPersonAIO>().playerCanMove = true;                                    // Set player's playerCanMove to true
        this.gameObject.GetComponent<FirstPersonAIO>().useHeadbob = true;                                       // Set player's useHeadbob to true
        this.gameObject.transform.SetParent(null);                                                              // Parent player under world
        #endregion

        #region Do stuffs to playerVehicle
        m_PlayerVehicle.GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>().ToggleDrivable();       // Diable playerVehicle drivable
        #endregion
    }
    #endregion
}
