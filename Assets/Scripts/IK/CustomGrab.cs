using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    [SerializeField]
    private Transform heldTarget;
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
                        heldTarget = hit.transform;
                        heldRigidbody = hit.GetComponent<Rigidbody>();
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
            //position
            heldRigidbody.linearVelocity = (transform.position - heldTarget.position) / Time.fixedDeltaTime;
       
            //rotation
            Quaternion rotationDifference = transform.rotation * Quaternion.Inverse(heldTarget.rotation);
            rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
       
            // Ensure the shortest rotation path (ChatGPT)
            if (angleInDegree > 180f)
            {
                angleInDegree -= 360f; // Flip to the shorter negative rotation
            }
       
            Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;
       
            heldRigidbody.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
        }
    }
}
