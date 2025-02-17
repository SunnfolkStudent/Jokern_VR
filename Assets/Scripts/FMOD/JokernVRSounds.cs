using System;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using static AugustBase.All;

[Serializable]
public struct SoundToSoundPath {
	public JokernVRSound  sound;
	public string soundPath;
}

public class JokernVRSounds : MonoBehaviour {
	[HideInInspector] public static JokernVRSounds instance;

	[Tooltip("If there are multiple sound events matching the same sound the first one in the list will always get picked.")]
	public SoundToSoundPath[] soundToSoundPath;

	void Awake() {
		if (instance != null) {
			Debug.LogError($"There should only be one '{nameof(JokernVRSounds)}'!");
			Destroy(this);
		}

		instance = this;
	}

	bool LoadTextResourceAsLines(string name, out string[] result) {
		TextAsset textAsset = Resources.Load<TextAsset>(name);
		if (textAsset == null) {
			Debug.LogError($"We expect a resource called '{name}' to exist, but there isn't one!");
			result = default;
			return false;
		}

		if (textAsset.text.Length == 0) {
			Debug.LogWarning($"No text in resource '{name}'.");
		}

		result = textAsset.text.Split('\n');

		if (result.Length > 0) {
			if (String.IsNullOrEmpty(result[result.Length - 1])) {
				SetLength(ref result, result.Length - 1);
			}
		}

		return true;
	}

	public void ReloadSoundPathsFromDisk() {
		const string allSoundPathsTextFileName = "AllSoundPaths";

		string[] soundPathsAsLines;
		if (!LoadTextResourceAsLines(allSoundPathsTextFileName, out soundPathsAsLines)) return;

		var countJokernVRSounds = Enum.GetNames(typeof(JokernVRSound)).Length;
		if (soundPathsAsLines.Length != countJokernVRSounds) {
			Debug.LogError($"The amount of lines in '{allSoundPathsTextFileName}' ({soundPathsAsLines.Length}) does not match the amount of items in the {nameof(JokernVRSound)} enum ({countJokernVRSounds}).");
			return;
		}

		soundToSoundPath = new SoundToSoundPath[soundPathsAsLines.Length];

		for (int i = 0; i < soundPathsAsLines.Length; ++i) {
			soundToSoundPath[i].sound = (JokernVRSound)i;
			soundToSoundPath[i].soundPath = soundPathsAsLines[i];
		}
	}

	void Start() {
		ReloadSoundPathsFromDisk();
	}

	public string GetSoundPath(JokernVRSound sound) {
		if (soundToSoundPath == null) return default;

		for (int i = 0; i < soundToSoundPath.Length; ++i) {
			if (soundToSoundPath[i].sound == sound) {
				return soundToSoundPath[i].soundPath;
			}
		}

#if UNITY_EDITOR
		Debug.LogError($"No sound event set for sound '{sound.ToString()}'.");
#endif

		return default;
	}
}

public enum JokernVRSound {
	None,
	SFX_ChipsBagGrab,
	SFX_ChipsBagGround,
	SFX_ClownBlowCandle,
	SFX_ClownFootstep,
	SFX_ClownHorn,
	SFX_BloodSplatter,
	SFX_JokerAirConditioner,
	SFX_JokerFreezer,
	SFX_JokerPawnMachine,
	SFX_JokerScanner,
	SFX_JokerSodaCabinet,
	SFX_JokerDarkDoor,
	SFX_JokerLitDoor,
	SFX_BallCollisionCanStrong,
	SFX_BallCollisionGroundStrong,
	SFX_BallCollisionWoodStrong,
	SFX_CanCollisionCan,
	SFX_CanCollisionGround,
	SFX_BallCollisionCanWeak,
	SFX_BallCollisionGroundWeak,
	SFX_BallCollisionWoodWeak,
	SFX_Applause,
	SFX_Confetti,
	SFX_ConfettiPop,
	SFX_PartyFlute,
	SFX_Flashlight,
	SFX_Walking,
	AMB_River,
	MX_CircusThemeOutside,
	MX_JokerDarkTheme,
	MX_JokerLitTheme,
}
