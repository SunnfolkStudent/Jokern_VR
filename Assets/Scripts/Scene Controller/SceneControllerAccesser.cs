using UnityEngine;

public class SceneControllerAccesser : MonoBehaviour {
	public static void CloseMainMenu() => SceneController.CloseMainMenu();
	public static void OpenMainMenu()  => SceneController.OpenMainMenu();
}
