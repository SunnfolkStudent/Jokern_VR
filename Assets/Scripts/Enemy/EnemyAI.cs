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
    
    private Animator anim;
    public Transform playerPosition;
    private Vector3 destination;
    public LayerMask Layers;

    private bool playerInSight = false;
    private bool caughtPlayer;
    private bool playerInRange = false;
    private bool playerInRange2 = false;
    private bool playerInRange3 = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        enemyRadius = 40f;
        caughtPlayer = false;
        agent.isStopped = false;
        agent.updateRotation = true;
    }
    private void Update()
    {
        destination = playerPosition.position;
        
        playerInRange = Physics.CheckSphere(transform.position, enemyRadius, Layers);
        playerInRange2 = Physics.CheckSphere(transform.position, enemyRadius2, Layers);
        playerInRange3 = Physics.CheckSphere(transform.position, enemyRadius3, Layers);
        
        anim.SetBool("isChasing", playerInRange);
        
        if (playerInRange && !caughtPlayer)
        {
            enemyRadius = 80f;
            // RotateTowardPlayer();
            Chasing();
            FMODController.finalPathTension = FMODController.FinalPathTension.Low;
            
        }

        if (playerInRange && playerInRange2)
        {
            FMODController.finalPathTension = FMODController.FinalPathTension.Medium;
        }
        else
        {
            FMODController.finalPathTension = FMODController.FinalPathTension.None;
        }

        if (playerInRange && playerInRange2 && playerInRange3)
        {
            FMODController.finalPathTension = FMODController.FinalPathTension.High;
        }
        else
        {
            FMODController.finalPathTension = FMODController.FinalPathTension.None;
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
        transform.LookAt(playerPosition.position);
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