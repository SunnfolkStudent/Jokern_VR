using UnityEngine;

public class PathRandomizerChunk : MonoBehaviour {
	[Tooltip("Increase this value to make this chunk randomly avoid spawning more often.")]
	[Range(0.0f, 1.0f)]
	public float chanceToBeEmpty = 0.0f;
	public PathRandomizerChunk[] doNotSpawnIfTheseAreActive;

	public PathRandomizerExitBlocker[] unblockTheseExitsIfActive;

	public GameObject[] theseAreAlsoPartOfTheChunk;

	public void Deactivate() {
		SetActiveState(false);
	}

	void SetActiveState(bool active) {
		gameObject.SetActive(active);
		isActive = active;

		if (theseAreAlsoPartOfTheChunk != null) {
			for (int i = 0; i < theseAreAlsoPartOfTheChunk.Length; ++i) {
				theseAreAlsoPartOfTheChunk[i].SetActive(active);
			}
		}
	}

	[HideInInspector] public bool isActive { get; private set; }

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

		if (unblockTheseExitsIfActive != null) {
			for (int i = 0; i < unblockTheseExitsIfActive.Length; ++i) {
				if (unblockTheseExitsIfActive[i] != null) {
					unblockTheseExitsIfActive[i].Deactivate();
				}
			}
		}

		SetActiveState(true);
	}
}
