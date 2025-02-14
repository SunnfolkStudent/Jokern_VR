#if false
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public class FlashlightMaster : MonoBehaviour
{
    //Sets the counter, intensity of light, and max lifetime
    [SerializeField]private float lightTimer;
    [SerializeField]private float lightIntensity;
    [SerializeField]private float lightLifetime = 90;

    private InputDevice leftController;
    [SerializeField]private Vector3 leftControllerVelocity;
    
    [SerializeField]private bool isLightOn = true;
    private Light light;
    
    
    private void Start()
    {
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
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
            leftController.TryGetFeatureValue(CommonUsages.deviceVelocity, out leftControllerVelocity);
            Debug.Log(leftControllerVelocity);
            if (leftControllerVelocity.magnitude > 0.1f)
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
        }
    }
    
}
#region TODO
//TODO: Check for motion input (shaking), which refills the lifetime
//TODO: When lifetime is at 90 (100%), re-enable light and continue battery loop

#endregion
#endif
