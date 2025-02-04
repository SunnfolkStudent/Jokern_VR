using System;
using UnityEngine;

public class IKSprunged : MonoBehaviour
{
    public Transform trackingTarget;
    // public Rigidbody body;
    public ConfigurableJoint joint;
    public SpringJoint springJoint;

    private void FixedUpdate()
    {
       // joint.targetPosition = trackingTarget.position; 
       // joint.targetRotation = trackingTarget.rotation; 
    }
}
