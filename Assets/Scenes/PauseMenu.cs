using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Menu Settings")]
    [SerializeField] private GameObject _settingMenu;
    public static bool _menuActive = false;
    
    // TODO: Input actions such as interacting with the diamond ore, and picking up and putting down objects need to be disabled while Time.timeScale = 0;
    
    private void Start()
    {
        SwitchMenu();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        if (InputActions.Pause)   
        {
            TurnMenuOff();
        }
    }

    private void TurnMenuOff()
    {
        _menuActive = !_menuActive;
        if (_menuActive)
        {
            Time.timeScale = 0;
            SwitchMenu();
        }
        else
        {
            Time.timeScale = 1;
            SwitchMenu();
        }
    }

    private void SwitchMenu()
    {
        _settingMenu.SetActive(_menuActive);
    }
}
