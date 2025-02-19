using UnityEngine;

public class JokerForestCircusOutsideAmbianceChanger : MonoBehaviour {
	void OnTriggerStay(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			FMODController.jokerForestCircusOutside = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			FMODController.jokerForestCircusOutside = false;
		}
	}
}
