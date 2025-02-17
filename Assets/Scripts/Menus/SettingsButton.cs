using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject settingsPanel;
    
    public void SettingsChangeState()
    {
        if (settingsPanel.activeInHierarchy)
        {
            settingsPanel.SetActive(false);
        }
        else if (!settingsPanel.activeInHierarchy)
        {
            settingsPanel.SetActive(true);
        }
    }
}
