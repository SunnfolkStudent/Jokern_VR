using UnityEngine;
using System.Collections;

public class FadeMaster : MonoBehaviour
{
    public Animator animator;
    
    //TEMPORARY
    public bool isPlayerCaught;
    
    void Update()
    {
        if (isPlayerCaught)
        {
            FadeWhenCaught();
        }
    }
    
    //POV: Joker'n gets you
    public void FadeWhenCaught()
    {
        animator.SetTrigger("FadeOut");
        StartCoroutine(nameof(WaitUntil));
    }
    
    private void Revive()
    {
        isPlayerCaught = false;
        animator.ResetTrigger("FadeOut");
        animator.SetTrigger("FadeIn");
    }

    private IEnumerator WaitUntil()
    {
        yield return new WaitForSeconds(2f);
        Revive();
    }
    
}
