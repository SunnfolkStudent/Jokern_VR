using UnityEngine;
using UnityEngine.Events;

public class IFVoiceEvent1 : MonoBehaviour {
	public string voiceLinePath;

	[Space(10)]
	public UnityEvent onVoiceLineEnd;

	void VoiceLineEnd() {
		if (onVoiceLineEnd != null) {
			onVoiceLineEnd.Invoke();
		}
	}

	void Start() {
		Play();
	}

	public void Play() {
		FMODController.onVoiceLineEnd.AddListener(VoiceLineEnd);
		SubtitleSystem.PlayVoiceLine(voiceLinePath);
	}
}
