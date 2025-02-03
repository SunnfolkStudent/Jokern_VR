using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using static AugustBase.All;

public class SceneController : MonoBehaviour {
	// TODO: This may break on build since we are using something from UnityEditor.
	[Tooltip("These scenes will get loaded additively.")]
	public SceneAsset[] loadOnStartup;

	[Space(10)]
	[Header("Levels")]
	public int currentLevel;
	public SceneAsset[] levelScenes;

	void Awake() {
		for (int i = 0; i < loadOnStartup.Length; ++i) {
			SceneManager.LoadScene(loadOnStartup[i].name, LoadSceneMode.Additive);
		}
	}

	void Start() {
		if (SceneIsLoaded("BasicScene")) {
			print("BasicScene is loaded!");
		}

		LoadLevel(0);
	}

	// @Temp TODO: remove this
	public bool goToNextLevel;
	void Update() {
		if (goToNextLevel) {
			LoadNextLevel();
			goToNextLevel = false;
		}
	}

	void LogNoSuchLevelExists(int level) {
		Debug.LogError($"Try to load level {level}, but no such level exists. Please assign levels to the Level Scenes field on the Scene Controller.");
	}

	public void LoadLevel(int level) {
		if (level < levelScenes.Length) {
			UnloadScene(levelScenes[currentLevel].name);
			LoadSceneIfNotLoaded(levelScenes[level].name);
			currentLevel = level;
		} else {
			LogNoSuchLevelExists(level);
		}
	}

	public void LoadNextLevel() {
		currentLevel += 1;
		if (currentLevel < levelScenes.Length) {
			UnloadScene(levelScenes[currentLevel - 1].name);
			LoadSceneIfNotLoaded(levelScenes[currentLevel].name);
		} else {
			LogNoSuchLevelExists(currentLevel);
		}
	}

	public void LoadPreviousLevel() {
		currentLevel -= 1;
		if (currentLevel >= 0) {
			UnloadScene(levelScenes[currentLevel + 1].name);
			LoadSceneIfNotLoaded(levelScenes[currentLevel].name);
		} else {
			LogNoSuchLevelExists(currentLevel);
		}
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

	public void LoadSceneIfNotLoaded(int buildIndex) {
		if (!SceneIsLoaded(buildIndex)) {
			LoadScene(buildIndex);
		}
	}

	public void LoadSceneIfNotLoaded(string name) {
		if (!SceneIsLoaded(name)) {
			LoadScene(name);
		}
	}

	// NOTE: UnloadScene() may not work in Awake().
	public void UnloadScene(int buildIndex) {
		if (SceneIsLoaded(buildIndex)) {
			SceneManager.UnloadSceneAsync(buildIndex);
		}
	}

	public void UnloadScene(string name) {
		if (SceneIsLoaded(name)) {
			SceneManager.UnloadSceneAsync(name);
		}
	}

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
