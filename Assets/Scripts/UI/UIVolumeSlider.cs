using UnityEngine;
using UnityEngine.UI;

public class UIVolumeSlider : MonoBehaviour
{
    public FMODController.VolumeSlider volumeSlider;
    
    Slider sliderComponent;

    void Awake() {
        sliderComponent = GetComponent<Slider>();
    }

    void Update() {
        float volume = sliderComponent.value;
        if (volumeSlider != FMODController.VolumeSlider.None) {
            FMODController.SetVolume(volumeSlider, volume);
        }
    }
}
