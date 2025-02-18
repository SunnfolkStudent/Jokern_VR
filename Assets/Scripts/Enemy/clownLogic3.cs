using UnityEngine;

public class clownLogic3 : MonoBehaviour {
    private Animator animator;
    private string currentVoiceLine;
    private float time;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.Play("FingerWag");
        currentVoiceLine = "vo_circus_clown_happy_01";
        time = Time.deltaTime + 2;
    }

    void Update()
    {
        if (time <= Time.deltaTime)
        {
            SubtitleSystem.PlayVoiceLine(currentVoiceLine);
            print("mjau");
            time = Time.deltaTime + 2;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ehm"))
        {
            currentVoiceLine = "vo_circus_clown_neutral_01";
        }
        else if (other.CompareTag("uhm"))
        {
            currentVoiceLine = "vo_circus_clown_angry_01";
        }
    }
}