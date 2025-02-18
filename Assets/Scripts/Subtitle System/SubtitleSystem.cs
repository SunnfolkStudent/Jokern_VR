using System;
using UnityEngine;
using static AugustBase.All;

[Serializable]
public struct VoiceLine {
	public string text;
	public string soundPath;
}

public class SubtitleSystem : MonoBehaviour {
	public static VoiceLine[] voiceLines = new VoiceLine[0];
	public static int subtitleIndex;

	SubtitleReceiver[] subtitleReceivers;
	void FindSubtitleReceivers() {
		subtitleReceivers = UnityEngine.Object.FindObjectsByType<SubtitleReceiver>(FindObjectsInactive.Include, FindObjectsSortMode.None);
	}

	public void ReloadSubtitlesFromDisk() {
		const string allSubtitlesTextFileName  = "AllSubtitles";
		const string allVoiceLinesTextFileName = "AllVoiceLines";

		string[] subtitlesAsLines, voiceLinesAsLines;
		if (!LoadTextResourceAsLines(allSubtitlesTextFileName,  out subtitlesAsLines))  return;
		if (!LoadTextResourceAsLines(allVoiceLinesTextFileName, out voiceLinesAsLines)) return;

		if (subtitlesAsLines.Length != voiceLinesAsLines.Length) {
			Debug.LogError($"The amount of lines in '{allSubtitlesTextFileName}' ({subtitlesAsLines.Length}) does not match the amount of lines in '{allVoiceLinesTextFileName}' ({voiceLinesAsLines.Length}). Remember that the lines in these file correspond to eachother.");
			return;
		}

		voiceLines = new VoiceLine[subtitlesAsLines.Length];

		for (int i = 0; i < subtitlesAsLines.Length; ++i) {
			voiceLines[i].text = subtitlesAsLines[i];
		}

		for (int i = 0; i < voiceLinesAsLines.Length; ++i) {
			voiceLines[i].soundPath = voiceLinesAsLines[i];
		}
	}

	void Start() {
		FindSubtitleReceivers();
		ReloadSubtitlesFromDisk();

		// Set every receiver to empty.
		if (subtitleReceivers != null) {
			for (int i = 0; i < subtitleReceivers.Length; ++i) {
				if (subtitleReceivers[i] != null) {
					subtitleReceivers[i].ReceiveText("");
				}
			}
		}
	}

	void TransmitSubtitles() {
		string text;
		if (0 <= subtitleIndex && subtitleIndex < voiceLines.Length) {
			text = voiceLines[subtitleIndex].text;
		} else {
#if UNITY_EDITOR
			text = $"(No subtitle has index {subtitleIndex})";
#else
			text = "";
#endif
		}

		if (subtitleReceivers != null) {
			for (int i = 0; i < subtitleReceivers.Length; ++i) {
				if (subtitleReceivers[i] != null) {
					subtitleReceivers[i].ReceiveText(text);
				}
			}

#if UNITY_EDITOR
			if (subtitleReceivers.Length == 0) print($"NEW SUBTITLE: {text}");
#endif
		}
	}

	int previousSubtitleIndex;
	void Update() {
		if (previousSubtitleIndex != subtitleIndex) {
			TransmitSubtitles();
			previousSubtitleIndex = subtitleIndex;
		}
	}

	public static void PlayVoiceLine(string voiceLineFileName) {
		SetVoiceLinePath(voiceLineFileName);

		if (0 <= subtitleIndex && subtitleIndex < voiceLines.Length) {
			FMODController.PlayVoiceLineAudio(voiceLines[subtitleIndex].soundPath);
		}
	}
	static void SetVoiceLinePath(string voiceLineFileName) {
		// Find the subtitle belonging to the voice line!
		subtitleIndex = -1;
		for (int i = 0; i < voiceLines.Length; ++i) {
			// TODO: This does not match the actual file name, just the end of the string.
			if (voiceLines[i].soundPath.EndsWith(voiceLineFileName)) {
				subtitleIndex = i;
				break;
			}
		}
	}

	public static void PlayVoiceLineFrom(string voiceLineFileName, GameObject obj) {
		SetVoiceLinePath(voiceLineFileName);

		if (0 <= subtitleIndex && subtitleIndex < voiceLines.Length) {
			FMODController.PlayVoiceLineAudioFrom(voiceLines[subtitleIndex].soundPath, obj);
		}
	}

	public void NextDialogue() {
		if (subtitleIndex + 1 < voiceLines.Length) subtitleIndex += 1;
	}
}
