using System;
using UnityEngine;

public class c4 : MonoBehaviour
{
    [SerializeField] private GameObject confettiPSPrefab;
    // private ParticleSystem confettiSystem;
    private float confettiTime = .5f;
    private MeshRenderer objectRenderer;

    private void Start()
    {
        // Instantiate(confettiPSPrefab, transform.position, Quaternion.identity);
        objectRenderer = this.gameObject.GetComponent<MeshRenderer>();
    }

    public void Explode()
    {
        objectRenderer.enabled = false;
        GameObject confettiInstance =  Instantiate(confettiPSPrefab, transform.position, Quaternion.identity);
        // ParticleSystem confettiInstance = Instantiate(confettiSystem, transform.position, Quaternion.identity);
        Destroy(confettiInstance, confettiTime);
    }
}
