using UnityEngine;
using UnityEngine.Events;

public class PlayerEventAreaTrigger : MonoBehaviour {
	const string playerTag = "Player";

	public bool onlyTriggerEnterOnce;
	public bool onlyTriggerExitOnce;

	[Space(10)] public UnityEvent onTriggerEnter;
	[Space(10)] public UnityEvent onTriggerStay;
	[Space(10)] public UnityEvent onTriggerExit;

	bool hasTriggerEntered;
	void OnTriggerEnter(Collider other) {
		if (onlyTriggerEnterOnce && hasTriggerEntered) return;

		if (other.gameObject.CompareTag(playerTag)) {
			if (onTriggerEnter != null) {
				hasTriggerEntered = true;
				onTriggerEnter.Invoke();
			}
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.CompareTag(playerTag)) {
			if (onTriggerStay != null) {
				onTriggerStay.Invoke();
			}
		}
	}

	bool hasTriggerExited;
	void OnTriggerExit(Collider other) {
		if (onlyTriggerExitOnce && hasTriggerExited) return;

		if (other.gameObject.CompareTag(playerTag)) {
			if (onTriggerExit != null) {
				hasTriggerExited = true;
				onTriggerExit.Invoke();
			}
		}
	}
}
