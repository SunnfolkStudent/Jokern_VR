using UnityEngine;
using UnityEngine.InputSystem;

public class GrittyMovement : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            transform.position -= transform.right * (Time.deltaTime * 4f);
        }
    }
}