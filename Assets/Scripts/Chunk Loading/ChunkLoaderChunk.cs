using UnityEngine;

public class ChunkLoaderChunk : MonoBehaviour {
	[Tooltip("If you have some objects that aren't children of this one, but you still want them to be included in the chunk then assign them here.")]
	public GameObject[] includeTheseInTheChunk;

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

		if (includeTheseInTheChunk != null) {
			for (int i = 0; i < includeTheseInTheChunk.Length; ++i) {
				includeTheseInTheChunk[i].SetActive(loaded);
			}
		}
	}
}
