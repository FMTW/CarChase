using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        private float h, v;
        [SerializeField] private bool isDrivable = false;
        [SerializeField] private Transform driverSeat;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }

        private void FixedUpdate()
        {
            // pass the input to the car!
            if (isDrivable)
            {
                h = CrossPlatformInputManager.GetAxis("Horizontal");
                v = CrossPlatformInputManager.GetAxis("Vertical");
            }
            else
            {
                h = 0;
                v = 0;
            }

            #if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
            #else
            m_Car.Move(h, v, v, 0f);
            #endif
        }

        public void ToggleDrivable()
        {
            if (isDrivable)
                isDrivable = false;
            else
                isDrivable = true;
        }

        public Transform GetDriverSeatTransform()
        {
            return driverSeat;
        }
    }
}
