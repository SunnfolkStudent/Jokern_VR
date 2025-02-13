using System;
using UnityEngine;
using UnityEngine.Events;
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

	const string parameterName_isPlayingVoiceLine = "IsPlayingVoiceLine";
	static bool weThinkFMODIsPlayingAVoiceLine;

	public static UnityEvent onVoiceLineEnd = new();

	void FixedUpdate() {
		if (weThinkFMODIsPlayingAVoiceLine) {
			float isPlayingVoiceLineAsFloat;
			var fmodStatus = RuntimeManager.StudioSystem.getParameterByName(parameterName_isPlayingVoiceLine,
			                                                                out isPlayingVoiceLineAsFloat);
			if (fmodStatus == FMOD.RESULT.OK) {
				bool isPlayingVoiceLine = isPlayingVoiceLineAsFloat != 0.0f;
				if (!isPlayingVoiceLine) {
					weThinkFMODIsPlayingAVoiceLine = false;

					if (onVoiceLineEnd != null) {
						onVoiceLineEnd.Invoke();
					}

					onVoiceLineEnd.RemoveAllListeners();
				}
			} else {
				Debug.LogError($"FMOD is not ok! ({fmodStatus.ToString()})");
			}
		}
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

	public static void PlayVoiceLineAudio(string path) {
		RuntimeManager.StudioSystem.setParameterByName(parameterName_isPlayingVoiceLine, 1.0f);
		RuntimeManager.PlayOneShot(path);
		weThinkFMODIsPlayingAVoiceLine = true;
	}
}
