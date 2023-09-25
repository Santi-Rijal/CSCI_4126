using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class Player : MonoBehaviour {
    
    [SerializeField] private float playerSpeed = 3f;

    private int _steps;
    private CharacterController _characterController;

    private void Awake() {
        if (SystemInfo.supportsAccelerometer) InputSystem.EnableDevice(Accelerometer.current);
        if (SystemInfo.supportsGyroscope) InputSystem.EnableDevice(Gyroscope.current);
        InputSystem.EnableDevice(StepCounter.current);

        _characterController = GetComponent<CharacterController>();
        _steps = 0;
    }

    private void Update() {
        HandleMove();
        //LookAround();
        StepsUpdate();
    }

    private void HandleMove() {
        if (Accelerometer.current.enabled) {
            var acceleration = Accelerometer.current.acceleration.ReadValue();

            var moveDirection = new Vector3(acceleration.x * (playerSpeed * Time.deltaTime), 0f, -acceleration.z * (playerSpeed * Time.deltaTime));
            //var transformedDirection = transform.TransformDirection(moveDirection);

            if (_characterController == null) {
                Debug.LogError("CharacterController not found!");
            }
            else {
                _characterController.Move(moveDirection);
            }
        }
    }
    
    private void LookAround() {
        if (Gyroscope.current.enabled) {
            
        }
    }

    private void StepsUpdate() {
        bool isNotNullAndIsEnabled = StepCounter.current != null && StepCounter.current.enabled;

        if (isNotNullAndIsEnabled) {
            bool stepsUpdated = StepCounter.current.stepCounter.ReadValue() > _steps;
            
            if (stepsUpdated) {
                _steps = StepCounter.current.stepCounter.ReadValue();
                Debug.Log(_steps);
            }
        }
    }
}
