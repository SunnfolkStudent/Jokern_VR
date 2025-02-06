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

	Bounds GetGameObjectBounds(GameObject obj) {
		Bounds result;

		Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
		if (renderers.Length > 0) {
			// Take the first element as the starting point!
			result = renderers[0].bounds;
			for (int i = 1; i < renderers.Length; ++i) {
				result.Encapsulate(renderers[i].bounds);
			}
		} else {
			result = new Bounds();
		}

		/*
		MeshRenderer[] meshRenderers = obj.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < meshRenderers.Length; ++i) {
			result.Encapsulate(meshRenderers[i].bounds);
		}

		SkinnedMeshRenderer[] skinnedMeshRenderers = obj.GetComponentsInChildren<SkinnedMeshRenderer>();
		for (int i = 0; i < skinnedMeshRenderers.Length; ++i) {
			result.Encapsulate(skinnedMeshRenderers[i].bounds);
		}
		*/

		return result;
	}

	Bounds GetChunkBounds(ChunkLoaderChunk chunk) {
		Bounds result = GetGameObjectBounds(chunk.gameObject);

		var extraToInclude = chunk.includeTheseInTheChunk;
		if (extraToInclude != null) {
			for (int i = 0; i < extraToInclude.Length; ++i) {
				var obj = extraToInclude[i];
				if (obj != null) {
					result.Encapsulate(GetGameObjectBounds(obj));
				}
			}
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
		if (chunks != null) {
			Gizmos.color = Color.blue;
			for (int i = 0; i < chunks.Length; ++i) {
				var bounds = GetChunkBounds(chunks[i]);
				Gizmos.DrawWireCube(bounds.center, bounds.size);
			}
		}
	}

	[Tooltip("Draw a cubed outline around every chunk.")]
	[SerializeField] bool drawChunkBounds;

	[Tooltip("Draw a cubed outline around every game object in every chunk, colored on a per-chunk basis. Items that do not have a chunk assigned to them do not get an outline.")]
	[SerializeField] bool drawChunkItemBounds;

	void DrawChunkGizmos() {
		if (chunks != null) {
			var oldState = Random.state;
			Random.InitState(69420);

			for (int i = 0; i < chunks.Length; ++i) {
				Gizmos.color = new Color(Random.value, Random.value, Random.value);

				if (drawChunkBounds) {
					var bounds = GetChunkBounds(chunks[i]);
					Gizmos.DrawWireCube(bounds.center, bounds.size);
				}

				if (drawChunkItemBounds) {
					Bounds parentItemBounds = GetGameObjectBounds(chunks[i].gameObject);
					Gizmos.DrawWireCube(parentItemBounds.center, parentItemBounds.size);

					var extraToInclude = chunks[i].includeTheseInTheChunk;
					if (extraToInclude != null) {
						for (int j = 0; j < extraToInclude.Length; ++j) {
							GameObject obj = extraToInclude[j];

							if (obj != null) {
								Bounds objBounds = GetGameObjectBounds(obj);
								Gizmos.DrawWireCube(objBounds.center, objBounds.size);
							}
						}
					}
				}
			}

			Random.state = oldState;
		}
	}

	void OnDrawGizmos() {
		FindAllChunks();
		DrawChunkGizmos();
	}
#endif
}
