using System;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMODController : MonoBehaviour {
	[Tooltip("Make sure there aren't duplicate entries, or you may encounter an FMOD bug!")]
	public EventReference[] playOnStartup;

	static bool alreadyExists;
	void Awake() {
		if (alreadyExists) {
			Debug.LogError($"There should only be one '{nameof(FMODController)}'!");
			Destroy(this);
		}

		alreadyExists = true;
	}
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

	public static void PlaySound(JokernVRSound sound) {
		if (sound == JokernVRSound.None) return;

		var soundEvent = JokernVRSounds.instance.GetSoundEvent(sound);
		RuntimeManager.PlayOneShot(soundEvent);
	}

	public static void PlaySoundFrom(JokernVRSound sound, GameObject obj) {
		if (sound == JokernVRSound.None) return;

		var soundEvent = JokernVRSounds.instance.GetSoundEvent(sound);
		RuntimeManager.PlayOneShotAttached(soundEvent, obj);
	}

	public static void PlayFootstepSound(FootstepSound sound, bool footstepIsOnRightFoot) {
		if (sound == FootstepSound.None) return;

		RuntimeManager.StudioSystem.setParameterByName("FootstepDirection", footstepIsOnRightFoot ? 1.0f : 0.0f);
		RuntimeManager.StudioSystem.setParameterByName("Footsteps", (float)sound);

		var footstepSoundEvent = JokernVRSounds.instance.GetSoundEvent(JokernVRSound.SFX_Walking);
		RuntimeManager.PlayOneShot(footstepSoundEvent);
	}
}
