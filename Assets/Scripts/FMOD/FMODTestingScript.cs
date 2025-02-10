using System;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMODTestingScript : MonoBehaviour
{
	[field: Header ("HideBang")]
	[field: SerializeField] public EventReference ambiance { get; private set; }
	
	private void Start()
	{
		RuntimeManager.PlayOneShot(ambiance);
	}
}