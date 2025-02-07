using UnityEngine;

public class FlashlightMaster : MonoBehaviour
{
    [SerializeField]private float lightTimer;
    [SerializeField]private float lightIntensity;
    [SerializeField]private float lightLifetime = 90;
    
    [SerializeField]private bool isLightOn = true;
    private Light light;

    private void Start()
    {
        lightTimer = lightLifetime;
        light = GetComponent<Light>();
        if (isLightOn == false)
        {
            isLightOn = true;
        }
    }

    private void Update()
    {
        if (isLightOn)
        {
            light.enabled = true;
            lightTimer -= Time.deltaTime;
            lightIntensity = Mathf.Floor(lightTimer);
        }

        if (lightTimer > 50) return;
        if (lightTimer > 1)
        {
            light.intensity = lightIntensity; 
        }
        else if (lightTimer <= 1 && isLightOn)
        {
            light.intensity = Random.Range(0f, 1f);
        }

        if (lightTimer <= 0 && isLightOn)
        {
            isLightOn = false;
            light.enabled = false;
        }
        // if Shake then
        // { lightTimer = lightLifetime; }

        
    }
}
#region TODO
//TODO: Check for motion input (shaking), which refills the lifetime
//TODO: When lifetime is at 90 (100%), re-enable light and continue battery loop

#endregion