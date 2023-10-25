using TMPro;
using Riptide;
using Riptide.Utils;
using UnityEngine;

/**
 * Class to manage the connection on the server side.
 */
public class NetworkManager : MonoBehaviour {

    public Server Server { get; private set; }

    [SerializeField] private ushort port;   // Port for the server.
    [SerializeField] private ushort maxClientCount; // Num of clients allowed.
    [SerializeField] private TextMeshProUGUI text;  // Text to display message.

    private string _oldMessage = "";    // Previous message ref.

    private void Start() {
        // Initialize riptide logs.
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Server = new Server();  // Create a new server.
        Server.Start(port, maxClientCount); // Start the server on port with max client count.

        Server.MessageReceived += ServerOnMessageReceived;  // Subscribe to message received event.
        Server.ClientDisconnected += ServerOnClientDisconnected;    // Subscribe to client disconnect event.
        Server.ClientConnected += ServerOnClientConnected;  // Subscribe to client connect event.
    }

    // A subscription method for client connected event.
    private void ServerOnClientConnected(object sender, ServerConnectedEventArgs e) {
        text.SetText("Connected");
    }

    // A subscription method for client disconnected event.
    private void ServerOnClientDisconnected(object sender, ServerDisconnectedEventArgs e) {
        text.SetText("Disconnected");
    }

    // A subscription method for message received event.
    private void ServerOnMessageReceived(object sender, MessageReceivedEventArgs e) {
        var interactionName = e.Message.GetString();    // Get message as a string.

        // If the message is new type of interaction.
        if (!interactionName.Equals(_oldMessage)) {
            text.SetText(interactionName);  // Show on Screen.
            _oldMessage = interactionName;  // Set a ref.
            
            print(interactionName);
        }
    }
    
    private void FixedUpdate() {
        Server.Update();    // Call the server's update method at a fixed update.
    }

    private void OnApplicationQuit() {
        Server.Stop();  // Stop the server on application quit.
    }
}
