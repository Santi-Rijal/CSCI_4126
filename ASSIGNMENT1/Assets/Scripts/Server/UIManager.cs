using Riptide;
using UnityEngine;

public class UIManager : MonoBehaviour {
    
    private static UIManager _singleton;
    
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject player;
    
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

    [SerializeField] private GameObject connectUI;

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

    public void SendName() {
        Message message = Message.Create(MessageSendMode.Reliable, ClientToServerId.name);
        message.Add("User");
        NetworkManager.Singleton.Client.Send(message);
    }
}
