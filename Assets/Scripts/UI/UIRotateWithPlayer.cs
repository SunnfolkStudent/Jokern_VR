using UnityEngine;
using UnityEngine.UI;

public class UIRotateWithPlayer : MonoBehaviour {
    public GameObject dings;
    
    void Update() {
        if (dings == null) return;
        transform.LookAt(dings.transform);
    }
}
