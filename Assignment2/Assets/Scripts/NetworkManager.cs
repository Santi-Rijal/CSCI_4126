using System;
using Riptide;
using Riptide.Utils;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    private static NetworkManager _singleton;

    public static NetworkManager Singleton {
        get => _singleton;

        private set {
            if (_singleton == null) {
                _singleton = value;
            }
            else if (_singleton != value) {
                Debug.Log($"{nameof(NetworkManager)} instance already exists, destroying duplicate.");
                Destroy(value);
            }
        }
    }

    public Server Server { get; private set; }

    [SerializeField] private ushort port;
    [SerializeField] private ushort maxClientCount;

    private void Awake() {
        _singleton = this;
    }

    private void Start() {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Server = new Server();
        Server.Start(port, maxClientCount);
    }

    private void FixedUpdate() {
        Server.Update();
    }

    private void OnApplicationQuit() {
        Server.Stop();
    }
}
