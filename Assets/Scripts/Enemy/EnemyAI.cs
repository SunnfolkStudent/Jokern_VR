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
    public float runSpeed = 9;
    public float idleRadius = 1f;
    public float maxRayDistance = 100f;
    
    private Vector3 playerPosition;

    public LayerMask PlayerMask;

    public bool playerSpotted;
    private bool caughtPlayer;
    private bool playerInRange;

    private Vector3 destination;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        caughtPlayer = false;
        agent.isStopped = false;
    }
    private void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        playerInRange = Physics.CheckSphere(transform.position, idleRadius, PlayerMask);
        
        if (!caughtPlayer)
        {
            RotateTowardPlayer();
            Chasing();
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
            Debug.Log("Caught Player");
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

    private void RotateTowardPlayer()
    {
        transform.LookAt(playerPosition);
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
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, idleRadius);
    }
}