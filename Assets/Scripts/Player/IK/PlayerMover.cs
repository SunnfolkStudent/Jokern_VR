using System;
using UnityEngine;

// THIS IS FOR PORTAL TELEPORTING - TRYM
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Start()
    {
        target = GameObject.Find("Player Rig").GetComponent<Transform>();
    }

    public Transform GetTargetReference()
    {
        return target;
    }
    public void Move(Vector3 position)
    {
        target.position = position - (transform.position - target.position);
    }
}
