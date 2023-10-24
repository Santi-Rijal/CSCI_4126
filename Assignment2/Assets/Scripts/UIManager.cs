using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    
    private static UIManager _singleton;
    
    [SerializeField] private TextMeshProUGUI text;
    
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

    public void ShowInteraction(string interactionName) {
        text.SetText(interactionName);
    }
}
