using UnityEngine;

public class PathRandomizerChunk : MonoBehaviour {
	public PathRandomizerChunk[] doNotSpawnIfTheseAreActive;

	public void Deactivate() {
		gameObject.SetActive(false);
		isActive = false;
	}

	//[HideInInspector]
	public bool isActive;

	public void Activate() {
		if (doNotSpawnIfTheseAreActive != null) {
			for (int i = 0; i < doNotSpawnIfTheseAreActive.Length; ++i) {
				if (doNotSpawnIfTheseAreActive[i] != null &&
					doNotSpawnIfTheseAreActive[i].isActive) {
					return;
				}
			}
		}

		gameObject.SetActive(true);
		isActive = true;
	}
}
