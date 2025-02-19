using UnityEngine;

public class clownLogic1 : MonoBehaviour
{
    private Animator animator;
    public bool talk;

    public string voiceLine;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.Play("Idle");
    }

    public void Talk()
    {
        SubtitleSystem.PlayVoiceLine(voiceLine);
    }

    void Update()
    {
        if (talk)
        {
            talk = false;
            Talk();
        }
    }

}
