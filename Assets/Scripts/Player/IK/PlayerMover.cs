using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Transform target;
    public Transform GetTargetReference()
    {
        return target;
    }
    public void Move(Vector3 position)
    {
        target.position = position - (transform.position - target.position);
    }
}
