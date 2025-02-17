using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public Transform Head;
    public Transform FloorReference;
    CapsuleCollider BodyCollider;
    void Start()
    {
        BodyCollider = GetComponent<CapsuleCollider>();
    }
    
    void Update()
    {
        float Height = Head.position.y - FloorReference.position.y;
        BodyCollider.height = Height;
        transform.position = Head.position - Vector3.up * Height / 2;
        print("hi mother");
    }
}
