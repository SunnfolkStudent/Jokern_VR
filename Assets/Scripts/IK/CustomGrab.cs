using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    [SerializeField]
    private Transform heldTransform;
    private Renderer heldRenderer;
    private Vector3 heldTarget;
    private Vector3 heldSize;
    
    [SerializeField]
    private Rigidbody heldRigidbody;
    [SerializeField]
    private bool holdingSomething = false;
    [SerializeField]
    private float throwForce = 2f;
    
    [SerializeField]
    private InputAction gripButton;
    
    public InputActionProperty triggerAction;
    public InputActionProperty gyroInput;
    public InputActionProperty gyroInputRot;

    [SerializeField]
    private Vector3 gyroVel;
    private Vector3 gyroRotVel;


    private void Start()
    {
       gripButton = triggerAction.action; 
    }

    private void Update()
    {
        gyroVel = gyroInput.action.ReadValue<Vector3>();
        gyroRotVel = gyroInputRot.action.ReadValue<Vector3>();
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
            heldTarget = transform.position + transform.forward * heldSize.x/3 + transform.right * heldSize.z/3;
            
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
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 100);
            foreach (var hit in hitColliders)
            {
                if (hit.GetComponent<CustomGrabbable>())
                {
                    print("Grabbed something grabbable");
                    holdingSomething = true;
                    heldTransform = hit.transform;
                    heldRigidbody = hit.GetComponent<Rigidbody>();
                    heldRenderer = hit.GetComponent<Renderer>();
                    heldSize = heldRenderer.bounds.size; // World space size
                }
            }
        }
        else
        {
            if (holdingSomething)
            {
                heldRigidbody.linearVelocity = gyroVel * throwForce;
            }
            holdingSomething = false;
        }
    }
}