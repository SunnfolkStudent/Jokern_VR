using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PortalTeleporter : MonoBehaviour
{ 
    [Tooltip("This is for the parent player object")]
    public Transform player;
    
    [Tooltip("This is for the Main Camera")]
    public Transform vrCamera;
    
    [Tooltip("This is for the other portal collider")]
    public Transform reciever;

    public float teleportCooldown = 1f;

    private bool playerIsOverlapping = false;
    [SerializeField] private bool isTeleporting = false;
    
    private void Update()
    {
        if (playerIsOverlapping && !isTeleporting)
        {
            //StartCoroutine(TeleportPlayerButBetter());
            TeleportPlayer();
            Debug.Log(isTeleporting);
        }
    }

    private void TeleportPlayer()
    {
        isTeleporting = true;
        
        Vector3 portalToPlayer = player.position - transform.position;
        float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

        Debug.Log(dotProduct);
        
        if (dotProduct < 0f)
        {
            float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
            rotationDiff += 180;
            vrCamera.Rotate(Vector3.up, rotationDiff);
    
            Debug.Log($"BEFORE: {player.position}");
            
            Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
            player.position = reciever.position + positionOffset;
            
            Debug.Log($"AFTER: {player.position}");
                
            playerIsOverlapping = false;
            isTeleporting = false;
        }
    }

    IEnumerator SimpleTeleport()
    {
        isTeleporting = true;
        
        player.position = reciever.position;

        yield return new WaitForSeconds(.2f);
        isTeleporting = false;
    }

    // IEnumerator TeleportPlayerButBetter()
    // {
    //     isTeleporting = true;
    //     Vector3 portalToPlayer = player.position - transform.position;
    //     float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
    //     
    //     Debug.Log(portalToPlayer);
    //     Debug.Log(dotProduct);
    //
    //     if (dotProduct < 0f)
    //     {
    //         float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
    //         rotationDiff += 180;
    //         player.Rotate(Vector3.up, rotationDiff);
    //
    //         Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
    //         player.position = reciever.position + positionOffset;
    //             
    //         playerIsOverlapping = false;
    //         isTeleporting = false;
    //         
    //         yield return new WaitForSeconds(teleportCooldown);
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOverlapping = false;
        }
    }
}
