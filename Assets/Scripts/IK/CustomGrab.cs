using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    [SerializeField]
    private Transform heldTransform;
    private Vector3 heldTarget;
    private Renderer heldRenderer;
    private Vector3 heldSize;
    [SerializeField]
    private Rigidbody heldRigidbody;
    [SerializeField]
    private bool holdingSomething = false;
    
    public InputActionProperty triggerAction;
    // private Vector3 heldDirection;
    // private Vector3 heldDirection;
    private RingBuffer<Vector3> heldVelocityBuffer;

    [SerializeField]
    // private Vector3 gripPos;
    private InputAction gripButton;

    private void Start()
    {
       gripButton = triggerAction.action; 
       heldVelocityBuffer = new RingBuffer<Vector3>(10);
    }

    // var inputDevices = new List<UnityEngine.XR.InputDevice>();
    // UnityEngine.XR.InputDevices.GetDevices(inputDevices);
    //
    // foreach (var device in inputDevices)
    // {
    //     Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
    // }
    

    private void Update()
    {
        // gripPos = triggerAction.action.ReadValue<Vector3>();
        // if (Keyboard.current.kKey.wasPressedThisFrame)
        // if(Keyboard)
        if (gripButton.WasPressedThisFrame())
        {
            Grab(true);
        }
        else if (gripButton.WasReleasedThisFrame())
        {
            Grab(false);
        }
    }

    private void FixedUpdate()
    {
        if (holdingSomething)
        {
            // heldTarget = transform.position + transform.forward * 0.5f + transform.right * 0.5f;
            // heldTarget = transform.position + transform.forward * heldSize.x/2 - offsetx + transform.right * heldSize.z/2 - offsety;
            heldTarget = transform.position + transform.forward * heldSize.x/3 + transform.right * heldSize.z/3;
            
            //position
            heldRigidbody.linearVelocity = (heldTarget - heldTransform.position) / Time.fixedDeltaTime;
            heldVelocityBuffer.Add((heldTarget - heldTransform.position) / Time.fixedDeltaTime);
       
            //rotation
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
            // print("Pressing K");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 100);
            // print(hitColliders.Length + ": objects in range");
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
                heldRigidbody.linearVelocity = heldVelocityBuffer.Get(0);
            }
            holdingSomething = false;
        }
    }
}
