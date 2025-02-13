using UnityEngine;

public class HighStrikerMainComponent : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _hitForce;
    public GameObject strikePillow;
    public HIghStrikerForceFinder _forceFinder;
    public bool grounded = true;
    public Transform groundCheck;
    public Vector3 groundBoxSize = new Vector3(1f,0.6f,1f);
    public LayerMask whatIsGround;
    public bool bellIsHit = false;
    public GameObject obstacle;

    private void Start() //at this point i hope this makes sense GetComponent gets the component
    {
        _rigidbody = GetComponent<Rigidbody>();
        _forceFinder = strikePillow.GetComponent<HIghStrikerForceFinder>();
        
    }

    private void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.position, 0.2f, whatIsGround); //checks if the weight thingy is on the ground

        if (bellIsHit == true)
        {
            obstacle.SetActive(false);
        }
        if (grounded == false) return; //stops the rest of update from running if weight is not grounded
        
        
        
        if (grounded == true && _forceFinder.forceVector != Vector3.zero) //checks if the weight is grounded and that the force vector is above zero
        {
            _rigidbody.AddForce(_forceFinder.forceVector); //adds the vector force to the weight object
        }
        _rigidbody.linearVelocity = _forceFinder.forceVector; //runs a constant check to see if force vector has changed
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("bell")) //heres where you sound boys will do sound or maybe you want to make a script for the bell on its own up to you
        {
            Debug.Log("letsa gooo you won bing bing bing");
            bellIsHit = true;
        }
    }
}
