using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    public Camera portalCameraA;
    public Camera portalCameraB;

    public Material cameraMatA;
    public Material cameraMatB;
    
    void Start()
    {
        if (portalCameraA.targetTexture != null)
        {
            portalCameraA.targetTexture.Release();
        }
        portalCameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatA.mainTexture = portalCameraA.targetTexture;
        
        if (portalCameraB.targetTexture != null)
        {
            portalCameraB.targetTexture.Release();
        }
        portalCameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatB.mainTexture = portalCameraB.targetTexture;
    }
    
}
