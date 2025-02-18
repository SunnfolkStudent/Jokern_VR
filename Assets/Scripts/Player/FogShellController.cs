using System;
using UnityEngine;

public class FogShellController : MonoBehaviour
{
    [SerializeField] private Transform trackingTarget;
    public bool isFogOn = false;

    private void Update()
    {
       transform.position = trackingTarget.position; 
    }
}
