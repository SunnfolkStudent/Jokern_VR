using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum FootstepSound {
	None,
	Asfalt,
	Sti,
	Sten,
	Vann,
	Gjørme,
	HøytGress,
	Barnåler,
	Kvister,
	Mose,
	Grus,
}

[Serializable]
public class TextureToFootstepSound {
	public Texture texture;
	public FootstepSound soundType;
}

public class PlayerFootsteps : MonoBehaviour {
	public Vector3 relativeRaycastFrom;
	public LayerMask groundMask = ~0;

	public bool useTextureToFootstepSound;
	public TextureToFootstepSound[] textureToFootstepSounds;

	public FootstepSound currentlyStandingOn;

	void SetFootstepSoundBasedOnMaterial(Material material) {
		if (material == null) return;

		var texture = material.mainTexture;
		if (texture == null) return;

		for (int i = 0; i < textureToFootstepSounds.Length; ++i) {
			var item = textureToFootstepSounds[i];
			if (item.texture      == null) continue;
			if (item.texture.name == null) continue;

			if (item.texture.name == texture.name) {
				currentlyStandingOn = item.soundType;
			}
		}
	}

	public float minFootStepInterval = 0.60f;
	public float maxFootStepInterval = 0.70f;
	public bool  footstepIsOnRightFoot;

	public GameObject footprintDecal;

	float footStepInterval;
	float lastFootstepWasAt;
	void PlayFootsteps() {
		if (lastFootstepWasAt + footStepInterval < Time.time) {
			footStepInterval = Random.Range(minFootStepInterval, maxFootStepInterval);
			lastFootstepWasAt = Time.time;

			footstepIsOnRightFoot = !footstepIsOnRightFoot;

			FMODController.PlayFootstepSound(currentlyStandingOn, footstepIsOnRightFoot);

			var rotation = footprintDecal.transform.rotation;
			rotation *= PlayerMovement.playerMoveDirectionWithoutPitch.transform.rotation;
			// The footsteps should destroy themselves after some amount of time.
			Instantiate(footprintDecal, transform.position, rotation);
		}
	}

	void Start() {
		footStepInterval = Random.Range(minFootStepInterval, maxFootStepInterval);
	}

	public static bool isWalking;

	void Update() {
		FMODController.playerIsCurrentlyWalking = isWalking;
		if (!isWalking) return;

		if (useTextureToFootstepSound) {
			currentlyStandingOn = FootstepSound.None;

			RaycastHit hit;
			if (Physics.Raycast(transform.position + relativeRaycastFrom, Vector3.down, out hit, groundMask)) {
				if (hit.transform != null) {
					var obj = hit.transform.gameObject;
					if (obj.TryGetComponent<Renderer>(out Renderer renderer)) {
						// .sharedMaterial is shared; changing it will change it for the object as well.
						// .material is not shared and makes a copy.
						SetFootstepSoundBasedOnMaterial(renderer.sharedMaterial);
					}
				}
			}

		}

		if (currentlyStandingOn != FootstepSound.None) {
			PlayFootsteps();
		}
	}

	public FootstepSound defaultFootstepSound;
	[Tooltip("The layers that the player is on.")]
	public LayerMask playerMask = ~0;
	void FixedUpdate() {
		if (!useTextureToFootstepSound) {
			currentlyStandingOn = defaultFootstepSound;

			var soundAreas = UnityEngine.Object.FindObjectsByType<PlayerFootstepSoundArea>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

			for (int i = 0; i < soundAreas.Length; ++i) {
				var areaTransform = soundAreas[i].transform;
				Collider[] objectsInArea = Physics.OverlapBox(areaTransform.position,
				                                              areaTransform.localScale,
				                                              areaTransform.rotation,
				                                              playerMask);

				for (int j = 0; j < objectsInArea.Length; ++j) {
					if (objectsInArea[i].CompareTag("Player")) {
						currentlyStandingOn = soundAreas[i].areaSound;
					}
				}
			}
		}
	}
}
