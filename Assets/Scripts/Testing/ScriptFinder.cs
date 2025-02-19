using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ScriptFinder : MonoBehaviour {
    public int countOfType;
    public string[] found;
    public GameObject[] objects;
    
    void Update() {
        var dings = GameObject.FindObjectsByType<TrackedPoseDriver>(FindObjectsSortMode.None);
        countOfType = dings.Length;
        
        found = new string[dings.Length];
        objects = new GameObject[dings.Length];
        
        for (int i = 0; i < countOfType; i++) {
            found[i] = dings[i].gameObject.name;
            objects[i] = dings[i].gameObject;
        }
    }
}
