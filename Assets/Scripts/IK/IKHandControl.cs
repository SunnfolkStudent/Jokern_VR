using UnityEngine;

public class IKHandControl : MonoBehaviour
{
    public Transform trackedTransform = null;
    public Rigidbody body = null;
    public float positionStrength = 20;
    public float rotationStrength = 30;
    void FixedUpdate()
    {
        var vel = (trackedTransform.position - body.position).normalized * positionStrength * Vector3.Distance(trackedTransform.position, body.position);
        body.linearVelocity = vel;
        float kp = (6f * rotationStrength) * (6f * rotationStrength) * 0.25f;
        float kd = 4.5f * rotationStrength;
        Vector3 x;
        float xMag;
        Quaternion q = trackedTransform.rotation * Quaternion.Inverse(transform.rotation);
        q.ToAngleAxis(out xMag, out x);
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
