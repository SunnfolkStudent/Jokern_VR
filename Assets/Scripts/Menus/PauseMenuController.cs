using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public InputActionReference openMenuAction;
    public Transform cameraTransform;
    private void Awake()
    {
        openMenuAction.action.Enable();
        openMenuAction.action.performed += ToggleMenu;
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDestroy()
    {
        openMenuAction.action.Disable();
        openMenuAction.action.performed -= ToggleMenu;
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        // TODO: For polish the menu can be made to open a bit lower
        pauseMenu.transform.position = cameraTransform.position;
        pauseMenu.transform.position += cameraTransform.forward * 2f;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected:
                openMenuAction.action.Disable();
                openMenuAction.action.performed -= ToggleMenu;
                break;
            case InputDeviceChange.Reconnected:
                openMenuAction.action.Enable();
                openMenuAction.action.performed += ToggleMenu;
                break;
        }
    }
    
    private void Update()
    {
        Time.timeScale = pauseMenu.activeSelf ? 0 : 1;
    }
    
}
