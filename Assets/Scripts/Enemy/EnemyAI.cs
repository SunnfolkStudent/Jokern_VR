using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using NUnit.Framework.Internal;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public float runSpeed = 9;
    private float enemyRadiusOriginal;
    public float enemyRadius = 40f;
    public float enemyRadius2 = 20f;
    public float enemyRadius3 = 10f;
    
    private Animator anim;
    private Transform playerPosition;
    private Vector3 destination;
    public LayerMask Layers;
    public ParticleSystem confetti;
    private float confettiTime = .5f;
    
    float maxDistance = 20f;
    public Material staticMaterial;
    
    public Transform[] spawnPoints; 

    private bool playerInSight = false;
    private bool caughtPlayer;
    private bool playerInRange;
    private bool playerInRange2;
    private bool playerInRange3;
    private bool shouldChase;
    private bool die;

    private GameObject cam;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        
        Debug.Log(cam.name);
        playerPosition = cam.transform;
        
        staticMaterial.SetFloat("_Strength", 0);
        transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        enemyRadiusOriginal = enemyRadius;
        caughtPlayer = false;
        playerInRange = false;
        playerInRange2 = false;
        playerInRange3 = false;
        agent.isStopped = false;
        agent.updateRotation = true;
        die = false;
        shouldChase = true;

        //Use this to test death part 2: electric boogaloo
        // StartCoroutine(TestDeath());
    }
    private void Update()
    {
        playerPosition = cam.transform;

        destination = playerPosition.position;
        
        float distance = Vector3.Distance(transform.position, playerPosition.position);
        float value = Mathf.InverseLerp(maxDistance, 0, distance);
        staticMaterial.SetFloat("_Strength", value);
        
        playerInRange = Physics.CheckSphere(transform.position, enemyRadius, Layers);
        playerInRange2 = Physics.CheckSphere(transform.position, enemyRadius2, Layers);
        playerInRange3 = Physics.CheckSphere(transform.position, enemyRadius3, Layers);
        
        anim.SetBool("isChasing", playerInRange);
        
        if (playerInRange && !caughtPlayer && shouldChase)
        {
            enemyRadius = 100f;
            Chasing();
            FMODController.finalPathTension = FMODController.FinalPathTension.Low;
            
        }

        if (die && !caughtPlayer)
        {
            Death();
        }

        // if (playerInRange && playerInRange2)
        // {
        //     FMODController.finalPathTension = FMODController.FinalPathTension.Medium;
        // }
        // else
        // {
        //     FMODController.finalPathTension = FMODController.FinalPathTension.None;
        // }
        //
        // if (playerInRange && playerInRange2 && playerInRange3)
        // {
        //     FMODController.finalPathTension = FMODController.FinalPathTension.High;
        // }
        // else
        // {
        //     FMODController.finalPathTension = FMODController.FinalPathTension.None;
        // }
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

    public void Death()
    {
        ConfettiExplosion();
        // FMODController.PlaySoundFrom(JokernVRSound.SFX_ConfettiPop, gameObject);
        confetti.Play();
        
        ResetEnemy(enemyRadiusOriginal);
    }

    private void ResetEnemy(float radiusDefaultValue)
    {
        enemyRadius = radiusDefaultValue;
        Stop();
        staticMaterial.SetFloat("_Strength", 0);
        die = false;
        shouldChase = false;
        playerInRange = false;
        caughtPlayer = false;
        transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
    }

    private void ConfettiExplosion()
    {
        ParticleSystem confettiInstance = Instantiate(confetti, transform.position, Quaternion.identity);
        Destroy(confettiInstance, confettiTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            caughtPlayer = true;
        }

        if (other.gameObject.CompareTag("Light"))
        {
            Invoke("Death", 1.5f);
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