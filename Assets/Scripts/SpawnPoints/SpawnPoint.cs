using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

//NB: This is to be used for scenes WITH randomized path

public class SpawnPoint : MonoBehaviour
{
    public GameObject player;
    private bool doTheThing = true;
    [SerializeField]private FadeMaster fade;
    
    private void Update()
    {
        if (fade.isPlayerCaught && doTheThing)
        {
            StartCoroutine(nameof(RespawnPlayer));
        }
    }

    private void FixedUpdate()
    {
        if (doTheThing == false && fade.isPlayerCaught)
        {
            StartCoroutine(nameof(ResetBoolean));
        }
    }

    private void SpawnPlayer()
    {
        doTheThing = false;
        var spawnPoints = UnityEngine.Object.FindObjectsByType<PlayerSpawnRandom>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        int spawn = Random.Range(0, spawnPoints.Length);
        var spawnLocation = spawnPoints[spawn];
        player.transform.position = spawnLocation.transform.position;
    }
    
    IEnumerator ResetBoolean()
    {
        yield return new WaitForSeconds(1.5f);
        doTheThing = true;
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(1f);
        SpawnPlayer();
    }
}
