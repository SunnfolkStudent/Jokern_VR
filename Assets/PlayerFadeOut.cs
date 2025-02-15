using UnityEngine;

public class PlayerFadeOut : MonoBehaviour
{
    public Material fogMaterial;

    private float normalVol = 1.2f;
    private float maxVol = 5f;

    public float lerpTime;
    public float timeStartedLerping;

    private bool shouldLerp = false;

    private void Update()
    {
        if (shouldLerp)
        {
            fogMaterial.SetFloat("_DensityMultiplier", Lerp(maxVol, normalVol, timeStartedLerping, lerpTime));
        }
    }

    public void StartLerping()
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
