using UnityEngine;

public class UIManager : MonoBehaviour {
    
    private static UIManager _singleton;
    
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject connectUI;
    
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
    
    private void Awake() {
        _singleton = this;
    }

    public void ClickedConnect() {
        connectUI.SetActive(false);
        NetworkManager.Singleton.Connect();
        if (spawnPoint != null) Instantiate(player, spawnPoint.position, spawnPoint.rotation);
    }

    public void BackToMain() {
        connectUI.SetActive(true);
        Destroy(player);
    }
}
