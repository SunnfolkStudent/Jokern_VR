using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPoint : MonoBehaviour
{
    public Transform[] spawnLocations;
    public GameObject player;
    

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        int spawn = Random.Range(0, spawnLocations.Length);
        Transform spawnLocation = spawnLocations[spawn];
        Instantiate(player, spawnLocation.position, spawnLocation.rotation);
    }
}
