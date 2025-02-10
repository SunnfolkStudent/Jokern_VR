using UnityEngine;

public class ChunkLoaderChunk : MonoBehaviour {
#if UNITY_EDITOR
	public bool neverDrawThisChunkGizmo;
#endif

	/* TODO: This causes problems, but I still want the flexibility!
	 *       If you reenable this, make sure you uncomment the stuff in ChunkLoader.cs too!
	 *       They are all tagged with @ChunkExtraToIncludeField
	[Tooltip("If you have some objects that aren't children of this one, but you still want them to be included in the chunk then assign them here.")]
	public GameObject[] includeTheseInTheChunk;
	*/

	public void Unload() {
		SetLoadState(false);
	}

	public void Load() {
		SetLoadState(true);
	}

	[HideInInspector] public bool isLoaded;

	void SetLoadState(bool loaded) {
		gameObject.SetActive(loaded);
		isLoaded = loaded;

		/* @ChunkExtraToIncludeField
		if (includeTheseInTheChunk != null) {
			for (int i = 0; i < includeTheseInTheChunk.Length; ++i) {
				includeTheseInTheChunk[i].SetActive(loaded);
			}
		}
		*/
	}
}
