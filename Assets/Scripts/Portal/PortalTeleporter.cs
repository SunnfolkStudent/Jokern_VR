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
            
        }
    }



    IEnumerator Teleporting()
    {
        isTeleporting = true;
        
        player.position = reciever.position;
        
        yield return new WaitForSeconds(teleportCooldown);
        
        isTeleporting = false;
    }
    

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
