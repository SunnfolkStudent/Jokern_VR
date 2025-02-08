using System;
using UnityEngine;

public enum FootstepSoundType {
	None,
	Asfalt,
	Sti,
	Sten,
	Vann,
	Gjørme,
	Høytgress,
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

	void PlaySoundBasedOnMaterial(Material material) {
		if (material == null) return;

		var texture = material.mainTexture;
		if (texture == null) return;

		for (int i = 0; i < textureToFootstepSounds.Length; ++i) {
			var item = textureToFootstepSounds[i];
			if (item.texture      == null) continue;
			if (item.texture.name == null) continue;

			if (item.texture.name == texture.name) {
				//print($"BINGO! Playing sound for texture '{texture.name}'.");

				currentlyStandingOn = item.soundType;
				//SetFMODParameter("Eg. FootstepsSoundType", item.soundType);
			}
		}
	}

	void Update() {
		RaycastHit hit;
		if (Physics.Raycast(transform.position + relativeRaycastFrom, Vector3.down, out hit, groundMask)) {
			if (hit.transform != null) {
				var obj = hit.transform.gameObject;
				if (obj.TryGetComponent<Renderer>(out Renderer renderer)) {
					// .sharedMaterial is shared; changing it will change it for the object as well.
					// .material is not shared and makes a copy.
					PlaySoundBasedOnMaterial(renderer.sharedMaterial);
				}
			}
		}
	}
}
