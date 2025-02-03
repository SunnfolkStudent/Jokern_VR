using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using static AugustBase.All;

public class SceneController : MonoBehaviour {
	// TODO: This may break on build since we are using something from UnityEditor.
	[Tooltip("These scenes will get loaded additively.")]
	public SceneAsset[] loadOnStartup;

	void Awake() {
		for (int i = 0; i < loadOnStartup.Length; ++i) {
			SceneManager.LoadScene(loadOnStartup[i].name, LoadSceneMode.Additive);
		}
	}

	void Start() {
		if (SceneIsLoaded("BasicScene")) {
			print("BasicScene is loaded!");
		}

		LoadSceneIfNotAlreadyLoaded("BasicScene");
	}

	public bool SceneIsLoaded(int buildIndex) {
		for (int i = 0; i < SceneManager.sceneCount; ++i) {
			var scene = SceneManager.GetSceneAt(i);
			if (scene.buildIndex < 0) {
				Debug.LogError($"Scene '{scene.name}' is not in the build!");
			} else if (scene.buildIndex == buildIndex) {
				return true;
			}
		}

		return false;
	}

	public bool SceneIsLoaded(string name) {
		return SceneIsLoaded(SceneNameToBuildIndex(name));
	}

	public void LoadSceneIfNotAlreadyLoaded(int buildIndex) {
		if (!SceneIsLoaded(buildIndex)) {
			LoadScene(buildIndex);
		}
	}

	public void LoadSceneIfNotAlreadyLoaded(string name) {
		if (!SceneIsLoaded(name)) {
			LoadScene(name);
		}
	}

	// NOTE: These functions may not work in Awake().
	public void UnloadScene(int buildIndex) => SceneManager.UnloadSceneAsync(buildIndex);
	public void UnloadScene(string name)    => SceneManager.UnloadSceneAsync(name);

	public int SceneNameToBuildIndex(string name) {
		return SceneManager.GetSceneByName(name).buildIndex;
	}

	public void LoadScene(string name) {
		SceneManager.LoadScene(name, LoadSceneMode.Additive);
	}

	public void LoadScene(int buildIndex) {
		SceneManager.LoadScene(buildIndex, LoadSceneMode.Additive);
	}

	public void ReloadAllScenes() {
		// By reloading the scene controller as the only scene, we
		// effectively reload everything since it is responsible for
		// setting up all the other scenes.
		SceneManager.LoadScene("SceneController", LoadSceneMode.Single);
	}
}
