using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.XR.XRSettings;

public class PortalTextureSetup : MonoBehaviour
{
    public Camera portalCameraA;
    public Camera portalCameraB;

    public Material cameraMatA;
    public Material cameraMatB;

    private int width = 2080;
    private int height = 2096;
    private int depth = 24;
    
    void Start()
    {
        StartCoroutine(CheckWidthHeight());
        
        if (portalCameraA.targetTexture != null)
        {
            portalCameraA.targetTexture.Release();
        }
        portalCameraA.targetTexture = new RenderTexture(width, 
            height, depth);
        cameraMatA.mainTexture = portalCameraA.targetTexture;
        
        if (portalCameraB.targetTexture != null)
        {
            portalCameraB.targetTexture.Release();
        }
        portalCameraB.targetTexture = new RenderTexture(width,
            height, depth);
        cameraMatB.mainTexture = portalCameraB.targetTexture;
    }

    IEnumerator CheckWidthHeight()
    {
        while (XRSettings.eyeTextureWidth == 0)
        {
            yield return new WaitForEndOfFrame();
            
            Debug.Log($"W: {XRSettings.eyeTextureWidth}, H: {XRSettings.eyeTextureHeight}");
        }
    }
    
}
