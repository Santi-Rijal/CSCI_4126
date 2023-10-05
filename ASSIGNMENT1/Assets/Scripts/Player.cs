using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Android;

public class Player : MonoBehaviour {
    
    [SerializeField] private float playerSpeed = 3f;
    [SerializeField] private TextMeshProUGUI displaySteps;

    private int _steps;
    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private float _gravity = 10f;
    
    private string _text = "Steps: " + 0;
    
    private void Awake() {
        if (SystemInfo.supportsAccelerometer) InputSystem.EnableDevice(Accelerometer.current);
        
        InputSystem.EnableDevice(StepCounter.current);

        _characterController = GetComponent<CharacterController>();
        _steps = 0;
        
        displaySteps.SetText(_text);
    }
    
    private void Start() {
        if (!Permission.HasUserAuthorizedPermission("android.permission.BODY_SENSORS")) {
            Permission.RequestUserPermission("android.permission.BODY_SENSORS");
        }
    }

    private void Update() {
        var isGrounded = IsGrounded();
        
        if (!isGrounded) {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }
        else {
            _moveDirection.y = -1f;
        }
        
        HandleMove();
        StepsUpdate();
    }

    private void HandleMove() {
        if (Accelerometer.current.enabled) {
            var moveHorizontal = Input.acceleration.x;
            var moveVertical = -Input.acceleration.z;

            _moveDirection = new Vector3(moveHorizontal, _moveDirection.y, moveVertical) * (playerSpeed * Time.deltaTime);

            _characterController.Move(_moveDirection);
        }
    }
    
    private bool IsGrounded() {
        return _characterController.isGrounded;
    }

    private void StepsUpdate() {
        bool isNotNullAndIsEnabled = StepCounter.current != null && StepCounter.current.enabled;
        
        if (isNotNullAndIsEnabled) {
            var currentSteps = StepCounter.current.stepCounter.ReadValue();
            
            if (currentSteps > _steps) {
                _steps = currentSteps;
                
                _text = "Steps: " + _steps;
                displaySteps.SetText(_text);
            }
        }
    }
}
