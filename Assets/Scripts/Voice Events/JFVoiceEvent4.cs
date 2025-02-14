using UnityEngine;

public class JFVoiceEvent4 : MonoBehaviour {
	public string voiceLinePath;

	int countTimesExited = 0;
	void OnTriggerExit(Collider other) {
		if (!other.gameObject.CompareTag("Player")) return;

		countTimesExited += 1;

		if (countTimesExited == 1) {
			// @SFX
		} else if (countTimesExited == 2) {
			SubtitleSystem.PlayVoiceLine(voiceLinePath);
		}
	}
}
