using System;
using UnityEngine;

public class c4 : MonoBehaviour
{
    [SerializeField] private GameObject confettiPSPrefab;
    // private ParticleSystem confettiSystem;
    private float confettiTime = 3f;
    private MeshRenderer objectRenderer;
    private Vector3 centerPos;

    private void Start()
    {
        // Instantiate(confettiPSPrefab, transform.position, Quaternion.identity);
        objectRenderer = this.gameObject.GetComponent<MeshRenderer>();
    }

    public void Explode()
    {
        objectRenderer.enabled = false;
        centerPos = objectRenderer.bounds.center;
        GameObject confettiInstance =  Instantiate(confettiPSPrefab, centerPos, Quaternion.identity);
        // ParticleSystem confettiInstance = Instantiate(confettiSystem, transform.position, Quaternion.identity);
        Destroy(confettiInstance, confettiTime);
    }
}
