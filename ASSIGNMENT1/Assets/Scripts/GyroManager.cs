using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroManager : MonoBehaviour {
    
    private Gyroscope _gyroscope;
    private Quaternion _rotation;

    [SerializeField] private GameObject cameraContainer;

    private void Awake() {

        if (SystemInfo.supportsGyroscope) {
            _gyroscope = Input.gyro;
            _gyroscope.enabled = true;
        }
    }

    private void Start() {
        if (cameraContainer != null) {
            cameraContainer.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            _rotation = new Quaternion(0, 0, 1, 0);
        }
    }

    private void Update() {
        cameraContainer.transform.rotation = _gyroscope.attitude;
        cameraContainer.transform.Rotate(0f, 0f, 200f, Space.Self);
        cameraContainer.transform.Rotate(90f, 200f, 0f, Space.World);

        transform.rotation = Quaternion.Slerp(transform.rotation, cameraContainer.transform.rotation, 0.1f);
        
        // Vector3 eulerAngles = _gyroscope.attitude.eulerAngles;
        //
        // if (eulerAngles.y >= 80 && eulerAngles.y <= 280) {
        //     Quaternion currentRotation = _gyroscope.attitude * _rotation;
        //     currentRotation.eulerAngles = new Vector3(0, currentRotation.eulerAngles.y, 0); 
        //     
        //     transform.localRotation = currentRotation;
        // }
    }
}
