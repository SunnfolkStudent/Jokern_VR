using UnityEngine;
using UnityEngine.Events;

public class DunkTheClownMinigame : MonoBehaviour {
	const string ballTag = "Ball";

	[Tooltip("This event gets invoked when the ball hits the target.")]
	public UnityEvent onTargetHit;
	public bool targetWasHit;

	void Start() {
		targetWasHit = false;
	}

	void Hit() {
		targetWasHit = true;

		if (onTargetHit != null) {
			onTargetHit.Invoke();
		}
	}
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag(ballTag)) {
			Hit();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag(ballTag)) {
			Hit();
		}
	}
}
