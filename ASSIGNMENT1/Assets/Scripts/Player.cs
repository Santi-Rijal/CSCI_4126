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

        _characterController = new CharacterController();
    }

    private void Update() {
        HandleMove();
        LookAround();
    }

    private void HandleMove() {
        if (Accelerometer.current.enabled) {
            var acceleration = Accelerometer.current.acceleration.ReadValue();

            var moveDirection = new Vector3(acceleration.x * (playerSpeed * Time.deltaTime), 0f, -acceleration.z * (playerSpeed * Time.deltaTime));
            var transformedDirection = transform.TransformDirection(moveDirection);

            _characterController.Move(transformedDirection);
        }
    }
    
    private void LookAround() {
        if (Gyroscope.current.enabled) {
            
        }
    }
}
