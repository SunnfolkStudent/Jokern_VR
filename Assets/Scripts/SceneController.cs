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
	}

	public void ReloadAllScenes() {
		// By reloading the scene controller as the only scene, we
		// effectively reload everything since it is responsible for
		// setting up all the other scenes.
		SceneManager.LoadScene("SceneController", LoadSceneMode.Single);
	}
}
