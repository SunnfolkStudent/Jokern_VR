using UnityEngine;

public class clownLogic1 : MonoBehaviour
{
    private Animator animator;

    public string voiceLine;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.Play("Idle");
        SubtitleSystem.PlayVoiceLine(voiceLine);
        print("talking");
    }
    
    public void PlayVoiceLine()
    {
    }
}
