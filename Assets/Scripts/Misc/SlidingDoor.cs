using UnityEngine;

public class SlidingDoor : MonoBehaviour {
	public bool  shouldBeOpen;
	public float sizeToOpen = 1.0f;
	public float moveSpeed = 10.0f;

	Vector3 leftOriginPosition;
	Vector3 rightOriginPosition;

	public Transform leftDoor;
	public Transform rightDoor;

	void Start() {
		leftOriginPosition  = leftDoor.position;
		rightOriginPosition = rightDoor.position;
	}

	void MoveTowards(Transform toMove, Vector3 position) {
		var delta = position - toMove.position;

		var maxDistanceToMove = moveSpeed * Time.deltaTime;
		if (Mathf.Abs(delta.x) > maxDistanceToMove) delta.x = (delta.x / Mathf.Abs(delta.x)) * maxDistanceToMove;
		if (Mathf.Abs(delta.y) > maxDistanceToMove) delta.y = (delta.y / Mathf.Abs(delta.y)) * maxDistanceToMove;
		if (Mathf.Abs(delta.z) > maxDistanceToMove) delta.z = (delta.z / Mathf.Abs(delta.z)) * maxDistanceToMove;

		toMove.position += delta;
	}

	void Update() {
		var leftTargetPosition = leftOriginPosition;
		if (shouldBeOpen) leftTargetPosition.z  -= sizeToOpen * 0.5f;
		MoveTowards(leftDoor, leftTargetPosition);

		var rightTargetPosition = rightOriginPosition;
		if (shouldBeOpen) rightTargetPosition.z += sizeToOpen * 0.5f;
		MoveTowards(rightDoor, rightTargetPosition);
	}
}
