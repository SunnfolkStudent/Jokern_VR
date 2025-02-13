using UnityEngine;

public enum DoorSlidingAxis {
	X, Y, Z,
}

public class SlidingDoor : MonoBehaviour {
	public bool  shouldBeOpen;
	public DoorSlidingAxis slidingAxis;
	public float sizeToOpen = 2.0f;
	public float moveSpeed = 3.0f;

	Vector3 leftOriginPosition;
	Vector3 rightOriginPosition;

	public Transform leftDoor;
	public Transform rightDoor;

	void Start() {
		if (leftDoor  != null) leftOriginPosition  = leftDoor.position;
		if (rightDoor != null) rightOriginPosition = rightDoor.position;
	}

	void MoveTowards(Transform toMove, Vector3 position) {
		if (toMove == null) return;

		var delta = position - toMove.position;

		var maxDistanceToMove = moveSpeed * Time.deltaTime;
		if (Mathf.Abs(delta.x) > maxDistanceToMove) delta.x = (delta.x / Mathf.Abs(delta.x)) * maxDistanceToMove;
		if (Mathf.Abs(delta.y) > maxDistanceToMove) delta.y = (delta.y / Mathf.Abs(delta.y)) * maxDistanceToMove;
		if (Mathf.Abs(delta.z) > maxDistanceToMove) delta.z = (delta.z / Mathf.Abs(delta.z)) * maxDistanceToMove;

		toMove.position += delta;
	}

	void Update() {
		var leftTargetPosition = leftOriginPosition;
		if (shouldBeOpen) leftTargetPosition[(int)slidingAxis] -= sizeToOpen;
		MoveTowards(leftDoor, leftTargetPosition);

		var rightTargetPosition = rightOriginPosition;
		if (shouldBeOpen) rightTargetPosition[(int)slidingAxis] += sizeToOpen;
		MoveTowards(rightDoor, rightTargetPosition);
	}
}
