using System;
using UnityEngine;
using UnityEngine.Rendering;

public class FogGateController : MonoBehaviour
{
    // [SerializeField] private Transform LeftDoor;
    // [SerializeField] private Transform RightDoor;
    [SerializeField] private HingedDoor LeftDoor;
    [SerializeField] private HingedDoor RightDoor;
    [SerializeField] private HingedDoor[] Doors;

    [SerializeField] private FogGatePSController FogGateDoor;
    [SerializeField] private FogGatePSController FogGateFill;

    private void Start()
    {   
        Doors = new HingedDoor[]{LeftDoor, RightDoor};
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Door Collided with: " + other.name);
        if (other.CompareTag("Player"))
        {
            OpenDoors();
            if (FogGateDoor.spawn == false)
            {
                FogGateDoor.spawn = true; 
                FogGateDoor.SpawnFogGate();
            }
            if (FogGateFill.spawn == false)
            {
                FogGateFill.spawn = true; 
                FogGateFill.SpawnFogGate();
            }
        } 
    }

    void OpenDoors()
    {
        LeftDoor.shouldBeOpen = true;
        RightDoor.shouldBeOpen = true;
        LeftDoor.moveSpeed = 70f;
        RightDoor.moveSpeed = 70f;
    }
}
