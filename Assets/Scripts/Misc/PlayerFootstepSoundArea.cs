using UnityEngine;

public class PlayerFootstepSoundArea : MonoBehaviour {
	public FootstepSound areaSound;

	void OnDrawGizmos() {
		var previousMatrix = Gizmos.matrix;
		Gizmos.matrix = transform.localToWorldMatrix;

		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(Vector3.zero, transform.localScale);

		Gizmos.matrix = previousMatrix;
	}
}
