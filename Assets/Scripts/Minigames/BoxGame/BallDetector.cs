using System;
using UnityEngine;

public class BallDetector : MonoBehaviour
{
    private Transform _ball;
    private Rigidbody _rigidbody;
    
    private Vector3 _ballPosition;
    private Quaternion _ballRotation;

    public bool isUsed;

    private void Start()
    {
        _ball = GetComponent<Transform>(); //gets the position of the ball
        _ballPosition = _ball.position; //saves the balls position so it can return on reset
        _ballRotation = _ball.rotation; //saves the balls rotation so it can return on reset this is semi unnecessary
        _rigidbody = GetComponent<Rigidbody>(); //gets the rigidbody of the ball
        isUsed = false;
    }
    
    public void ResetTransform() //a method that makes it possible for the boxGameManager to reset the ball back to start pos
    {
        _ball.position = _ballPosition;
        _ball.rotation = _ballRotation;
        _rigidbody.linearVelocity = new Vector3(0, 0, 0);
        _rigidbody.angularVelocity = new Vector3(0, 0, 0);
        isUsed = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wood"))
        {
           FMODController.PlaySoundFrom(JokernVRSound.SFX_BallCollisionWoodWeak, this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
           FMODController.PlaySoundFrom(JokernVRSound.SFX_BallCollisionGroundStrong, this.gameObject);
        }
    }
}
