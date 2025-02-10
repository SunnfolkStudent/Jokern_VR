using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class HandPhysics : MonoBehaviour
{
    public Transform target;
    [SerializeField] private Vector3 targetPos;
    private Rigidbody rb;

    public Renderer nonPhysicalHand;
    public float showNonPhysicalHandDistance = 0.05f;

    [SerializeField] private HapticImpulsePlayer Haptic;
    [SerializeField] private float HapticAmplitude = 1;
    [SerializeField] private float HapticDuration = 0.1f;
    [SerializeField] private float HapticFrequency = 0;
    [SerializeField] private float HapticBooster = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
       float distance = Vector3.Distance(transform.position, target.position);
       if (distance > showNonPhysicalHandDistance)
       // if(true)
       { nonPhysicalHand.enabled = true; }
       else
       { nonPhysicalHand.enabled = false; }
    }

    private void FixedUpdate()
    {
       //position
       // targetPos = target.position;
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
    
    private void OnCollisionEnter(Collision collision)
    {
        // foreach (ContactPoint contact in collision.contacts)
        // {
        //     Debug.DrawRay(contact.point, contact.normal, Color.white);
        // }

        // Haptic.SendHapticImpulse(collision.relativeVelocity.magnitude, 1f);
        // Haptic.SendHapticImpulse(1, 1f);
        // Haptic.SendHapticImpulse(HapticAmplitude, HapticDuration, HapticFrequency);
        float intensity = Mathf.Clamp(collision.relativeVelocity.magnitude * HapticBooster, 0, 1);
        Debug.Log("Intensity: " + intensity);
        Haptic.SendHapticImpulse(intensity, HapticDuration, HapticFrequency);
        // if (collision.relativeVelocity.magnitude > 2)
        // {
        // }
    } 
}
