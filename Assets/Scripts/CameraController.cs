using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset, rcOffset;
    public float followSpeed = 10;
    public float lookSpeed = 10;

    public bool isInRCMode = false;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            if (isInRCMode)
            {
                MoveToTarger(rcOffset);
            }
            else
            {
                MoveToTarger(offset);
                LookAtTarget();
            }
        }
    }

    private void LookAtTarget()
    {
        Vector3 _lookDirection = target.position + new Vector3(0,2,0) - transform.position;
        Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);
    }

    private void MoveToTarger(Vector3 _offset)
    {
        Vector3 _targetPos = target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
    }

}
