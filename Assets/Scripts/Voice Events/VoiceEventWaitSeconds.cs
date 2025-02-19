using UnityEngine;
using UnityEngine.Events;

public class VoiceEventWaitSeconds : MonoBehaviour {
	public string voiceLinePath;
	public float  minSecondsToWait;
	public float  maxSecondsToWait;

	float playAt = Mathf.Infinity;
	bool played;
	void Update() {
		if (!played && playAt <= Time.time) {
			SubtitleSystem.PlayVoiceLine(voiceLinePath);
			played = true;
		}
	}

	public void StartWaiting() {
		playAt = Time.time + Random.Range(minSecondsToWait, maxSecondsToWait);
	}
}
