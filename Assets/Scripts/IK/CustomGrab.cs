using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class CustomGrab : MonoBehaviour
{
    private SphereCollider grabZone;

    [SerializeField]
    private float grabDistanceExtension = 0;
    [SerializeField]
    private float throwForce = 2f;
    
    [Header("Held Item Debug:")]
    [SerializeField]
    private Transform heldTransform;
    private Renderer heldRenderer;
    private Vector3 heldTarget;
    private Vector3 heldSize;
    private int heldLayer;
    
    [SerializeField]
    private Rigidbody heldRigidbody;
    [SerializeField]
    private bool holdingSomething = false;
    
    // [SerializeField]
    private InputAction gripButton;
    
    [Header("Controller References:")]
    public InputActionProperty grabInputAction;
    public InputActionProperty gyroVelocityInput;
    public InputActionProperty gyroAngularVelocityInput;
    [SerializeField] private HapticImpulsePlayer Haptic;

    // [SerializeField]
    private Vector3 gyroVel;
    private Vector3 gyroAngVel;


    private void Start()
    { 
        grabZone = GetComponent<SphereCollider>();
        gripButton = grabInputAction.action; 
    }

    private void Update()
    {
        if (gripButton.WasPressedThisFrame() || Keyboard.current.kKey.wasPressedThisFrame)
        {
            Grab(true);
        }
        else if (gripButton.WasReleasedThisFrame() || Keyboard.current.kKey.wasReleasedThisFrame)
        {
            Grab(false);
        }
    }

    private void FixedUpdate()
    {
        if (holdingSomething)
        {
            //      Target & Vars       //
            gyroVel = gyroVelocityInput.action.ReadValue<Vector3>();
            gyroAngVel = gyroAngularVelocityInput.action.ReadValue<Vector3>();
            heldTarget = transform.position + transform.forward * heldSize.x/3 + transform.right * -heldSize.z/3;
            
            //      position        //
            heldRigidbody.linearVelocity = (heldTarget - heldTransform.position) / Time.fixedDeltaTime;
            
            //      rotation       // 
            Quaternion rotationDifference = transform.rotation * Quaternion.Inverse(heldTransform.rotation);
            rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
       
            // Ensure the shortest rotation path (ChatGPT)
            if (angleInDegree > 180f)
            { angleInDegree -= 360f; } // Flip to the shorter negative rotation 

            Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;
       
            heldRigidbody.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
        }
    }
    private void Grab(bool grab)
    {
        if (grab)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position + grabZone.center, grabZone.radius + grabDistanceExtension);
            foreach (var hit in hitColliders)
            {
                if (hit.GetComponent<CustomGrabbable>())
                {
                    Haptic.SendHapticImpulse(0.1f, 0.1f);
                    print("Grabbed something grabbable");
                    holdingSomething = true;
                    heldTransform = hit.transform;
                    heldRigidbody = hit.GetComponent<Rigidbody>();
                    heldRenderer = hit.GetComponent<Renderer>();
                    heldSize = heldRenderer.bounds.size; // World space size
                    
                    heldLayer = heldTransform.gameObject.layer;
                    int handLayer = LayerMask.NameToLayer("Right Hand Physics");
                    heldTransform.gameObject.layer = handLayer;
                }
            }
        }
        else
        {
            if (holdingSomething)
            {
                heldRigidbody.linearVelocity = gyroVel * throwForce;
                heldTransform.gameObject.layer = heldLayer;
            }
            holdingSomething = false;
        }
    }
}