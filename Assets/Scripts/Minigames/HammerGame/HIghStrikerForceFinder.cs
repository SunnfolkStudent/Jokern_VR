using System;
using UnityEngine;

public class HIghStrikerForceFinder : MonoBehaviour
{
    public BoxCollider _boxCollider;
    public Vector3 forceVector;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    void OnCollisionEnter(Collision col)
    {
        Vector3 hitForce = col.impulse / Time.fixedDeltaTime; //checks the velocity of the object that it collided with and sets hit force to it
        forceVector = hitForce;
        print(hitForce);
    }
    void OnCollisionExit(Collision other)
    {
        forceVector = Vector3.zero; //makes sure when hammer is removed to reset the force on the weight
    }
}
