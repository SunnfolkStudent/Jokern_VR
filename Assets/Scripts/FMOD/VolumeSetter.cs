using UnityEngine;

public class VolumeSetter : MonoBehaviour {
	public static void SetVolume(FMODController.VolumeSlider slider, float volume) {
		FMODController.SetVolume(slider, volume);
	}

	public static float GetVolume(FMODController.VolumeSlider slider) {
		return FMODController.GetVolume(slider);
	}
}
