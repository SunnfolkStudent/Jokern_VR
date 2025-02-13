using System;
using UnityEngine;

public class BlockerManager : MonoBehaviour
{
    private GameObject self;

    private void Start()
    {
        self = this.gameObject;
    }

    public void DeactivateBlocker()
    {
        self.SetActive(false);
    }
}
