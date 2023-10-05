using UnityEngine;

// Class to handle player spawn.
public class SpawnManager : MonoBehaviour {
    
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject player;

    // Instantiate a player on Awake if spawn point isn't null.
    private void Awake() {
        if (spawnPoint != null) Instantiate(player, spawnPoint.position, spawnPoint.rotation);
    }
}
