using FMOD;
using UnityEngine;

public class SFXPlayer : MonoBehaviour {
    public JokernVRSound sound;
    [SerializeField] private GameObject alternativeSource;
    [SerializeField] private string voiceLinePath;
    public void PlayDingsFromAlt()
    {
        FMODController.PlaySoundFrom(sound, alternativeSource);
    }

    public void PlayDingsFromPlayer()
    {
        FMODController.PlaySound(sound);
    }

    public void PlaySFXAudio()
    {
        FMODController.PlayVoiceLineAudio(voiceLinePath);
    }
}

