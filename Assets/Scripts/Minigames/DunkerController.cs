using System;
using System.Collections;
using UnityEngine;

public class DunkerController : MonoBehaviour
{
    public HingedDoor sign;
    public c4 signC4;
    public Rigidbody ClownRb;
    public clownLogic2 Clown;

    private IEnumerator OnHit()
    {
        Clown.fall = true;
        yield return new WaitForSeconds(1f);
        ClownRb.useGravity = true;
        yield return new WaitForSeconds(0.5f);
        // sign.shouldBeOpen = true;
        signC4.Explode();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Balls"))
        {
            StartCoroutine(OnHit());
        }
    }
}
