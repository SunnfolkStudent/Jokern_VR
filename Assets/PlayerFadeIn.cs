using System;
using System.Collections;
using System.Numerics;
using UnityEngine;

public class PlayerFadeIn : MonoBehaviour
{
    public Material fogMaterial;

    private float normalVol = 1.2f;
    private float maxVol = 5f;

    public float lerpTime;
    public float timeStartedLerping;

    private bool shouldLerp = false;
    
    void Start()
    {
        StartLerping();
    }

    private void Update()
    {
        if (shouldLerp)
        {
            fogMaterial.SetFloat("_DensityMultiplier", Lerp(maxVol, normalVol, timeStartedLerping, lerpTime));
        }
    }

    private void StartLerping()
    {
        timeStartedLerping = Time.time;

        shouldLerp = true;
    }

    private float Lerp(float start, float end, float timeStartedLerping, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;
        
        float percentageComplete = timeSinceStarted / lerpTime;
        
        var result = Mathf.Lerp(start, end, percentageComplete);
        
        return result;
    }
}
