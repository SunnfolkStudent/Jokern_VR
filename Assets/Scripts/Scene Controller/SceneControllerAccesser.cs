using UnityEngine;

public class SceneControllerAccesser : MonoBehaviour {
	public static void CloseMainMenu()     => SceneController.CloseMainMenu();
	public static void OpenMainMenu()      => SceneController.OpenMainMenu();
	public static void QuitEverything()    => SceneController.QuitEverything();
	public static void LoadNextLevel()     => SceneController.LoadNextLevel();
	public static void LoadPreviousLevel() => SceneController.LoadPreviousLevel();
}
