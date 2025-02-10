using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMODController : MonoBehaviour {
	Transform cameraTransform;
	void Start() {
		cameraTransform = Camera.main.transform;

		// We don't need an instance to play our sounds as Robin has made the
		// FMOD parameters global. RuntimeManager.PlayOneShot() is all we need
		// for now.
		//footstepSoundInstance = RuntimeManager.CreateInstance(footstepSoundEvent);
		//RuntimeManager.AttachInstanceToGameObject(footstepSoundInstance, cameraTransform);
		//footstepSoundInstance.set3DAttributes(RuntimeUtils.To3DAttributes(cameraTransform.position));
		//footstepSoundInstance.start();
	}

	public bool playThing;
	public FootstepSoundType foot;
	void Update() {
		if (playThing) {
			playThing = false;
			PlayFootstepSound(foot);
		}

		// TODO: Do we want to set this?
		// takes in a ATTRIBUTES_3D
		//footstepSoundInstance.set3DAttributes();
	}

	public EventReference footstepSoundEvent;
	public EventInstance  footstepSoundInstance;
	public void PlayFootstepSound(FootstepSoundType sound) {
		if (sound == FootstepSoundType.None) return;

		/* if (!footstepSoundEvent.oneshot) {
			Debug.LogError("Footstep sound should be oneshot! We cant play multiple loopings, or both FMOD and Unity will die!");
			return;
		} */

		// TODO: Parameters
		//footstepSoundInstance.


		RuntimeManager.PlayOneShot(footstepSoundEvent);
	}
}
