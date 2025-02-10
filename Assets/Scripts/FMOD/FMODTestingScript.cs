using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMODTestingScript : MonoBehaviour {
	public bool playThing;
	void Update() {
		if (playThing) {
			playThing = false;

			RuntimeManager.PlayOneShot("event:/SFX/sfx_walking");
		}
	}
}
