using UnityEngine;

public class PlayVoiceLineAfter : MonoBehaviour {
	//public string voiceLinePath;

	public float delayRangeBegin;
	public float delayRangeEnd;

	float actuallyPlayAfter;
	float beginAt;

	void Start() {
		beginAt = Time.time;
		actuallyPlayAfter = Random.Range(delayRangeBegin, delayRangeEnd);
	}

	bool hasPlayed;
	void Update() {
		if (!hasPlayed && beginAt + actuallyPlayAfter < Time.time) {
			// @FMOD
			//FMODController.PlayVoiceLine(voiceLinePath);
			hasPlayed = true;
		}
	}
}
