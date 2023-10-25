using UnityEngine;

/**
 * Class to handle connect button click and player spawn.
 */
public class UIManager : MonoBehaviour {
    
    private static UIManager _singleton;    // Singleton of this class.
    
    [SerializeField] private Transform spawnPoint;  // Player spawn point.
    [SerializeField] private GameObject player; // Player
    [SerializeField] private GameObject connectUI;  // Connection UI.
    
    // Create a new singleton if it already doesn't exists else destroy it.
    public static UIManager Singleton {
        get => _singleton;

        private set {
            if (_singleton == null) {
                _singleton = value;
            }
            else if (_singleton != value) {
                Debug.Log($"{nameof(UIManager)} instance already exists, destroying duplicate.");
                Destroy(value);
            }
        }
    }
    
    // Set the singleton.
    private void Awake() {
        _singleton = this;
    }

    // Method to handle connect button click.
    public void ClickedConnect() {
        connectUI.SetActive(false); // Hide connect UI screen.
        
        NetworkManager.Singleton.Connect(); // Try to connect to server.
        
        // Spawn player.
        if (spawnPoint != null) Instantiate(player, spawnPoint.position, spawnPoint.rotation);
    }

    // If failed to connect or disconnected, show thw connect screen UI.
    public void BackToMain() {
        connectUI.SetActive(true);  // Show connect screen.
        
        Destroy(player);    // Destroy the previous player object.
    }
}
