using UnityEngine;

public class HingedDoor : MonoBehaviour {
	public bool  shouldBeOpen;
	public Axis rotateAround;
	public float degreesToOpen = 90.0f;
	public float moveSpeed = 15.0f;

	public Transform hinge;

	Vector3 originEulerAngles;
	Vector3 actualEulerAngles;

	void Start() {
		if (hinge != null) originEulerAngles = hinge.localEulerAngles;
	}

	void Update() {
		if (hinge == null) return;

		var target = originEulerAngles;
		if (shouldBeOpen) target[(int)rotateAround] += degreesToOpen;

		var delta = target - actualEulerAngles;

		var maxToRotate = moveSpeed * Time.deltaTime;
		if (Mathf.Abs(delta.x) > maxToRotate) delta.x = (delta.x / Mathf.Abs(delta.x)) * maxToRotate;
		if (Mathf.Abs(delta.y) > maxToRotate) delta.y = (delta.y / Mathf.Abs(delta.y)) * maxToRotate;
		if (Mathf.Abs(delta.z) > maxToRotate) delta.z = (delta.z / Mathf.Abs(delta.z)) * maxToRotate;
		actualEulerAngles += delta;

		hinge.localEulerAngles = actualEulerAngles;
	}

#if UNITY_EDITOR
	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		if (hinge != null) {
			Gizmos.DrawWireSphere(hinge.position, 0.1f);
		}
	}
#endif
}
