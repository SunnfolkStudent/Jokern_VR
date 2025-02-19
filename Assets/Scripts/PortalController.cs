using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class PortalController : MonoBehaviour
{
    [FormerlySerializedAs("Other")] public Transform OtherPortal;
    public Vector3 Offset;
    public Transform PlayerTransform;
	public TeleportationProvider playerTeleportController;
    private float RotY;

    private void OnTriggerEnter(Collider probablyPlayer)
    {
        print("Collided with " + probablyPlayer.name);
        if (!probablyPlayer.CompareTag("Player")) return;
        print("HIT PLAYER");
        
        // PlayerTransform = probablyPlayer.gameObject.GetComponent<PlayerMover>().GetTargetReference();
#if true
        SceneController.LoadNextLevel();
#else
        StartCoroutine(Teleport(probablyPlayer, 0.1f));
#endif
    }

    private IEnumerator Teleport(Collider probablyPlayer, float delay)
    {
        // print("Starting Wait");
        yield return new WaitForSeconds(delay);
        // print("Ending Wait");
        UpdatePosition(probablyPlayer);
        UpdateRotation(probablyPlayer);
        yield return null;
    }
    private void UpdateRotation(Collider probablyPlayer)
    {
        if (transform.localEulerAngles.y < probablyPlayer.transform.localEulerAngles.y)
        {
            RotY = OtherPortal.eulerAngles.y + probablyPlayer.transform.eulerAngles.y;
        }
        else
        {
            RotY = OtherPortal.eulerAngles.y - probablyPlayer.transform.eulerAngles.y;
        }
            
        var rotXZ = probablyPlayer.transform.eulerAngles;
        
        probablyPlayer.transform.localEulerAngles = new Vector3(rotXZ.x, RotY, rotXZ.z);
    }

    private void UpdatePosition(Collider probablyPlayer)
    {
        print("PlayerPos Before: " + probablyPlayer.transform.position);
        // probablyPlayer.transform.position = OtherPortal.position;
        Vector3 otherPosOffset = transform.position - probablyPlayer.transform.position;
        otherPosOffset.y = 0;
        // probablyPlayer.transform.position = OtherPortal.position + otherPosOffset;
        probablyPlayer.GetComponent<PlayerMover>().Move(OtherPortal.position + otherPosOffset);
        playerTeleportController.ForceTeleport(probablyPlayer.transform.position);
        print("PlayerPos After: " + probablyPlayer.transform.position);
        print("PlayerPos Target: " + (OtherPortal.position + otherPosOffset));
    }
}
