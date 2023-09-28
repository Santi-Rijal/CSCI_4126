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
        //HandleMove();
        StepsUpdate();
    }

    private void HandleMove() {
        if (Accelerometer.current.enabled) {
            Vector3 acceleration = Accelerometer.current.acceleration.ReadValue();

            Vector3 moveDirection = new(acceleration.x * playerSpeed * Time.deltaTime, 0, -acceleration.z * playerSpeed * Time.deltaTime);
            Vector3 transformedDirection = transform.TransformDirection(moveDirection);

            _characterController.Move(transformedDirection);
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
