using System;
using UnityEngine;

public class HandPhysics : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;

    public Renderer nonPhysicalHand;
    public float showNonPhysicalHandDistance = 0.05f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
       float distance = Vector3.Distance(transform.position, target.position);
       // if (distance > showNonPhysicalHandDistance)
       if(true)
       { nonPhysicalHand.enabled = true; }
       else
       { nonPhysicalHand.enabled = false; }
    }

    private void FixedUpdate()
    {
       //position
       rb.linearVelocity = (target.position - transform.position) / Time.fixedDeltaTime;
       
       //rotation
       Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
       rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
       
       // Ensure the shortest rotation path (ChatGPT)
       if (angleInDegree > 180f)
       {
           angleInDegree -= 360f; // Flip to the shorter negative rotation
       }
       
       Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;
       
       rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
