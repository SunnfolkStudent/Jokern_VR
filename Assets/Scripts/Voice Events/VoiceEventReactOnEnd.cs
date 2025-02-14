using UnityEngine;
using UnityEngine.Events;

public class VoiceEventReactOnEnd : MonoBehaviour {
	public string voiceLinePath;
	public bool playOnStart;

	[Space(10)]
	public UnityEvent onVoiceLineEnd;

	void VoiceLineEnd() {
		if (onVoiceLineEnd != null) {
			onVoiceLineEnd.Invoke();
		}
	}

	void Start() {
		if (playOnStart) Play();
	}

	public void Play() {
		FMODController.onVoiceLineEnd.AddListener(VoiceLineEnd);
		SubtitleSystem.PlayVoiceLine(voiceLinePath);
	}
}
