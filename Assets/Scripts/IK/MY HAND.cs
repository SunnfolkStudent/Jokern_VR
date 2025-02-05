using System;
using UnityEngine;

public class MYHAND : MonoBehaviour
{
    private Rigidbody rb;
    public Transform trackingTarget;
    [SerializeField]
    private Vector3 targetPosition;
    [SerializeField]
    private bool stopped;

    public float PushingForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        targetPosition = trackingTarget.position;
        if (stopped)
        {
            // OldFunc();
            rb.linearVelocity = (rb.linearVelocity + ((trackingTarget.position - transform.position).normalized * PushingForce) / Time.fixedDeltaTime)/2;
            
            //rotation
            Quaternion rotationDifference = trackingTarget.rotation * Quaternion.Inverse(transform.rotation);
            rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
            
            Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;
            
            rb.angularVelocity = ((rotationDifferenceInDegree * Mathf.Deg2Rad).normalized * PushingForce) / Time.fixedDeltaTime;
            // rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad) / Time.fixedDeltaTime;
        }
        else
        {
            rb.position = trackingTarget.position;
            rb.rotation = trackingTarget.rotation;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        stopped = true;
    }
    // private void OnCollisionExit(Collision other)
    // {
    //     stopped = false;
    // }
    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = (trackingTarget.position - transform.position).normalized * PushingForce;
        Gizmos.DrawRay(transform.position, direction);
    }

    private void OldFunc()
    {
       rb.linearVelocity = (trackingTarget.position - transform.position) / Time.fixedDeltaTime;
       
       //rotation
       Quaternion rotationDifference = trackingTarget.rotation * Quaternion.Inverse(transform.rotation);
       rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
       
       Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;
       
       rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
