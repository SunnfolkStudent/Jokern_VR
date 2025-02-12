using UnityEngine;
using TMPro;

public class SubtitleReceiver : MonoBehaviour {
	// Since we use a receiver, we can create multiple channels if we want to.
	//public int subtitleChannel;

	TMP_Text receiverTextField;
	void Start() {
		if (!TryGetComponent(out receiverTextField)) {
			Debug.LogError($"Subtitle receiver {this.name} does not have a TMP Text component attached to it. TMP Text is currently the only supported text receiver format. Talk to August if you need something else.");
			Destroy(this);
		}
	}

	public void ReceiveText(string text) {
		receiverTextField.text = text;
	}
}
