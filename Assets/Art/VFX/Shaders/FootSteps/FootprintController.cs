using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FootprintController : MonoBehaviour
{
    public float footPrintLivingTime = 5f;
    private DecalProjector _footprintProjector;
   void Start() => _footprintProjector = GetComponent<DecalProjector>();
    void Update()
    {
        _footprintProjector.fadeFactor -= 0.1f/footPrintLivingTime;
        if (_footprintProjector.fadeFactor <= 0f)  Destroy(gameObject); 
    }
}
