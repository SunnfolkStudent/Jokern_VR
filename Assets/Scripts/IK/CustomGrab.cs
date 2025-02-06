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
    private void Start()
    {
    }

    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            if (!holdingSomething)
            {
                print("Pressing K");
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1);
                print(hitColliders.Length + ": objects in range");
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
                holdingSomething = false;
            }
            
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
}
