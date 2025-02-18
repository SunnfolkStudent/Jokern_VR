using UnityEngine;

public class clownLogic2 : MonoBehaviour {
    private Animator animator;

    public string voiceLine;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        animator.Play("Idle");
    }

    public void ClownFall()
    {
        animator.Play("FallingFromEdge");
        SubtitleSystem.PlayVoiceLine(voiceLine);
    }
}
