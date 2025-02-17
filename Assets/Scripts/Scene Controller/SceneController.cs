using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
using static AugustBase.All;

[Serializable]
public struct Level {
	public string name;

	[Tooltip("The scene that becomes the active scene when this level is loaded.")]
	public string setAsActiveScene;

	[FormerlySerializedAs("additionalScenes")]
	public string[] scenes;
}

public class SceneController : MonoBehaviour {
	[HideInInspector]
	public static SceneController instance;

	[Tooltip("These scenes will get loaded additively.")]
	public string[] loadOnStartup;

	[Space(10)]
	public string[] mainMenuScenes;

	[Space(10)]
	[Header("Levels")]
	public int currentLevel;
	public Level[] levels;
	void Awake() {
		if (instance != null) {
			Destroy(this);
			return;
		}

		instance = this;

		if (loadOnStartup != null) {
			for (int i = 0; i < loadOnStartup.Length; ++i) {
				LoadScene(loadOnStartup[i]);
			}
		}

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void Start() {
		OpenMainMenu();
	}

	public static void CloseMainMenu() {
		if (!instance.mainMenuIsLoaded) return;

		if (instance.mainMenuScenes != null) {
			for (int i = 0; i < instance.mainMenuScenes.Length; ++i) {
				if (instance.mainMenuScenes[i] != null) {
					UnloadScene(instance.mainMenuScenes[i]);
				}
			}
		}

		LoadLevelScenes(instance.currentLevel);
		instance.previousCurrentLevel = instance.currentLevel;

		instance.mainMenuIsLoaded = false;
	}

	[HideInInspector] public bool mainMenuIsLoaded;
	public static void OpenMainMenu() {
		if (instance.mainMenuIsLoaded) return;

		UnloadLevelScenes(instance.currentLevel);
		instance.previousCurrentLevel = instance.currentLevel;

		if (instance.mainMenuScenes != null) {
			for (int i = 0; i < instance.mainMenuScenes.Length; ++i) {
				if (instance.mainMenuScenes[i] != null) {
					LoadScene(instance.mainMenuScenes[i]);
				}
			}
		}

		instance.mainMenuIsLoaded = true;
	}

	int previousCurrentLevel;
	void Update() {
		if (instance.mainMenuIsLoaded) {
			FMODController.currentAmbianceStage = FMODController.AmbianceStage.MainMenu;
		} else {
			// @Hardcoded
			if (SceneIsLoaded("CreditsForest_Events")) {
				FMODController.currentAmbianceStage = FMODController.AmbianceStage.Credits;
			} else {
				FMODController.currentAmbianceStage = FMODController.AmbianceStage.Levels;
			}

			// @Hardcoded
			if (0 <= instance.currentLevel && instance.currentLevel < levels.Length) {
				switch (levels[instance.currentLevel].name) {
					case "Intro":         FMODController.currentLevel = FMODController.Level.Intro;        break;
					case "Joker Forest":  FMODController.currentLevel = FMODController.Level.JokerForest;  break;
					case "Dark Joker":    FMODController.currentLevel = FMODController.Level.DarkJoker;    break;
					case "Circus Forest": FMODController.currentLevel = FMODController.Level.CircusForest; break;
					case "Circus":        FMODController.currentLevel = FMODController.Level.Circus;       break;
					case "Final Path":    FMODController.currentLevel = FMODController.Level.FinalPath;    break;
					case "Lit Joker":     FMODController.currentLevel = FMODController.Level.LitJoker;     break;
					case "Credits Forest": break; // The credits forest has its own thing!

					default: {
						Debug.LogError($"Level name '{levels[instance.currentLevel].name}' not recognized!");
					} break;
				}
			}

			if (instance.previousCurrentLevel != instance.currentLevel) {
				if (instance.currentLevel < 0 || instance.levels.Length <= instance.currentLevel) {
					LogNoSuchLevelExists(instance.currentLevel);
				} else {
					UnloadLevelScenes(instance.previousCurrentLevel);
					LoadLevelScenes(instance.currentLevel);

					instance.previousCurrentLevel = instance.currentLevel;
				}
			}
		}
	}

	static void LogNoSuchLevelExists(int level) {
		Debug.LogError($"Try to load level {level}, but no such level exists. Please assign levels to the Levels field on the Scene Controller.");
	}

	public static void LoadScene(int buildIndex) => SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
	public static void LoadScene(string name)    => SceneManager.LoadSceneAsync(name,       LoadSceneMode.Additive);

	public static void SetActiveScene(int buildIndex) {
		var scene = SceneManager.GetSceneByBuildIndex(buildIndex);
		SceneManager.SetActiveScene(scene);
	}

	public static void SetActiveScene(string name) {
		var scene = SceneManager.GetSceneByName(name);
		SceneManager.SetActiveScene(scene);
	}

	static void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		if (!scene.isLoaded) {
			// Sanity check.
			Debug.LogError("OnSceneLoaded got a scene that isn't loaded!?");
		}

		if (scene.name == instance.levels[instance.currentLevel].setAsActiveScene) {
			SceneManager.SetActiveScene(scene);
		}
	}

