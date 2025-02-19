using UnityEngine;

public class BoxDetector : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _boxLocation;
    public bool boxHasMoved;
    private Vector3 _boxStartPosition;
    private Quaternion _boxStartRotation;
    private bool _initialized;
    [SerializeField] private float thresholdOffset = 0.5f;

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
        // if (_rigidbody.linearVelocity.x > 0.1f && _initialized == false) //checks if the box has moved and if it has already been moved before
        if (Vector3.Distance(_boxStartPosition, transform.position) > thresholdOffset  && _initialized == false) //checks if the box has moved and if it has already been moved before
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
    
    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Balls"))
        {
            print("Play BALL HIT SOUNDEFFECT");
           // FMODController.PlaySound(JokernVRSound.SFX_BallCollisionCanStrong); 
           FMODController.PlaySoundFrom(JokernVRSound.SFX_BallCollisionCanStrong, this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Can"))
        {
           FMODController.PlaySoundFrom(JokernVRSound.SFX_CanCollisionCan, this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
           FMODController.PlaySoundFrom(JokernVRSound.SFX_CanCollisionGround, this.gameObject);
        }
    }
}