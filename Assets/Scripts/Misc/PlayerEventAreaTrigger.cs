using UnityEngine;
using UnityEngine.Events;

public class PlayerEventAreaTrigger : MonoBehaviour {
	const string playerTag = "Player";

	[Space(10)] public UnityEvent onTriggerEnter;
	[Space(10)] public UnityEvent onTriggerStay;
	[Space(10)] public UnityEvent onTriggerExit;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag(playerTag)) {
			if (onTriggerEnter != null) {
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

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag(playerTag)) {
			if (onTriggerExit != null) {
				onTriggerExit.Invoke();
			}
		}
	}
}
