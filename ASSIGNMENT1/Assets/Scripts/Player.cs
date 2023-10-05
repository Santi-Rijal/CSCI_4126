using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Android;

// Class to handle player movement and user steps.
public class Player : MonoBehaviour {
    
    [SerializeField] private float playerSpeed = 3f;
    [SerializeField] private TextMeshProUGUI displaySteps;

    private int _steps; // Current steps.
    private string _text;   // In text format,i.e, Steps: 0.
    
    private CharacterController _characterController;
    private Vector3 _moveDirection; // Character move direction.
    
    private const float GRAVITY = 10f;
    
    // On Awake, enable both accelerometer and step-counter. Then get the character controller from player.
    private void Awake() {
        if (SystemInfo.supportsAccelerometer) InputSystem.EnableDevice(Accelerometer.current);
        InputSystem.EnableDevice(StepCounter.current);

        _characterController = GetComponent<CharacterController>();
        
        _steps = 0;
        _text = "Steps: " + _steps;
        displaySteps.SetText(_text);
    }
    
    // On Start if the user hasn't already given permission to use BODY_SENSORS, ask for it.
    private void Start() {
        if (!Permission.HasUserAuthorizedPermission("android.permission.BODY_SENSORS")) {
            Permission.RequestUserPermission("android.permission.BODY_SENSORS");
        }
    }

    // Update per frame.
    private void Update() {
        var isGrounded = IsGrounded();
        
        // If the player is in the air, bring them down to the floor. Else make sure they are grounded.
        if (!isGrounded) {
            _moveDirection.y -= GRAVITY * Time.deltaTime;
        }
        else {
            _moveDirection.y = -1f;
        }
        
        HandleMove();
        StepsUpdate();
    }

    // Handle player movement using accelerometer if it's enabled.
    private void HandleMove() {
        if (Accelerometer.current.enabled) {
            var moveHorizontal = Input.acceleration.x;
            var moveVertical = -Input.acceleration.z;

            _moveDirection = new Vector3(moveHorizontal, _moveDirection.y, moveVertical) * (playerSpeed * Time.deltaTime);

            _characterController.Move(_moveDirection);
        }
        else {
            InputSystem.EnableDevice(Accelerometer.current);
        }
    }
    
    // Check if player is on the ground or not.
    private bool IsGrounded() {
        return _characterController.isGrounded;
    }

    // Update user steps.
    // Source: https://www.reddit.com/r/Unity3D/comments/wjrg5q/how_to_use_the_pedometerstepcounter_sensor/
    private void StepsUpdate() {
        // Make sure step-counter isn't null and is enabled.
        bool isNotNullAndIsEnabled = StepCounter.current != null && StepCounter.current.enabled;
        
        // If everything is good, continue. Else enable it first.
        if (isNotNullAndIsEnabled) {
            var currentSteps = StepCounter.current.stepCounter.ReadValue();
            
            // If current steps is greater than the steps, set currentsteps as _steps.
            if (currentSteps > _steps) {
                _steps = currentSteps;
                _text = "Steps: " + _steps;
                displaySteps.SetText(_text);
            }
        }
        else {
            InputSystem.EnableDevice(StepCounter.current);
        }
    }
}
