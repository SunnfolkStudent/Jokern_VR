using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static AugustBase.All;

[Serializable]
public struct Level {
	public string name;
	public string[] scenes;
}

public class SceneController : MonoBehaviour {
	[Tooltip("These scenes will get loaded additively.")]
	public string[] loadOnStartup;

	[Space(10)]
	[Header("Levels")]
	public int currentLevel;
	public Level[] levels;

	void Awake() {
		for (int i = 0; i < loadOnStartup.Length; ++i) {
			LoadScene(loadOnStartup[i]);
		}
	}

	void Start() {
		LoadLevel(currentLevel);
	}

	int previousCurrentLevel;
	void Update() {
		if (previousCurrentLevel != currentLevel) {
			if (currentLevel < 0 || levels.Length <= currentLevel) {
				LogNoSuchLevelExists(currentLevel);
			} else {
				UnloadLevelScenes(previousCurrentLevel);
				LoadLevelScenes(currentLevel);

				previousCurrentLevel = currentLevel;
			}
		}
	}

	void LogNoSuchLevelExists(int level) {
		Debug.LogError($"Try to load level {level}, but no such level exists. Please assign levels to the Levels field on the Scene Controller.");
	}

	public void LoadScene(int buildIndex) => SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
	public void LoadScene(string name)    => SceneManager.LoadSceneAsync(name,       LoadSceneMode.Additive);

	void UnloadLevelScenes(int level) {
		if (level < 0 || levels.Length <= level) {
			LogNoSuchLevelExists(level);
			return;
		}

		if (levels[level].scenes == null) return;

		for (int i = 0; i < levels[level].scenes.Length; ++i) {
			if (levels[level].scenes[i] != null) {
				UnloadScene(levels[level].scenes[i]);
			}
		}
	}

	void LoadLevelScenes(int level) {
		if (level < 0 || levels.Length <= level) {
			LogNoSuchLevelExists(level);
			return;
		}

		if (levels[level].scenes == null) return;

		for (int i = 0; i < levels[level].scenes.Length; ++i) {
			if (levels[level].scenes[i] != null) {
				LoadScene(levels[level].scenes[i]);
			}
		}
	}

	public void LoadLevel(int level) {
		if (level < levels.Length) {
			UnloadLevelScenes(currentLevel);
			currentLevel = level;
			LoadLevelScenes(currentLevel);
		} else {
			LogNoSuchLevelExists(level);
		}
	}

	public void LoadNextLevel()     => currentLevel += 1;
	public void LoadPreviousLevel() => currentLevel -= 1;

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

	public void RestartTheGame() {
		// By reloading the scene controller as the only scene, we
		// effectively restart everything since it is responsible for
		// setting up all the other scenes.
		SceneManager.LoadScene("SceneController", LoadSceneMode.Single);
	}
}
