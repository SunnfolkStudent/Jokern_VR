using UnityEngine;
using UnityEngine.Events;

public class VoiceLinePlayer : MonoBehaviour {
	public void PlayVoiceLine(string voiceLinePath) {
		SubtitleSystem.PlayVoiceLine(voiceLinePath);
	}

	public void PlayVoiceLineFrom(string voiceLinePath, GameObject obj) {
		SubtitleSystem.PlayVoiceLineFrom(voiceLinePath, obj);
	}
}
