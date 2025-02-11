using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMODController : MonoBehaviour {
	[Tooltip("Make sure there aren't duplicate entries, or you may encounter an FMOD bug!")]
	public EventReference[] playOnStartup;
	void Start() {
		if (playOnStartup != null) {
			for (int i = 0; i < playOnStartup.Length; ++i) {
				RuntimeManager.PlayOneShot(playOnStartup[i]);
			}
		}

		// We don't need an instance to play our sounds as Robin has made the
		// FMOD parameters global. RuntimeManager.PlayOneShot() is all we need
		// for now.
		//footstepSoundInstance = RuntimeManager.CreateInstance(footstepSoundEvent);
		//RuntimeManager.AttachInstanceToGameObject(footstepSoundInstance, cameraTransform);
		// We may want to set this in Update()
		//footstepSoundInstance.set3DAttributes(RuntimeUtils.To3DAttributes(cameraTransform.position));
		//footstepSoundInstance.start();
	}

	// @Temp
	public bool playThing;
	public FootstepSoundType soundType;
	bool foot;
	void Update() {
		if (playThing) {
			playThing = false;
			foot = !foot;
			PlayFootstepSound(soundType, foot);
		}
	}

	public EventReference footstepSoundEvent;
	public void PlayFootstepSound(FootstepSoundType sound, bool footstepIsOnRightFoot) {
		if (sound == FootstepSoundType.None) return;

		/*
		if (!footstepSoundEvent.isOneShot) {
			Debug.LogError("Footstep sound should be oneshot! We cant play multiple loopings, or both FMOD and Unity will die!");
			return;
		}
		*/

		RuntimeManager.StudioSystem.setParameterByName("FootstepDirection", footstepIsOnRightFoot ? 1.0f : 0.0f);
		RuntimeManager.StudioSystem.setParameterByName("Footsteps", (float)sound);
		RuntimeManager.PlayOneShot(footstepSoundEvent);
	}
}
