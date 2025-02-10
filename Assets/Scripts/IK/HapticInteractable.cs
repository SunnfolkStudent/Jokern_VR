using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticInteractable : MonoBehaviour
{
    [Range(0, 1)]
    public float intesity;
    public float duration;

    private void Start()
    {
       XRBaseInteractor interactable = GetComponent<XRBaseInteractor>(); 
       interactable. addlistener()
    }
}
