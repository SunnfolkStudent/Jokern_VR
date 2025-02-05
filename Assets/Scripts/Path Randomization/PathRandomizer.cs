using UnityEngine;
using System;
using System.Linq;
using Random = UnityEngine.Random;
using static AugustBase.All;

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

	Transform GetChunksContainerTransform() => GetFirstChildByNameOrStop("Chunks");
	Transform GetExitBlockersContainerTransform() => GetFirstChildByNameOrStop("Exit Blockers");

	// Not recursive!
	Transform GetFirstChildByName(string name) {
		for (int i = 0; i < transform.childCount; ++i) {
			var child = transform.GetChild(i);

			if (child.gameObject.name == name) {
				return child;
			}
		}

		return null;
	}

	// Not recursive!
	Transform GetFirstChildByNameOrStop(string name) {
		var child = GetFirstChildByName(name);
		if (child == null) {
#if UNITY_EDITOR
			Debug.LogError($"No child of '{this.name}' is called '{name}', but we expect there to be one.");
			StopProgram();
#endif
		}

		return child;
	}

	public void DisableAllChunks() {
		// Deactivate the chunks themselves.
		var chunksContainerTransform = GetChunksContainerTransform();
		for (int i = 0; i < chunksContainerTransform.childCount; ++i) {
			Transform chunkTransform = chunksContainerTransform.GetChild(i);

			PathRandomizerChunk chunk;
			if (!chunkTransform.TryGetComponent(out chunk)) {
				chunk = chunkTransform.gameObject.AddComponent<PathRandomizerChunk>();
			}

			chunk.Deactivate();
		}

		// Activate the exit blockers.
		var exitBlockersContainerTransform = GetExitBlockersContainerTransform();
		for (int i = 0; i < exitBlockersContainerTransform.childCount; ++i) {
			Transform exitBlockerTransform = exitBlockersContainerTransform.GetChild(i);

			PathRandomizerExitBlocker exitBlocker;
			if (!exitBlockerTransform.TryGetComponent(out exitBlocker)) {
				exitBlocker = exitBlockerTransform.gameObject.AddComponent<PathRandomizerExitBlocker>();
			}

			exitBlocker.Activate();
		}
	}

	public void RegenerateRandomChunks() {
		// First we reset everything to zero.
		DisableAllChunks();

		var chunksContainerTransform = GetChunksContainerTransform();

		var randomIndexes = new int[chunksContainerTransform.childCount];
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
			Transform chunkTransform = chunksContainerTransform.GetChild(randomIndexes[i]);

			PathRandomizerChunk chunk;
			if (!chunkTransform.TryGetComponent(out chunk)) {
				// The chunk doesn't have a PathRandomizerChunk component, so we add one!
				chunk = chunkTransform.gameObject.AddComponent<PathRandomizerChunk>();
			}

			chunk.AttemptActivation();
		}
	}
}
