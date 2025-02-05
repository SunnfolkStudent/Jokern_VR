using UnityEngine;

public class PathRandomizerChunk : MonoBehaviour {
	[Tooltip("Increase this value to make this chunk randomly avoid spawning.")]
	[Range(0.0f, 1.0f)]
	public float chanceToBeEmpty = 0.0f;
	public PathRandomizerChunk[] doNotSpawnIfTheseAreActive;

	void Awake() {
		Deactivate();
	}

	public void Deactivate() {
		gameObject.SetActive(false);
		isActive = false;
	}

	[HideInInspector] public bool isActive;

	public void AttemptActivation() {
		if (doNotSpawnIfTheseAreActive != null) {
			for (int i = 0; i < doNotSpawnIfTheseAreActive.Length; ++i) {
				if (doNotSpawnIfTheseAreActive[i] != null &&
					doNotSpawnIfTheseAreActive[i].isActive) {
					return;
				}
			}
		}

		chanceToBeEmpty = Mathf.Clamp01(chanceToBeEmpty);
		if (Random.Range(0.0f, 1.0f) < chanceToBeEmpty) {
			return;
		}

		gameObject.SetActive(true);
		isActive = true;
	}
}
