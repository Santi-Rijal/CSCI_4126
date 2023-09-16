using UnityEngine;

public class PlayerInput : MonoBehaviour {

    private PlayerInputActions _playerInputActions;

    private void Awake() {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }
    
    public Vector2 GetMovementVectorNormalized() {
        var inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }
}
