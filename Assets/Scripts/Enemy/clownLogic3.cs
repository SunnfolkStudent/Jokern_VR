using UnityEngine;

public class clownLogic3 : MonoBehaviour {
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.Play("FingerWag");
    }
    
}
