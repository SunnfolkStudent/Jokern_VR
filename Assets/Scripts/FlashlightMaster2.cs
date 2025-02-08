using System;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;
using UnityEngine.InputSystem;

public class FlashlightMaster2 : MonoBehaviour
{
    //Sets the counter, intensity of light, and max lifetime
    [SerializeField]private float lightTimer;
    [SerializeField]private float lightIntensity;
    [SerializeField]private float lightLifetime = 90;
    
    //      Flashlight Shake        //
    [Header("Shake Refresh Flashlight")]
    [SerializeField] private float shakeBufferTime = 3f;
    [SerializeField] private float shakeTimer = 0;
    [SerializeField] private float shakeIntensity = 0.3f;
    [SerializeField] private int shakeAmount = 6;
    [SerializeField] private int shakeCounter = 0;
    [SerializeField] private int shakeTurnThreshold = 0;
    private Vector3 shakeVelocity;
    private Func<bool>[] shakeConditionals;
    private Vector3 previousShakeVelocity;

    [SerializeField] private float DotProduct;

    public InputActionProperty gyroVelocityInput;
    public InputActionProperty gyroAngularVelocityInput;
    [SerializeField]private Vector3 leftControllerVelocity;
    [SerializeField]private Vector3 leftControllerAngularVelocity;
    
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

        shakeConditionals = new Func<bool>[]
        {
            () => shakeVelocity.magnitude > shakeIntensity,
            () => Vector3.Dot(previousShakeVelocity, RoundVec3(shakeVelocity, shakeTurnThreshold)) < -1
            // () => Mathf.Approximately(leftControllerVelocity.magnitude, 0),
            // () => leftControllerVelocity.magnitude < 0.1f,
        };
    }

    private void Update()
    {
        if (isLightOn)
        {
            light.enabled = true;
            lightTimer -= Time.deltaTime;
            lightIntensity = Mathf.Floor(lightTimer);
        }

        if (lightTimer > 30) return;
        if (lightTimer > 20)
        {
            light.intensity = lightIntensity; 
        }
        else if (lightTimer <= 1 && isLightOn)
        {
            light.intensity = Random.Range(0f, 7.5f);
        }

        if (lightTimer <= 0 && isLightOn)
        {
            isLightOn = false;
            light.enabled = false;
        }
        UpdateInput();
    }

    private void UpdateInput()
    {
        if (isLightOn == false)
        {
            leftControllerVelocity = gyroVelocityInput.action.ReadValue<Vector3>();
            leftControllerAngularVelocity = gyroAngularVelocityInput.action.ReadValue<Vector3>();
            // Debug.Log(leftControllerVelocity);
            // if (shakeCounter < shakeAmount)
            // {
            //     if (shakeTimer > 0 && shake)
            //     {
            //         
            //     }
            //     else
            //     {
            //         shakeCounter = 0;
            //         shakeTimer = shakeBufferTime;
            //     }
            // }
            
            // () => Vector3.Dot(previousShakeVelocity, RoundVec3(leftControllerVelocity, shakeTurnThreshold)) < -1
            print("DotProduct: " + Vector3.Dot(previousShakeVelocity, RoundVec3(leftControllerVelocity, shakeTurnThreshold)));
            DotProduct = Vector3.Dot(previousShakeVelocity, RoundVec3(shakeVelocity, shakeTurnThreshold));
            shakeVelocity = leftControllerAngularVelocity;
            shakeTimer -= Time.deltaTime;
            if (shakeTimer < 0) { shakeCounter = 0; }
            if (shakeConditionals[shakeCounter % 2]())
            {
                shakeTimer = shakeBufferTime;
                previousShakeVelocity = RoundVec3(shakeVelocity, shakeTurnThreshold);
                shakeCounter += 1;
            }
            if (shakeCounter > shakeAmount * 2) { RefreshFlashLight(); shakeCounter = 0; }
        }
    }

    private void RefreshFlashLight()
    {
        lightTimer += 10;
        if (lightTimer > lightLifetime)
        {
            isLightOn = true;
            light.enabled = true;
            light.intensity = 30;
            lightTimer = lightLifetime;
        }
    }
    public static Vector3 RoundVec3(Vector3 values, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        Vector3 results;
        results.x = Mathf.Round(values.x * mult) / mult;
        results.y = Mathf.Round(values.y * mult) / mult;
        results.z = Mathf.Round(values.z * mult) / mult;
        return results;
        // return Mathf.Round(value * mult) / mult;
    }
    
}
#region TODO
//TODO: Check for motion input (shaking), which refills the lifetime
//TODO: When lifetime is at 90 (100%), re-enable light and continue battery loop

#endregion