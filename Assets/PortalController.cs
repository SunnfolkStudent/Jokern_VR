using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform Other;
    public Vector3 Offset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Offset = other.transform.position - Other.position;
            other.transform.position = new Vector3(Offset.x, other.transform.position.y, Other.position.z);
        }
    }
}
