using UnityEngine;
using UnityEngine.Serialization;

public class PortalController : MonoBehaviour
{
    [FormerlySerializedAs("Other")] public Transform OtherPortal;
    public Vector3 Offset;
    private float RotY;

    private void OnTriggerEnter(Collider probablyPlayer)
    {
        if (!probablyPlayer.CompareTag("Player")) return;
        
        probablyPlayer.transform.position = OtherPortal.position;
        
        UpdateRotation(probablyPlayer);
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
}