	static void UnloadLevelScenes(int level) {
		if (level < 0 || instance.levels.Length <= level) {
			LogNoSuchLevelExists(level);
			return;
		}

		if (instance.levels[level].scenes == null) return;

		for (int i = 0; i < instance.levels[level].scenes.Length; ++i) {
			if (instance.levels[level].scenes[i] != null) {
				UnloadScene(instance.levels[level].scenes[i]);
			}
		}
	}

	static void LoadLevelScenes(int level) {
		if (level < 0 || instance.levels.Length <= level) {
			LogNoSuchLevelExists(level);
			return;
		}

		if (instance.levels[level].scenes == null) return;

		for (int i = 0; i < instance.levels[level].scenes.Length; ++i) {
			if (instance.levels[level].scenes[i] != null) {
				LoadScene(instance.levels[level].scenes[i]);
			}
		}
	}

	public static void LoadLevel(int level) {
		if (level < instance.levels.Length) {
			if (instance.mainMenuIsLoaded) CloseMainMenu();

			UnloadLevelScenes(instance.currentLevel);
			instance.currentLevel = level;
			LoadLevelScenes(instance.currentLevel);
		} else {
			LogNoSuchLevelExists(level);
		}
	}

	public static void LoadNextLevel()     => instance.currentLevel += 1;
	public static void LoadPreviousLevel() => instance.currentLevel -= 1;

	public static bool SceneIsLoaded(int buildIndex) {
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

	public static bool SceneIsLoaded(string name) {
		return SceneIsLoaded(SceneNameToBuildIndex(name));
	}

	public static void LoadSceneIfNotLoaded(int buildIndex) {
		if (!SceneIsLoaded(buildIndex)) {
			LoadScene(buildIndex);
		}
	}

	public static void LoadSceneIfNotLoaded(string name) {
		if (!SceneIsLoaded(name)) {
			LoadScene(name);
		}
	}

	// NOTE: UnloadScene() may not work in Awake().
	public static void UnloadScene(int buildIndex) {
		if (SceneIsLoaded(buildIndex)) {
			SceneManager.UnloadSceneAsync(buildIndex);
		}
	}

	public static void UnloadScene(string name) {
		if (SceneIsLoaded(name)) {
			SceneManager.UnloadSceneAsync(name);
		}
	}

	public static int SceneNameToBuildIndex(string name) {
		return SceneManager.GetSceneByName(name).buildIndex;
	}

	public static void RestartTheGame() {
		// By reloading the scene controller as the only scene, we
		// effectively restart everything since it is responsible for
		// setting up all the other scenes.
		SceneManager.LoadScene("SceneController", LoadSceneMode.Single);
	}
}
