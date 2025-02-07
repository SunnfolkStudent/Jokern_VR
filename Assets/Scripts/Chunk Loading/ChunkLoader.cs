using UnityEngine;
using static AugustBase.All;

public class ChunkLoader : MonoBehaviour {
	ChunkLoaderChunk[] chunks;

	public void FindAllChunks() {
		chunks = UnityEngine.Object.FindObjectsByType<ChunkLoaderChunk>(FindObjectsInactive.Include, FindObjectsSortMode.None);
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

		/* @ChunkExtraToIncludeField
		var extraToInclude = chunk.includeTheseInTheChunk;
		if (extraToInclude != null) {
			for (int i = 0; i < extraToInclude.Length; ++i) {
				var obj = extraToInclude[i];
				if (obj != null) {
					result.Encapsulate(GetGameObjectBounds(obj));
				}
			}
		}
		*/

		return result;
	}

	public float simulationDistance;

	public Transform playerTransform;
	void FixedUpdate() {
		FindAllChunks();

		for (int i = 0; i < chunks.Length; ++i) {
			if (ChunkShouldBeLoaded(chunks[i])) {
				chunks[i].Load();
			} else {
				chunks[i].Unload();
			}
		}
	}

	// One could imagine this function getting more complicated in the future.
	public bool ChunkShouldBeLoaded(ChunkLoaderChunk chunk) {
		if (playerTransform != null) {
			var distanceToChunk = DistanceToChunk(playerTransform.position, chunk);
			return distanceToChunk < simulationDistance;
		}

#if UNITY_EDITOR
		Debug.LogError($"No player transform assigned to chunk loader '{this.name}'.");
		StopProgram();
#endif

		return false;
	}

	public float DistanceToChunk(Vector3 position, ChunkLoaderChunk chunk) {
		Bounds bounds = GetChunkBounds(chunk);
		Vector3 closest = bounds.ClosestPoint(position);
		var delta = position - closest;
		return Abs(delta).magnitude;
	}

#if UNITY_EDITOR
	[Tooltip("Draw a cubed outline around every chunk, colored on a per-chunk basis.")]
	[SerializeField] bool drawChunkBounds;

	[Tooltip("Draw a cubed outline around every game object in every chunk, colored on a per-chunk basis. Items that do not have a chunk assigned to them do not get an outline.")]
	[SerializeField] bool drawChunkItemBounds;

	[Tooltip("Draw a blue, cubed outline around every loaded chunk.")]
	[SerializeField] bool drawLoadedChunkBounds;

	void DrawGameObjectBoundsGizmos(GameObject obj) {
		Bounds parentItemBounds = GetGameObjectBounds(obj);
		Gizmos.DrawWireCube(parentItemBounds.center, parentItemBounds.size);

		for (int i = 0; i < obj.transform.childCount; ++i) {
			DrawGameObjectBoundsGizmos(obj.transform.GetChild(i).gameObject);
		}
	}

	void DrawChunkGizmos() {
		if (chunks != null) {
			var oldState = Random.state;
			Random.InitState(69420);

			for (int i = 0; i < chunks.Length; ++i) {
				if (chunks[i].neverDrawThisChunkGizmo) continue;

				{ // Per-chunk coloring!
					Gizmos.color = new Color(Random.value, Random.value, Random.value);

					if (drawChunkBounds) {
						var bounds = GetChunkBounds(chunks[i]);
						Gizmos.DrawWireCube(bounds.center, bounds.size);
					}

					if (drawChunkItemBounds) {
						DrawGameObjectBoundsGizmos(chunks[i].gameObject);

						/* @ChunkExtraToIncludeField
						var extraToInclude = chunks[i].includeTheseInTheChunk;
						if (extraToInclude != null) {
							for (int j = 0; j < extraToInclude.Length; ++j) {
								GameObject obj = extraToInclude[j];

								if (obj != null) {
									DrawGameObjectBoundsGizmos(obj);
								}
							}
						}
						*/
					}
				}

				if (drawLoadedChunkBounds) {
					Gizmos.color = Color.blue;
					if (ChunkShouldBeLoaded(chunks[i])) {
						var bounds = GetChunkBounds(chunks[i]);
						Gizmos.DrawWireCube(bounds.center, bounds.size);
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
