using UnityEngine;

public class BoxDetector : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _boxLocation;
    public bool boxHasMoved;
    private Vector3 _boxStartPosition;
    private Quaternion _boxStartRotation;
    private bool _initialized;

    private void Start()
    {
       _rigidbody = GetComponent<Rigidbody>(); //gets the rigidbody component from the box
       _boxLocation = GetComponent<Transform>(); //gets the transform from the box
       _boxStartPosition = _boxLocation.position; //saves the box position so it can return on reset
       _boxStartRotation = _boxLocation.rotation; //saves the box rotation so it can return on reset
       boxHasMoved = false; //makes the boxHasMoved variable false
    }
    
    private void Update()
    {
        if (_rigidbody.linearVelocity.x > 0.1f && _initialized == false) //checks if the box has moved and if it has already been moved before
        {
            boxHasMoved = true; //changes the boxHasMoved variable to true
            _initialized = true; //changes the initialized variable to true thus disabling the scripts ability to change boxHasMoved to true
        }
    }
    
    public void ResetTransform() //a method that makes it possible for the boxGameManager to reset the ball back to start pos
    {
        _boxLocation.position = _boxStartPosition;
        _boxLocation.rotation = _boxStartRotation;
        _rigidbody.linearVelocity = new Vector3(0, 0, 0);
        _rigidbody.angularVelocity = new Vector3(0, 0, 0);
        _initialized = false;
    }
}