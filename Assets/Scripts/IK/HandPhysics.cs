using System;
using UnityEngine;

public class HandPhysics : MonoBehaviour
{
    public Transform target;
    [SerializeField]
    public Vector3 Pos;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
       //position
       rb.linearVelocity = (target.position - transform.position) / Time.fixedDeltaTime;
       Pos = target.position;
       
       //rotation
       Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
       rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
       
       Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;
       
       rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
    // private void FixedUpdate()
    // {
    //     rb.MoveRotation(target.rotation);
    //
    //     Vector3 moveToHandVec = target.position - transform.position;
    //     float neededSpeed = moveToHandVec.magnitude * (1.0f / Time.fixedDeltaTime);
    //     Vector3 neededSpeedVec = moveToHandVec.normalized * neededSpeed;
    //     Vector3 currentSpeedVec = rb.linearVelocity;
    //     Vector3 newSpeedVec = neededSpeedVec - currentSpeedVec;
    //     rb.AddForce(newSpeedVec, ForceMode.VelocityChange);
    // }
}
