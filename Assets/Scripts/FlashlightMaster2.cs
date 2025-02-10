using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.InputSystem;

public class FlashlightMaster2 : MonoBehaviour
{
    //      Flashlight Variables        //
    // Sets the counter, intensity of light, and max lifetime
    [SerializeField]private float lightTimer;
    [SerializeField]private float lightIntensity;
    [SerializeField]private float lightLifetime = 90;
    [SerializeField]private bool isLightOn = true;
    private Light lightSource;
    
    //      Flashlight Shake        //
    [Header("Shake Refresh Flashlight")]
    [SerializeField] private float shakeBufferTime = 3f;
    [SerializeField] private float shakeTimer = 0;
    [SerializeField] private float shakeIntensity = 0.3f;
    [SerializeField] private int shakeAmount = 6;
    [SerializeField] private int shakeCounter = 0;
    [SerializeField] private int shakeTurnSensetivity = 0;
    private Vector3 shakeVelocity;
    private Func<bool>[] shakeConditionals;
    private Vector3 previousShakeVelocity;

    //      Clunky way to read gyro values      //
    public InputActionProperty gyroVelocityInput;
    public InputActionProperty gyroAngularVelocityInput;
    [SerializeField]private Vector3 leftControllerVelocity;
    [SerializeField]private Vector3 leftControllerAngularVelocity;
    
    
    
    private void Start()
    {
        lightTimer = lightLifetime;
        lightSource = GetComponent<Light>();
        if (isLightOn == false)
        {
            isLightOn = true;
        }

        shakeConditionals = new Func<bool>[]
        {
            () => shakeVelocity.magnitude > shakeIntensity,
            () => Vector3.Dot(previousShakeVelocity, RoundVec3(shakeVelocity, shakeTurnSensetivity)) < -1
        };
    }

    private void Update()
    {
        if (isLightOn)
        {
            lightSource.enabled = true;
            lightTimer -= Time.deltaTime;
            lightIntensity = Mathf.Floor(lightTimer);
        }

        // if (lightTimer > 30) return;
        if (lightTimer < 30 && lightTimer > 20)
        {
            lightSource.intensity = lightIntensity; 
        }
        else if (lightTimer <= 1 && isLightOn)
        {
            lightSource.intensity = Random.Range(0f, 7.5f);
        }
        else if (lightTimer <= 0 && isLightOn)
        {
            isLightOn = false;
            lightSource.enabled = false;
            UpdateInput();
        }
    }

    private void UpdateInput()
    {
        if (isLightOn == false)
        {
            // Variables needed for Shake Logic
            leftControllerAngularVelocity = gyroAngularVelocityInput.action.ReadValue<Vector3>();
            shakeVelocity = leftControllerAngularVelocity;
            
            // Shake Logic
            // tl;dr checks for conditions of a shake within a buffer interval
            // if you have done enough "shakes" it Resets the flashlight and needed variables
            // 1 shake is 2 numbers on the shakeCounter due to 1 shake having 2 conditions to fulfill
            shakeTimer -= Time.deltaTime;
            if (shakeTimer < 0) { shakeCounter = 0; }
            if (shakeConditionals[shakeCounter % 2]())
            {
                shakeTimer = shakeBufferTime;
                previousShakeVelocity = RoundVec3(shakeVelocity, shakeTurnSensetivity);
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
            lightSource.enabled = true;
            lightSource.intensity = 30;
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