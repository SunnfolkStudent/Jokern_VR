using UnityEngine;

public class AmbianceCalmingPoint : MonoBehaviour {
	public FMODController.AmbianceCalming calming = FMODController.AmbianceCalming.Calm;

	void OnTriggerStay(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			FMODController.currentAmbianceCalming = calming;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			FMODController.currentAmbianceCalming = FMODController.AmbianceCalming.Normal;
		}
	}
}
