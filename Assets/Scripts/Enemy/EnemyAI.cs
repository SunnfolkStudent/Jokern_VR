using System;
using System.Data;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public float runSpeed = 9;
    public float enemyRadius = 40f;
    public float enemyRadius2 = 20f;
    public float enemyRadius3 = 10f;
    
    public Transform playerPosition;

    private Vector3 destination;

    private bool caughtPlayer;
    private bool playerInRange = false;
    private bool playerInRange2 = false;
    private bool playerInRange3 = false;
    
    public LayerMask Layers;

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
        destination = playerPosition.position;
        
        playerInRange = Physics.CheckSphere(transform.position, enemyRadius, Layers);
        playerInRange2 = Physics.CheckSphere(transform.position, enemyRadius2, Layers);
        playerInRange3 = Physics.CheckSphere(transform.position, enemyRadius3, Layers);
        
        if (playerInRange && !caughtPlayer)
        {
            RotateTowardPlayer();
            Chasing();
            //Play chase music 1 here
        }

        if (playerInRange && playerInRange2)
        {
            //Play chase music 2 here
        }
        else
        {
            //Stop that
        }

        if (playerInRange && playerInRange2 && playerInRange3)
        {
            //Play chase music 3 here
        }
        else
        {
            //Stop doing that!!
        }
    }

    private void Chasing()
    {
        agent.SetDestination(destination);
        Move(runSpeed);

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
        Gizmos.DrawSphere(transform.position, enemyRadius);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, enemyRadius2);
        
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, enemyRadius3);
    }
}