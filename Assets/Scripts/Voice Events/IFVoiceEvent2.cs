using UnityEngine;
using UnityEngine.Events;

public class IFVoiceEvent2 : MonoBehaviour {
	public string voiceLinePath;
	public float  minSecondsToWait;
	public float  maxSecondsToWait;

	float playAt = Mathf.Infinity;
	void Update() {
		if (playAt <= Time.time) {
			SubtitleSystem.PlayVoiceLine(voiceLinePath);
		}
	}

	public void StartWaiting() {
		playAt = Time.time + Random.Range(minSecondsToWait, maxSecondsToWait);
	}
}
