using UnityEngine;

public class ClothBallMover : MonoBehaviour {
	Vector3 origin;
	void Start() {
		origin = transform.position;
	}

	public float speed = 1.0f;
	public float distance = 10.0f;
	void Update() {
		var newPosition = origin;
		newPosition.x += Mathf.Sin(Time.time * speed) * (distance / 2);
		transform.position = newPosition;
	}
}
