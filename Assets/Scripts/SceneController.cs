using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using static AugustBase.All;

public class SceneController : MonoBehaviour {
	[Tooltip("These scenes will get loaded additively.")]
	public SceneAsset[] loadOnStartup;

	void Awake() {
		for (int i = 0; i < loadOnStartup.Length; ++i) {
			SceneManager.LoadScene(loadOnStartup[i].name, LoadSceneMode.Additive);
		}

		if (SceneIsLoaded("BasicScene")) {
			print("BasicScene is loaded!");
		}
	}

	public bool SceneIsLoaded(int buildIndex) {
		for (int i = 0; i < SceneManager.sceneCount; ++i) {
			var scene = SceneManager.GetSceneAt(i);
			if (scene.buildIndex < 0) {
				Debug.LogError($"Scene {scene.name} does is not in the build!");
			} else if (scene.buildIndex == buildIndex) {
				return true;
			}
		}

		return false;
	}

	public bool SceneIsLoaded(string name) {
		return SceneIsLoaded(SceneManager.GetSceneByName(name).buildIndex);
	}

	/*
	public void LoadScene(SceneDings dings) {
		// if (dings is not loaded) {
		SceneManager.LoadScene(, LoadSceneMode.Additive);
		// }
	}
	*/

	public void ReloadAllScenes() {
		// By reloading the scene controller as the only scene, we
		// effectively reload everything since it is responsible for
		// setting up all the other scenes.
		SceneManager.LoadScene("SceneController", LoadSceneMode.Single);
	}
}
