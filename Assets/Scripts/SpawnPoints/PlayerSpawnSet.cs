using UnityEngine;

public class PlayerSpawnSet : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject player;

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Instantiate(player, spawnPoint.position, spawnPoint.rotation);
    }
}
