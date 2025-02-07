using UnityEngine;

public class PathRandomizerExitBlocker : MonoBehaviour {
	[HideInInspector] public bool isActive { get; private set; }

	public GameObject[] theseAreAlsoPartOfTheBlocker;

	void SetActiveState(bool active) {
		gameObject.SetActive(active);
		isActive = active;

		if (theseAreAlsoPartOfTheBlocker != null) {
			for (int i = 0; i < theseAreAlsoPartOfTheBlocker.Length; ++i) {
				theseAreAlsoPartOfTheBlocker[i].SetActive(active);
			}
		}
	}

	public void Deactivate() {
		SetActiveState(false);
	}

	public void Activate() {
		SetActiveState(true);
	}

	void Start() {
		if (TryGetComponent<ChunkLoaderChunk>(out ChunkLoaderChunk chunk)) {
			Debug.LogError($"Path exit blocker '{this.name}' has a chunk loader component, even though it shouldn't!");
		}
	}
}
