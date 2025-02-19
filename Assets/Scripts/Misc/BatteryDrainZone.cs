using UnityEngine;

public class BatteryDrainZone : MonoBehaviour {
	FlashlightMaster2 dings;
	public void DrainBattery() {
		if (dings == null) {
			dings = UnityEngine.Object.FindAnyObjectByType<FlashlightMaster2>();
		}

		if (dings != null) {
			dings.shakeIntensity = Mathf.Infinity;
		}
	}
}
