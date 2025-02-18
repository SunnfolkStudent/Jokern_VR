using UnityEngine;

public class SFXPlayer : MonoBehaviour {
    public JokernVRSound sound;
    public void PlayDings() {
        FMODController.PlaySoundFrom(sound, gameObject);
    } 
}
