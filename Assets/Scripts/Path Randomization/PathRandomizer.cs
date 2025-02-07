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


	PathRandomizerModule[] modules;
	PathRandomizerExitBlocker[] exitBlockers;

	void FindAllModulesAndExitBlockers() {
		modules = UnityEngine.Object.FindObjectsByType<PathRandomizerModule>(FindObjectsInactive.Include, FindObjectsSortMode.None);
#if UNITY_EDITOR
		if (modules.Length == 0) {
			Debug.LogError("No modules in any active scene!");
		}
#endif

		exitBlockers = UnityEngine.Object.FindObjectsByType<PathRandomizerExitBlocker>(FindObjectsInactive.Include, FindObjectsSortMode.None);
#if UNITY_EDITOR
		if (exitBlockers.Length == 0) {
			Debug.LogError("No exit blockers in any active scene!");
		}
#endif
	}

	void Start() {
		RegenerateRandomModules();
	}

	public void DisableAllModules() {
		// Deactivate the modules themselves.
		for (int i = 0; i < modules.Length; ++i) {
			if (modules[i] != null) {
				modules[i].Deactivate();
			}
		}

		// Activate all the exit blockers.
		for (int i = 0; i < exitBlockers.Length; ++i) {
			if (exitBlockers[i] != null) {
				exitBlockers[i].Activate();
			}
		}
	}

	public void RegenerateRandomModules() {
		FindAllModulesAndExitBlockers();

		// First we reset everything to zero.
		DisableAllModules();

		var randomIndexes = new int[modules.Length];
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
			var module = modules[randomIndexes[i]];
			module.AttemptActivation();
		}
	}
}
