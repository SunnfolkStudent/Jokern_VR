using UnityEngine;

public class MusicPlayer : MonoBehaviour {
	public JokernVRSound sound;
	void Start() {
		FMODController.PlaySoundFrom(sound, gameObject);
	}
}
