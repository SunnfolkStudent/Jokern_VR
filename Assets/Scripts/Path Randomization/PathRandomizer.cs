using UnityEngine;
using System;
using System.Linq;
using Random = UnityEngine.Random;
using static AugustBase.All;

public class PathRandomizer : MonoBehaviour {
	// @Temp TODO: remove this
	public bool regenerateRandomModules;
	void Update() {
		if (regenerateRandomModules) {
			RegenerateRandomModules();
			regenerateRandomModules = false;
		}
	}

	void Start() {
		GetModulesContainerTransform().gameObject.SetActive(true);
		GetExitBlockersContainerTransform().gameObject.SetActive(true);

		RegenerateRandomModules();
	}

	Transform GetModulesContainerTransform()       => transform.GetFirstChildByNameOrStop("Modules");
	Transform GetExitBlockersContainerTransform() => transform.GetFirstChildByNameOrStop("Exit Blockers");

	public void DisableAllModules() {
		// Deactivate the modules themselves.
		var modulesContainerTransform = GetModulesContainerTransform();
		for (int i = 0; i < modulesContainerTransform.childCount; ++i) {
			Transform moduleTransform = modulesContainerTransform.GetChild(i);

			PathRandomizerModule module;
			if (!moduleTransform.TryGetComponent(out module)) {
				module = moduleTransform.gameObject.AddComponent<PathRandomizerModule>();
			}

			module.Deactivate();
		}

		// Activate the exit blockers.
		var exitBlockersContainerTransform = GetExitBlockersContainerTransform();
		for (int i = 0; i < exitBlockersContainerTransform.childCount; ++i) {
			Transform exitBlockerTransform = exitBlockersContainerTransform.GetChild(i);

			PathRandomizerExitBlocker exitBlocker;
			if (!exitBlockerTransform.TryGetComponent(out exitBlocker)) {
				exitBlocker = exitBlockerTransform.gameObject.AddComponent<PathRandomizerExitBlocker>();
			}

			exitBlocker.Activate();
		}
	}

	public void RegenerateRandomModules() {
		// First we reset everything to zero.
		DisableAllModules();

		var modulesContainerTransform = GetModulesContainerTransform();

		var randomIndexes = new int[modulesContainerTransform.childCount];
		// Set all the members to their index.
		for (int i = 1; i < randomIndexes.Length; ++i) {
			randomIndexes[i] = i;
		}

		// Randomize!
		randomIndexes = randomIndexes.OrderBy(n => Guid.NewGuid()).ToArray();

#if false // Yeah it's random!
		print($"randomIndexes: (Length: {randomIndexes.Length})");
		for (int i = 0; i < randomIndexes.Length; ++i) {
			print($"    {randomIndexes[i]}");
		}
#endif

		for (int i = 0; i < randomIndexes.Length; ++i) {
			Transform moduleTransform = modulesContainerTransform.GetChild(randomIndexes[i]);

			PathRandomizerModule module;
			if (!moduleTransform.TryGetComponent(out module)) {
				// The module doesn't have a PathRandomizerModule component, so we add one!
				module = moduleTransform.gameObject.AddComponent<PathRandomizerModule>();
			}

			module.AttemptActivation();
		}
	}
}
