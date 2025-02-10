using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;

    public Transform reciever;

    private bool playerIsOverlapping = false;
    
    private void Update()
    {
        if (playerIsOverlapping)
        {
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        Vector3 portalToPlayer = player.position - transform.position;
        float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

        if (dotProduct < 0f)
        {
            float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
            rotationDiff += 180;
            player.Rotate(Vector3.up, rotationDiff);

            Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
            player.position = reciever.position + positionOffset;
                
            playerIsOverlapping = false;
        }
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
