using UnityEngine;

public class GyroManager : MonoBehaviour {
    
    private Gyroscope _gyroscope;
    
    private float _rotationSpeed = 2.0f;
    private float _zoomSpeed = 0.4f;

    [SerializeField] private new Camera camera;

    private void Awake() {
        if (SystemInfo.supportsGyroscope) {
            _gyroscope = Input.gyro;
            _gyroscope.enabled = true;
        }
    }


    void Update() {
        var rotateHorizontal = _gyroscope.rotationRateUnbiased.y;

        transform.Rotate(Vector3.up, rotateHorizontal * _rotationSpeed);

        if (Input.touchCount == 2) {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            var touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            var touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            var prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            var currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            var pinchDelta = currentMagnitude - prevMagnitude;

            camera.fieldOfView += pinchDelta * _zoomSpeed;
            camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 20, 60);
        }
    }
}
