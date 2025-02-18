using UnityEngine;

public class JFVoiceEvent4 : MonoBehaviour {
	public string voiceLinePath;
	public GameObject freezer;

	int countTimesExited = 0;
	void OnTriggerExit(Collider other) {
		if (!other.gameObject.CompareTag("Player")) return;

		countTimesExited += 1;

		if (countTimesExited == 1) {
			FMODController.PlayVoiceLineAudio("event:/VO/Joker Forest/vo_jokerforest_freezer_help_01");
		} else if (countTimesExited == 2) {
			SubtitleSystem.PlayVoiceLine(voiceLinePath);
		}
	}
}
