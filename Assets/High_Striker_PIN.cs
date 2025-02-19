using System;
using UnityEngine;

public class High_Striker_PIN : MonoBehaviour
{
    [SerializeField] private bool Grounded = true;
    [SerializeField] private bool Won = true;
    [SerializeField] private Rigidbody rb;

    [SerializeField]
    private HIghStrikerForceFinder force;

    private void Start()
    {
        Grounded = true;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Grounded)
        {
            rb.linearVelocity = new Vector3(0,0,force.forceVector.z);
        } 
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Pillow")
        {
            Grounded = true;
        }

        if (other.gameObject.name == "Bell")
        {
            Won = true;
        }
    }
}
