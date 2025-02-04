using UnityEngine;

public class IKHandControl1 : MonoBehaviour
{
    public Transform trackedTransform = null;
    public Rigidbody body = null;
    
    public float positionStrength = 20f;
    public float rotationStrength = 30f;
    public float positionDamping = 10f;  // New: Damping for position
    public float rotationDamping = 5f;   // New: Damping for rotation
    
    private Vector3 velocity = Vector3.zero; // New: Used for smooth position damping

    void FixedUpdate()
    {
        // Position Control with Smooth Damping
        Vector3 targetPos = trackedTransform.position;
        Vector3 smoothedPosition = Vector3.SmoothDamp(body.position, targetPos, ref velocity, 1f / positionDamping);
        Vector3 vel = (smoothedPosition - body.position) * positionStrength;
        body.linearVelocity = vel;

        // Rotation Control with Damping
        float kp = (6f * rotationStrength) * (6f * rotationStrength) * 0.25f;
        float kd = 4.5f * rotationStrength + rotationDamping; // Increased damping

        Quaternion q = trackedTransform.rotation * Quaternion.Inverse(transform.rotation);
        q.ToAngleAxis(out float xMag, out Vector3 x);
        
        if (xMag > 180f) xMag -= 360f; // Normalize angle to prevent jumps
        x.Normalize();
        x *= Mathf.Deg2Rad;
        
        Vector3 pidv = kp * x * xMag - kd * body.angularVelocity;
        Quaternion rotInertia2World = body.inertiaTensorRotation * transform.rotation;
        pidv = Quaternion.Inverse(rotInertia2World) * pidv;
        pidv.Scale(body.inertiaTensor);
        pidv = rotInertia2World * pidv;
        
        body.AddTorque(pidv);
    }
}
