using UnityEngine;

// Class to handle the camera rotation using gyroscope.
public class GyroManager : MonoBehaviour {
    
    private Gyroscope _gyroscope;
    
    private const float ROTATION_SPEED = 1.5f;
    private const float ZOOM_SPEED = 0.3f;

    [SerializeField] private new Camera camera;

    // If the phone supports the gyro, enable it on Awake.
    private void Awake() {
        if (SystemInfo.supportsGyroscope) {
            _gyroscope = Input.gyro;
            _gyroscope.enabled = true;
        }
    }

    // Update the camera position on every frame.
    private void Update() {
        CameraRotation();
        CameraZoom();
    }

    // With the phone being held in portrait mode, the Y is left and right and X is up and down.
    private void CameraRotation() {
        var rotateHorizontal = _gyroscope.rotationRateUnbiased.y;   // Get rotation measured by the gyro.
        transform.Rotate(Vector3.up, rotateHorizontal * ROTATION_SPEED);    // Rotate the camera in Y.
    }

    // If the user uses 2 fingers in a zoom gesture, make the camera zoom in or out.
    // Source: https://stackoverflow.com/questions/59030399/zooming-in-unity-mobile
    private void CameraZoom() {
        
        // If 2 fingers.
        if (Input.touchCount == 2) {
            // Touch struct of the 2 touches.
            var touchZero = Input.GetTouch(0);  
            var touchOne = Input.GetTouch(1);

            // Get the previous touch position by subtracting the current with delta.
            var touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            var touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Get the distance between the 2 fingers now and then.
            var prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            var currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            // Difference between the 2 lengths, before and after pinch.
            var pinchDelta = currentMagnitude - prevMagnitude;

            // Add the pinch value times the zoom to the camera's field of view. Then clamp the values of
            // field of view within max zoom 60 and min zoom 20.
            camera.fieldOfView += pinchDelta * ZOOM_SPEED;
            camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 20, 60);
        }
    }
}
