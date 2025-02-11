using System;
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

		var found = UnityEngine.Object.FindObjectsByType<JokernVRSounds>(FindObjectsSortMode.None);
		if (found.Length == 0) {
			Debug.LogError($"No '{nameof(JokernVRSounds)}' found, but we expect there to be one!");
		} else {
			if (found.Length > 1) {
				Debug.LogError($"Too many '{nameof(JokernVRSounds)}'!");
			}

			jokernVRSounds = found[0];
		}
	}

	JokernVRSounds jokernVRSounds;

#if UNITY_EDITOR
	// @Temp
	[Space(10)]
	public bool tempPlayFootstepSound;
	public FootstepSound tempSoundType;
	bool foot;

	public bool tempPlaySoundFromPosition;
	public JokernVRSound tempFromPosition_Sound;
	public Transform     tempFromPosition_Position;
#endif

	void Update() {
#if UNITY_EDITOR
		if (tempPlayFootstepSound) {
			tempPlayFootstepSound = false;
			foot = !foot;
			PlayFootstepSound(tempSoundType, foot);
		}

		if (tempPlaySoundFromPosition) {
			tempPlaySoundFromPosition = false;
			PlaySoundFrom(tempFromPosition_Sound, tempFromPosition_Position.gameObject);
		}
#endif
	}

	public void PlaySound(JokernVRSound sound) {
		if (sound == JokernVRSound.None) return;

		var soundEvent = jokernVRSounds.GetSoundEvent(sound);
		RuntimeManager.PlayOneShot(soundEvent);
	}

	public void PlaySoundFrom(JokernVRSound sound, GameObject obj) {
		if (sound == JokernVRSound.None) return;

		var soundEvent = jokernVRSounds.GetSoundEvent(sound);
		RuntimeManager.PlayOneShotAttached(soundEvent, obj);
	}

	public void PlayFootstepSound(FootstepSound sound, bool footstepIsOnRightFoot) {
		if (sound == FootstepSound.None) return;

		RuntimeManager.StudioSystem.setParameterByName("FootstepDirection", footstepIsOnRightFoot ? 1.0f : 0.0f);
		RuntimeManager.StudioSystem.setParameterByName("Footsteps", (float)sound);

		var footstepSoundEvent = jokernVRSounds.GetSoundEvent(JokernVRSound.SFX_Walking);
		RuntimeManager.PlayOneShot(footstepSoundEvent);
	}
}
