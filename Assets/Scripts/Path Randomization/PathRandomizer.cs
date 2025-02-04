using UnityEngine;

public class PathRandomizer : MonoBehaviour {
	public bool regenerateRandomChunks;
	void Update() {
		if (regenerateRandomChunks) {
			RegenerateRandomChunks();
			regenerateRandomChunks = false;
		}
	}

	public void RegenerateRandomChunks() {
		// First we reset everything to zero.
		for (int i = 0; i < transform.childCount; ++i) {
			Transform spotTransform = transform.GetChild(i);

			PathRandomizerSpot spot;
			if (!spotTransform.TryGetComponent(out spot)) {
				//Debug.LogWarning($"'{spotTransform.gameObject.name}' does not have a PathRandomizerSpot component, so we are creating one.");
				spot = spotTransform.gameObject.AddComponent<PathRandomizerSpot>();
			}

			spot.DeactivateEverything();
		}

		for (int i = 0; i < transform.childCount; ++i) {
			Transform spotTransform = transform.GetChild(i);

			PathRandomizerSpot spot;
			if (!spotTransform.TryGetComponent(out spot)) {
#if UNITY_EDITOR
				Debug.LogError($"{spotTransform.gameObject.name} should have a PathRandomizerSpot component by now. Since it doesn't, we've done an oopsie. See the code please.");
				UnityEditor.EditorApplication.isPlaying = false;
				spot = spotTransform.gameObject.AddComponent<PathRandomizerSpot>();
#endif
			}

			spot.Randomize();
		}
	}
}
