using System;
using System.Data;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public float startWaitTime = 0;
    public float rotateTime = 2;
    public float walkSpeed = 6;
    public float runSpeed = 9;
    public float patrolRadius = 1f;
    public float patrolAngle = 0f;
    public float maxRayDistance = 100f;

    public LayerMask layersToIgnore;
    
    private Vector3 playerLastPosition;
    private Vector3 playerPosition;

    public bool playerSpotted;
    public bool isPatrol;
    private bool caughtPlayer;

    private Vector3 destination;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    private void Start()
    {
        isPatrol = true;
        caughtPlayer = false;
        
        agent = GetComponent<NavMeshAgent>();

        agent.isStopped = false;
        agent.speed = walkSpeed;
    }

    private void FixedUpdate()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    private void Update()
    {
        EnviromentView();
        
        if (!isPatrol)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);

            Chasing();
        }
        else
        {
            CircularPath();
            RotateTowardPlayer();
        }
        
    }

    private void Chasing()
    {
        if (playerSpotted)
        {
            Move(runSpeed);
            agent.SetDestination(playerPosition);
        }

        if (caughtPlayer)
        {
            Destroy(gameObject);
        }
    }

    private void Move(float speed)
    {
        agent.isStopped = false;
        agent.speed = speed;
    }

    private void Stop()
    {
        agent.isStopped = true;
        agent.speed = 0;
    }

    private void CaughtPlayer()
    {
        caughtPlayer = true;
    }

    private void RotateTowardPlayer()
    {
        transform.LookAt(playerPosition);
    }

    private void EnviromentView()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance, layersToIgnore))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                isPatrol = false;
                playerSpotted = true;
            }
        }
        
    }

    private void CircularPath()
    {
        float x = playerPosition.x + Mathf.Cos(patrolAngle) * patrolRadius;  
        float y = playerPosition.y;
        float z = playerPosition.z + Mathf.Sin(patrolAngle) * patrolRadius;
        
        transform.position = new Vector3(x, y, z);
        
        patrolAngle += walkSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            caughtPlayer = true;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * maxRayDistance, Color.green);
    }
}
