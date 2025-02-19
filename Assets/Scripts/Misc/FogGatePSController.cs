using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FogGatePSController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSys;
    [SerializeField] private ParticleSystem.MainModule main;
    [SerializeField] private ParticleSystemRenderer particleSysRenderer;
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private AnimationCurve alphaCurve;
    public bool spawn;
    // [SerializeField] private float alphaDuration;
    // [SerializeField] private float sizeDuration;
    [SerializeField] private float duration = 0.4f;
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    private Vector3 pivot;

    private void Update()
    {
        main = particleSys.main;
        // Make the fog more in your face
        // pivot = cameraTransform.rotation.eulerAngles;
        // // pivot.x = pivot.x / 360 * 0.3f;
        // pivot.x = (360 - pivot.x) / 360 * 0.3f;
        // pivot.y = 0;
        // pivot.z = (360 - pivot.y) / 360 * 0.3f;
        // particleSysRenderer.pivot = pivot;
        // if (spawn)
        // {
        //     spawn = false;
        //     print("Button pressed");
        //     StartCoroutine(FogGateSpawnSimultanious());
        // }
    }

    public void SpawnFogGate()
    {
        StartCoroutine(FogGateSpawnSimultanious());
    }

    private IEnumerator FogGateSpawnSimultanious()
    {
        particleSys.Play();
        // Store the original scale
        Vector3 originalScale = transform.localScale;
        
        // Track the elapsed time
        float elapsedTime = 0f;

        Color minColor = main.startColor.colorMin;
        Color maxColor = main.startColor.colorMax;
    
        // Loop until the specified duration is reached
        while (elapsedTime < duration)
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
    
            // Use the curve to get the scale factor (from 1 to 0 based on curve)
            float curveVal = alphaCurve.Evaluate(t);
            // // particleSys.main.startColor.
            // minColor.a = alpha;
            // maxColor.a = alpha;
            // main.startColor = new ParticleSystem.MinMaxGradient(minColor, maxColor);
            // float size = sizeCurve.Evaluate(t);
            
            ChangeExistingParticles(curveVal, minSize + curveVal * maxSize);
            // Apply the scale factor to the original scale
            yield return null;
        }
    }
    // private IEnumerator FogGateSpawn()
    // {
    //     // Store the original scale
    //     Vector3 originalScale = transform.localScale;
    //     
    //     // Track the elapsed time
    //     float elapsedTime = 0f;
    //
    //     Color minColor = main.startColor.colorMin;
    //     Color maxColor = main.startColor.colorMax;
    //
    //     // Loop until the specified duration is reached
    //     while (elapsedTime < alphaDuration)
    //     {
    //         // Increment the elapsed time
    //         elapsedTime += Time.deltaTime;
    //         float t = elapsedTime / alphaDuration;
    //
    //         // Use the curve to get the scale factor (from 1 to 0 based on curve)
    //         float alpha = alphaCurve.Evaluate(t);
    //         // // particleSys.main.startColor.
    //         // minColor.a = alpha;
    //         // maxColor.a = alpha;
    //         // main.startColor = new ParticleSystem.MinMaxGradient(minColor, maxColor);
    //         // float size = sizeCurve.Evaluate(t);
    //         
    //         ChangeExistingParticles(alpha, 1);
    //
    //         // Apply the scale factor to the original scale
    //         yield return null;
    //     }
    //     elapsedTime = 0f;
    //     print("Switch:");
    //     while (elapsedTime < sizeDuration)
    //     {
    //         // Increment the elapsed time
    //         elapsedTime += Time.deltaTime;
    //         float t = elapsedTime / sizeDuration;
    //
    //         // Use the curve to get the scale factor (from 1 to 0 based on curve)
    //         float alpha = alphaCurve.Evaluate(t);
    //         // main.startSize = alpha * 5;
    //         ChangeExistingParticles(1, minSize + alpha * maxSize);
    //
    //         // Apply the scale factor to the original scale
    //         yield return null;
    //     }
    // }
    void ChangeExistingParticles(float alpha, float particleSize)
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[main.maxParticles];
        int count = particleSys.GetParticles(particles);
        
        for (int i = 0; i < count; i++)
        {
            particles[i].startSize = particleSize;  // Update size
            Color newColor = particles[i].startColor;
            newColor.a = alpha;
            particles[i].startColor = newColor; // Update Alpha
        }
    
        particleSys.SetParticles(particles, count); // Apply changes
    }

    // private IEnumerator ShrinkToZero()
    // {
    //     // Store the original scale
    //     Vector3 originalScale = transform.localScale;
    //
    //     // Track the elapsed time
    //     float elapsedTime = 0f;
    //
    //     // Loop until the specified duration is reached
    //     while (elapsedTime < shrinkDuration)
    //     {
    //         // Increment the elapsed time
    //         elapsedTime += Time.deltaTime;
    //         float t = elapsedTime / shrinkDuration;
    //
    //         // Use the curve to get the scale factor (from 1 to 0 based on curve)
    //         float scaleFactor = shrinkCurve.Evaluate(t);
    //
    //         // Apply the scale factor to the original scale
    //         transform.localScale = originalScale * scaleFactor;
    //
    //         // Wait for the next frame
    //         if (transform.localScale.x < 0.1f)
    //         { 
    //             transform.localScale = Vector3.zero;
    //             break;
    //         }
    //         yield return null;
    //     }
    //     HideOrShow(false);
    //     _state = "Obtained";
    //     transform.localScale = new Vector3(1,1,1);
    //     transform.position = mainScenePos;
    // }
}
