using UnityEngine;
using UnityEngine.Events;

public class IntroFade : MonoBehaviour
{
    private Animator _animator;
    public UnityEvent onFadeOutComplete;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        IntroFadeIn();
    }
    public void IntroFadeIn()
    {
        _animator.Play("IntroFade");
    }
    public void DisableIntroCanvas()
    {
        // Here you should be able to call to any function for subtitles
        if (onFadeOutComplete != null) {
            onFadeOutComplete.Invoke();
        }
        
        this.gameObject.SetActive(false);
    }
}
