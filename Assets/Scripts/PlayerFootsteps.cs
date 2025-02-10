using System;
using UnityEngine;

public enum FootstepSoundType {
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
	public FootstepSoundType soundType;
}

public class PlayerFootsteps : MonoBehaviour {
	public Vector3 relativeRaycastFrom;
	public LayerMask groundMask = ~0;

	public TextureToFootstepSound[] textureToFootstepSounds;

	public FootstepSoundType currentlyStandingOn;

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

	public float footStepInterval = 0.4f;
	float lastFootstepWasAt;

	void PlayFootsteps() {
		if (lastFootstepWasAt + footStepInterval < Time.time) {
			lastFootstepWasAt = Time.time;
			theFMODController.PlayFootstepSound(currentlyStandingOn);
		}
	}
	public FMODController theFMODController;

	void Start() {
		if (theFMODController == null) {

			var found = UnityEngine.Object.FindObjectsByType<FMODController>(FindObjectsSortMode.None);

			if (found.Length > 1) {
				Debug.LogError($"Too many '{nameof(FMODController)}'s! There is only supposed to be one, but found {found.Length}!");
			} else if (found.Length == 0) {
				Debug.LogError($"Found no '{nameof(FMODController)}'! Please create one!");
			}

			theFMODController = found[0];
		}
	}

	void Update() {
		currentlyStandingOn = FootstepSoundType.None;

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

		if (currentlyStandingOn != FootstepSoundType.None) {
			PlayFootsteps();
		}
	}
}
