using UnityEngine;

public class clownLogic3 : MonoBehaviour {
    private Animator animator;
    private string currentVoiceLine;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.Play("FingerWag");
        currentVoiceLine = "vo_circus_clown_happy_01";
    }

    public bool dings;
    void Update()
    {
        if (dings)
        {
            dings = false;
            PlayVoiceLine();
        }
    }
    
    public void PlayVoiceLine()
    {
        SubtitleSystem.PlayVoiceLineFrom(currentVoiceLine, gameObject);
        FMODController.onVoiceLineEnd.AddListener(Dings);
        print("mjau");
    }

    public void ChangeVoiceLine(string newVoiceLine)
    {
        currentVoiceLine = newVoiceLine;
        PlayVoiceLine();
    }
    
    void Dings()
    {
        print("ferdig");
    }
}