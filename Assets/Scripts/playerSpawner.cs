using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnPoint; // assign PlayerSpawnPoint
    public GameObject player;

    void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (player != null && spawnPoint != null)
            player.transform.position = spawnPoint.position;
    }
}
