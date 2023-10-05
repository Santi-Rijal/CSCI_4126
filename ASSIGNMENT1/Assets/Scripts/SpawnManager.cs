using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject player;

    private void Awake() {
        if (spawnPoint != null) Instantiate(player, spawnPoint.position, spawnPoint.rotation);
    }
}
