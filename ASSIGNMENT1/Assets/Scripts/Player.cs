using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Gyroscope = UnityEngine.Gyroscope;

public class Player : MonoBehaviour {
    
    [SerializeField] private float playerSpeed = 3f;

    private int _steps;
    private CharacterController _characterController;
    private Text _displaySteps;
    
    private void Awake() {
        if (SystemInfo.supportsAccelerometer) InputSystem.EnableDevice(Accelerometer.current);
        InputSystem.EnableDevice(StepCounter.current);

        _characterController = GetComponent<CharacterController>();
        _steps = 0;
        
    }

    private void Update() {
        HandleMove();
    }

    private void HandleMove() {
        if (Accelerometer.current.enabled) {
            var moveHorizontal = Input.acceleration.x;
            var moveVertical = -Input.acceleration.z;

            var movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * (playerSpeed * Time.deltaTime);

            _characterController.Move(movement);
        }
    }

    private void StepsUpdate() {
        bool isNotNullAndIsEnabled = StepCounter.current != null && StepCounter.current.enabled;

        if (isNotNullAndIsEnabled) {
            var currentSteps = StepCounter.current.stepCounter.ReadValue();
            
            Debug.Log(currentSteps);
            
            if (currentSteps > _steps) {
                _steps = currentSteps;
                _displaySteps.text = "Steps: " + _steps;
            }
        }
    }
}
