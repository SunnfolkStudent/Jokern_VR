using System;
using UnityEngine;

public class SubtitleSystem : MonoBehaviour {
	public string[] subtitles;
	public int subtitleIndex;

	SubtitleReceiver[] subtitleReceivers;
	void FindSubtitleReceivers() {
		subtitleReceivers = UnityEngine.Object.FindObjectsByType<SubtitleReceiver>(FindObjectsInactive.Include, FindObjectsSortMode.None);
	}

	public void ReloadSubtitlesFromDisk() {
		const string allSubtitlesTextFileName = "AllSubtitles";

		TextAsset subtitlesTextAsset = Resources.Load<TextAsset>(allSubtitlesTextFileName);
		if (subtitlesTextAsset == null) {
			Debug.LogWarning($"We expect a resource called '{allSubtitlesTextFileName}' to exist, but there isn't one!");
			return;
		}

		if (subtitlesTextAsset.text.Length == 0) {
			Debug.LogWarning($"No text in resource '{allSubtitlesTextFileName}'.");
			return;
		}

		string[] splitSubtitles = subtitlesTextAsset.text.Split(new [] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
		if (splitSubtitles == null || splitSubtitles.Length == 0) {
			Debug.LogError($"Failed to split text from resource '{allSubtitlesTextFileName}'.");
			return;
		}

		subtitles = splitSubtitles;
	}

	void Start() {
		FindSubtitleReceivers();
		ReloadSubtitlesFromDisk();
	}

	void TransmitSubtitle(string text) {
		if (subtitleReceivers != null) {
			for (int i = 0; i < subtitleReceivers.Length; ++i) {
				if (subtitleReceivers[i] != null) {
					subtitleReceivers[i].ReceiveText(text);
				}
			}
		}
	}

	void Update() {
		if (0 <= subtitleIndex && subtitleIndex < subtitles.Length) {
			TransmitSubtitle(subtitles[subtitleIndex]);
		} else {
			TransmitSubtitle("");
		}
	}

	public void NextDialogue() {
		if (subtitleIndex + 1 < subtitles.Length) subtitleIndex += 1;
	}
}
