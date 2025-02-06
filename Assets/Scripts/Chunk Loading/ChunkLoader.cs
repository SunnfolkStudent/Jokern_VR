using UnityEngine;

public class ChunkLoader : MonoBehaviour {
	ChunkLoaderChunk[] chunks;

	public void FindAllChunks() {
		chunks = UnityEngine.Object.FindObjectsByType<ChunkLoaderChunk>(FindObjectsSortMode.None);
#if UNITY_EDITOR
		if (chunks.Length == 0) {
			Debug.LogError("No chunks in any active scene!");
		}
#endif
	}

	void Start() {
		FindAllChunks();
	}

	Bounds GetChunkBounds(ChunkLoaderChunk chunk) {
		Bounds result;

		Renderer[] renderers = chunk.gameObject.GetComponentsInChildren<Renderer>();
		if (renderers.Length > 0) {
			// Take the first element as the starting point!
			result = renderers[0].bounds;
			for (int i = 1; i < renderers.Length; ++i) {
				result.Encapsulate(renderers[i].bounds);
			}
		} else {
			result = new Bounds();
		}

		MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < meshRenderers.Length; ++i) {
			result.Encapsulate(meshRenderers[i].bounds);
		}

		SkinnedMeshRenderer[] skinnedMeshRenderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
		for (int i = 0; i < skinnedMeshRenderers.Length; ++i) {
			result.Encapsulate(skinnedMeshRenderers[i].bounds);
		}

		return result;
	}

	void Update() {
#if false
		Gizmos.color = Color.blue;
		for (int i = 0; i < chunks.Length; ++i) {
			var bounds = GetChunkBounds(chunks[i]);
		}
#endif
	}

	public Vector3 playerPosition;
	void GivePlayerPosition(Vector3 position) {
		playerPosition = position;
	}

#if UNITY_EDITOR
	void DrawChunkBoundsGizmos() {
		Gizmos.color = Color.blue;
		for (int i = 0; i < chunks.Length; ++i) {
			var bounds = GetChunkBounds(chunks[i]);
			Gizmos.DrawWireCube(bounds.center, bounds.size);
		}
	}

	[SerializeField] private bool reloadChunks;
	void OnDrawGizmos() {
		if (chunks == null || reloadChunks) {
			FindAllChunks();
			reloadChunks = false;
		}

		DrawChunkBoundsGizmos();
	}
#endif
}
