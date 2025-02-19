using UnityEngine;

public class clownLogic2 : MonoBehaviour {
    private Animator animator;
    public bool fall;

    public string voiceLine;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        animator.Play("SittingOnEdge");
    }

    void Update()
    {
        if (fall)
        {
            fall = false;
            ClownFall();
        }
    }

    public void ClownFall()
    {
        animator.Play("FallingFromEdge");
        SubtitleSystem.PlayVoiceLine(voiceLine);
    }
}
