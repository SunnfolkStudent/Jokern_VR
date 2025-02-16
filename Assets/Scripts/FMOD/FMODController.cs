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
		//
		// Instance example:
		//footstepSoundInstance = RuntimeManager.CreateInstance(footstepSoundEvent);
		//RuntimeManager.AttachInstanceToGameObject(footstepSoundInstance, cameraTransform);
		//footstepSoundInstance.start();
		//
		// I don't think we need this:
		//footstepSoundInstance.set3DAttributes(RuntimeUtils.To3DAttributes(cameraTransform.position));
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

	void Update() {
		RuntimeManager.StudioSystem.setParameterByName("Walking", playerIsCurrentlyWalking ? 1.0f : 0.0f);
		RuntimeManager.StudioSystem.setParameterByName("Stage", (float)currentAmbianceStage);
		RuntimeManager.StudioSystem.setParameterByName("Level", (float)currentLevel);
		RuntimeManager.StudioSystem.setParameterByName("Calm Point", (float)currentAmbianceCalming);
		RuntimeManager.StudioSystem.setParameterByName("FlashlightPower", flashLightIsOn ? 1.0f : 0.0f);
		RuntimeManager.StudioSystem.setParameterByName("Final Path Tension", (float)finalPathTension);
		RuntimeManager.StudioSystem.setParameterByName("Joker Forest amb", jokerForestCircusOutside ? 1.0f : 0.0f);
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

	public static bool playerIsCurrentlyWalking;
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

	static string GetVolumeSliderParameterName(VolumeSlider slider) {
		switch (slider) {
			case VolumeSlider.Master:       return "Master Volume";
			case VolumeSlider.Music:        return "MX Volume";
			case VolumeSlider.SoundEffects: return "SFX Volume";
			case VolumeSlider.Ambience:     return "AMB Volume";
			case VolumeSlider.VoiceLines:   return "VO Volume";

			case VolumeSlider.None: {
				Debug.LogError("Trying to get the parameter name of a none-slider!");
				return "";
			};
		}

		Debug.LogError("Trying to get the parameter name that does not exist!");
		return "";
	}

	public static float GetVolume(VolumeSlider slider) {
		float volume = 0.0f;
		var parameterName = GetVolumeSliderParameterName(slider);
		if (parameterName == "") return volume;

		var fmodStatus = RuntimeManager.StudioSystem.getParameterByName(parameterName, out volume);

		if (fmodStatus != FMOD.RESULT.OK) {
			Debug.LogError($"FMOD is not ok! ({fmodStatus.ToString()}, trying to get parameter '{parameterName}')");
		}

		return volume;
	}

	public static void SetVolume(VolumeSlider slider, float volume) {
		var parameterName = GetVolumeSliderParameterName(slider);
		if (parameterName == "") return;

		var fmodStatus = RuntimeManager.StudioSystem.setParameterByName(parameterName, volume);

		if (fmodStatus != FMOD.RESULT.OK) {
			Debug.LogError($"FMOD is not ok! ({fmodStatus.ToString()}, trying to get parameter '{parameterName}')");
		}
	}

	public enum VolumeSlider {
		None,
		Master,
		Music,
		SoundEffects,
		Ambience,
		VoiceLines
	}

	public static AmbianceStage currentAmbianceStage;
	public enum AmbianceStage {
		MainMenu, // Title Screen
		Levels,   // Main Stage
		Credits   // Credits Forest
	}

	public static Level currentLevel;
	public enum Level {
		Intro,
		JokerForest,
		DarkJoker,
		CircusForest,
		Circus,
		FinalPath,
		LitJoker,
	}

	public static AmbianceCalming currentAmbianceCalming;
	public enum AmbianceCalming {
		Normal,
		Calm,
		NoAmbiance,
	}

	public static bool flashLightIsOn;

	// Set this if the player is outside the circus in the joker forest.
	public static bool jokerForestCircusOutside;

	// Set this based on the clowns distance to the player.
	public static FinalPathTension finalPathTension;
	public enum FinalPathTension {
		None, // Just ambiance
		Low,
		Medium,
		High,
	}
}

/*
 * Idea for how to make audio pausing a reality:
 * - When playing audio, make sure to create a new EventInstance every time.
 * - Store this instance in an array or something.
 * - When it's time to pause, just loop through this array and call .setPaused(pauseAllAudio);
 */
