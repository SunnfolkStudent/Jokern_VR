using System;
using Unity.Mathematics;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;

    private void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        var newOffsetX = playerOffsetFromPortal.x;
        var newOffsetY = playerOffsetFromPortal.y;
        var newOffsetZ = playerOffsetFromPortal.z;
        
        //transform.position = new Vector3(newOffsetX, newOffsetY, newOffsetZ);
        
        transform.position = portal.position + playerOffsetFromPortal;
        
        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        
        Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationDifference * playerCamera.forward;
        
        //transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        var newCameraDirX = -newCameraDirection.x;
        var newCameraDirY = newCameraDirection.y;
        var newCameraDirZ = newCameraDirection.z;
        
        Vector3 newCameraDirectionForRealThisTime = new Vector3(newCameraDirX, newCameraDirY, newCameraDirZ);
        
        //transform.rotation = Quaternion.LookRotation(newCameraDirectionForRealThisTime, Vector3.up);
    }
}
