using UnityEngine;
using UnityEngine.Events;

public class VoiceLinePlayer : MonoBehaviour {
	public void PlayVoiceLine(string voiceLinePath) {
		SubtitleSystem.PlayVoiceLine(voiceLinePath);
	}
}
