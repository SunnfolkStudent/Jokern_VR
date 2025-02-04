using System;
using UnityEngine;

public class IKPhysicsRig : MonoBehaviour
{
    // public Transform playerHead;
    public Transform leftController;
    public Transform rightController;
    [SerializeField, Tooltip("Displays the target's position in the Inspector")]
    private Vector3 targetPosition;

    [SerializeField, Tooltip("Displays the target's position in the Inspector")]
    private String name;

    // public ConfigurableJoint headJoint;
    public ConfigurableJoint leftHandJoint;
    public ConfigurableJoint rightHandJoint;
    
    private void FixedUpdate()
    {
        name = leftController.name;
        targetPosition = leftController.position;
        leftHandJoint.targetPosition = leftController.position;
        leftHandJoint.targetRotation = leftController.rotation;
        
        rightHandJoint.targetPosition = rightController.position;
        rightHandJoint.targetRotation = rightController.rotation;
    }
}
