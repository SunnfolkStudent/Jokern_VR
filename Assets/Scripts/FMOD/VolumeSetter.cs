using UnityEngine;

public class VolumeSetter : MonoBehaviour {
	public static void SetVolume(VolumeSlider slider, float volume) {
		FMODController.SetVolume(slider, volume);
	}

	public static float GetVolume(VolumeSlider slider) {
		return FMODController.GetVolume(slider);
	}
}
