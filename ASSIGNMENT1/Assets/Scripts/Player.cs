using UnityEngine;

public class Player : MonoBehaviour {
    
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float playerSpeed = 3f;

    private CharacterController _characterController;
    
    private void Awake() {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        HandleMove();
    }

    private void HandleMove() {
        var inputVector = playerInput.GetMovementVectorNormalized();

        var moveDir = new Vector3(-inputVector.x, 0f, -inputVector.y);
        
        _characterController.Move(moveDir * (Time.deltaTime * playerSpeed));
    }
}
