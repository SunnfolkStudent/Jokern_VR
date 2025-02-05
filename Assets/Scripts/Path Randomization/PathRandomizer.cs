using UnityEngine;
using System;
using System.Linq;
using Random = UnityEngine.Random;

public class PathRandomizer : MonoBehaviour {
	public bool regenerateRandomChunks;
	void Update() {
		if (regenerateRandomChunks) {
			RegenerateRandomChunks();
			regenerateRandomChunks = false;
		}
	}

	void Start() {
		RegenerateRandomChunks();
	}

	public void DisableAllChunks() {
		// First we reset everything to zero.
		for (int i = 0; i < transform.childCount; ++i) {
			Transform chunkTransform = transform.GetChild(i);

			PathRandomizerChunk chunk;
			if (!chunkTransform.TryGetComponent(out chunk)) {
				chunk = chunkTransform.gameObject.AddComponent<PathRandomizerChunk>();
			}

			chunk.Deactivate();
		}
	}

	public void RegenerateRandomChunks() {
		DisableAllChunks();

		var randomIndexes = new int[transform.childCount];
		// Set all the members to their index.
		for (int i = 1; i < randomIndexes.Length; ++i) {
			randomIndexes[i] = i;
		}

		// Randomize!
		randomIndexes = randomIndexes.OrderBy(n => Guid.NewGuid()).ToArray();

#if false // Yeah it's random!
		print($"randomIndexes: (Length: {randomIndexes.Length})");
		for (int i = 0; i < randomIndexes.Length; ++i) {
			print($"    {randomIndexes[i]}");
		}
#endif

		for (int i = 0; i < randomIndexes.Length; ++i) {
			Transform chunkTransform = transform.GetChild(randomIndexes[i]);

			PathRandomizerChunk chunk;
			if (!chunkTransform.TryGetComponent(out chunk)) {
				// The chunk doesn't have a PathRandomizerChunk component, so we add one!
				chunk = chunkTransform.gameObject.AddComponent<PathRandomizerChunk>();
			}

			chunk.AttemptActivation();
		}
	}
}
