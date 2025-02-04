using UnityEngine;

public class PathRandomizerSpot : MonoBehaviour {
	public void Randomize() {
		int chunkToSelect = Random.Range(0, transform.childCount);
		var chunkTransform = transform.GetChild(chunkToSelect);

		PathRandomizerChunk chunk;
		if (!chunkTransform.TryGetComponent(out chunk)) {
			//Debug.LogWarning($"'{chunkTransform.gameObject.name}' does not have a PathRandomizerChunk component, so we are creating one.");
			chunk = chunkTransform.gameObject.AddComponent<PathRandomizerChunk>();
		}

		chunk.Activate();
	}

	public void DeactivateEverything() {
		for (int i = 0; i < transform.childCount; ++i) {
			var chunkTransform = transform.GetChild(i);

			PathRandomizerChunk chunk;
			if (!chunkTransform.TryGetComponent(out chunk)) {
				//Debug.LogWarning($"'{chunkTransform.gameObject.name}' does not have a PathRandomizerChunk component, so we are creating one.");
				chunk = chunkTransform.gameObject.AddComponent<PathRandomizerChunk>();
			}

			chunk.Deactivate();
		}
	}
}
