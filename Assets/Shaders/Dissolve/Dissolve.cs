using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour{
    public float dissolveSpeed = 0.01f;
    private float _alphaClipThreshold = 0;
    private bool _dissolving = false;
    
    void Start() => _alphaClipThreshold = Shader.GetGlobalFloat("_AlphaClipThreshold");
    void Update() {
        Shader.SetGlobalFloat("_AlphaClipThreshold", _alphaClipThreshold);
        if (_dissolving) _alphaClipThreshold += dissolveSpeed;
    }

    public void Dissolve() => _dissolving = true;
}
