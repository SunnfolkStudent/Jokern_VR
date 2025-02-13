using System;
using UnityEngine;

public class BellBeller : MonoBehaviour
{
    public CapsuleCollider capsuleCollider;
    public Vector3 vectorForce;

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void OnCollisionEnter(Collision col)
    {
        Vector3 hitForce = col.impulse / Time.fixedDeltaTime;
        vectorForce = hitForce; //gives a number depending on how hard the bell is hit
        Debug.Log(vectorForce);
    }
}
