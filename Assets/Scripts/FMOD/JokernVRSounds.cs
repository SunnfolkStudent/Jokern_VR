using System;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class JokernVRSounds : MonoBehaviour {
	[HideInInspector] public static JokernVRSounds instance;

	[Tooltip("If there are multiple sound events matching the same sound the first one in the list will always get picked.")]
	public SoundToSoundEvent[] soundToSoundEvent; // = new SoundToSoundEvent[Enum.GetNames(typeof(JokernVRSound)).Length];

	void Awake() {
		if (instance != null) {
			Debug.LogError($"There should only be one '{nameof(JokernVRSounds)}'!");
			Destroy(this);
		}

		instance = this;
	}

	public EventReference GetSoundEvent(JokernVRSound sound) {
		if (soundToSoundEvent == null) return default;

		for (int i = 0; i < soundToSoundEvent.Length; ++i) {
			if (soundToSoundEvent[i].sound == sound) {
				return soundToSoundEvent[i].eventReference;
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
	MX_CircusTheme,
	MX_FinalPathTheme,
	MX_JokerLitTheme,
	MX_MainTheme,
	SFX_BloodSplatter,
	SFX_ClownBlowCandle,
	SFX_ClownFootstep,
	SFX_ClownHorn,
	SFX_Confetti,
	SFX_ConfettiPop,
	SFX_FlashlightFlicker,
	SFX_FlashlightHum,
	SFX_FlashlightRattle,
	SFX_PartyFlute,
	AMB_CircusForest,
	AMB_CircusOutside,
	AMB_FinalPath,
	AMB_ForestNight,
	AMB_JokerForest,
	AMB_JokerdarkInside,
	AMB_JokerlitInside,
	AMB_ParkingLot,
	AMB_River,
	SFX_BallCollisionCanStrong,
	SFX_BallCollisionWoodStrong,
	SFX_BellStrong,
	SFX_BellWeak,
	SFX_CanCollisionCan,
	SFX_CanCollisionGround,
	SFX_HammerStrong,
	SFX_JokerAirConditioner,
	SFX_JokerScanner,
	SFX_JokerFreezer,
	SFX_JokerPawnMachine,
	SFX_JokerBoneGrinder,
	SFX_JokerSodaCabinet,
	SFX_JokerDarkDoor,
	SFX_JokerLitDoor,
	SFX_Magpie,
	SFX_Walking,
	SFX_BallCollisionGroundStrong,
	AMB_CircusInside,
	AMB_JokerdarkOutside,
	AMB_JokerlitOutside,
	SFX_BallCollisionCanWeak,
	SFX_BallCollisionGroundWeak,
	SFX_BallCollisionWoodWeak,
	SFX_ChipsBagGrab,
	SFX_ChipsBagGround,
	SFX_HammerMedium,
	SFX_HammerWeak,
	MX_JokerDarkTheme,
}

[Serializable]
public class SoundToSoundEvent {
	public JokernVRSound  sound;
	public EventReference eventReference;
}
