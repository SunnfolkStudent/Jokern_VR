using UnityEngine;

public class PathRandomizerExitBlocker : MonoBehaviour {
	[HideInInspector] public bool isActive;

	public void Deactivate() {
		gameObject.SetActive(false);
		isActive = false;
	}

	public void Activate() {
		gameObject.SetActive(true);
		isActive = true;
	}
}
