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
		for (int i = 0; i < transform.childCount; ++i) {
			Transform spot = transform.GetChild(i);

			int chunkToSelect = Random.Range(0, spot.childCount);
			for (int j = 0; j < spot.childCount; ++j) {
				var chunk = spot.GetChild(j);
				chunk.gameObject.SetActive(j == chunkToSelect);
			}
		}
	}
}
