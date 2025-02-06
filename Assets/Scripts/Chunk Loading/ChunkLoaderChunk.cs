using UnityEngine;

public class ChunkLoaderChunk : MonoBehaviour {
	[Tooltip("If you have some objects that aren't children of this one, but you still want them to be included in the chunk then assign them here.")]
	public GameObject[] includeTheseInTheChunk;
}
