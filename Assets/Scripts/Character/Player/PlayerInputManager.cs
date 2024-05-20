using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }

    public PlayerManager playerManager;
    private NewControls _playerControls;

    [Header("Player Movement fields")]
    [SerializeField] private Vector2 _movementInput;
    public float VerticalInput { get; private set; }
    public float HorizontalInput { get; private set; }
    public float MoveAmount { get; private set; }

    [Header("Camera Movement fields")]
    [SerializeField] private Vector2 _cameraMovementInput;
    public float CameraVerticalInput { get; private set; }
    public float CameraHorizontalInput { get; private set; }

    [Header("Player Actions Input")]
    public bool dodgeInput = false;
    public bool sprintInput = false;
    public bool jumpInput = false;
    public bool attackInput = false;

    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new NewControls();

            //BU EVENTE HANDLE MOVEMENT FONKSİYONU ENTEGRE EDİLİRSE DAHA İYİ OPTİMİZE EDİLİR
            _playerControls.PlayerMovement.Movement.performed += i => _movementInput = i.ReadValue<Vector2>();
            _playerControls.PlayerCamera.Movement.performed += i => _cameraMovementInput = i.ReadValue<Vector2>();

            //_playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
            _playerControls.PlayerActions.Dodge.performed += HandleDodgeInputOnPressed;

            _playerControls.PlayerActions.Attack.performed += HandleAttackInput;

            //HOLDING ACTIONS
            _playerControls.PlayerActions.Sprint.started += HandleSprintingInputOnPressHold;
            _playerControls.PlayerActions.Sprint.canceled += HandleSprintingInputOnReleased;

            _playerControls.PlayerActions.Jump.performed += i => jumpInput = true;
        }

        _playerControls.Enable();
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += OnSceneChanged;
        Instance.enabled = false;
    }

    private void Update()
    {
        HandleMovementInput();
        HandleCameraMovementInput();
        HandleJumpInput();
        //HandleDodgeInput();
        //HandleSprintInput();
    }
    private void OnApplicationFocus(bool focusStatus)
    {
        if (enabled)
        {
            if (focusStatus)
            {
                _playerControls.Enable();
            }
            else
            {
                _playerControls.Disable();
            }
        }
    }
    //DISABLES WHEN MAIN MENU, ACTIVES WHEN ACCES GAME SCENE
    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        // Player controls doesn't work on main menu thanks to here
        if (newScene.buildIndex == WorldSaveGameManager.Instance.GetWorldSceneIndex())
        {
            Instance.enabled = true;
        }
        else
        {
            Instance.enabled = false;
        }
    }

    private void HandleMovementInput()
    {
        VerticalInput = _movementInput.y;
        HorizontalInput = _movementInput.x;

        MoveAmount = Mathf.Clamp01(Mathf.Abs(VerticalInput) + Mathf.Abs(HorizontalInput));
        //Debug.Log("move amount: " + MoveAmount);

        if (MoveAmount <= 0.5 && MoveAmount > 0)
        {
            MoveAmount = 0.5f;
        }
        else if (MoveAmount > 0.5 && MoveAmount <= 1)
        {
            MoveAmount = 1;
        }

        if (playerManager == null)
            return;
        playerManager.isRunning = MoveAmount >= 0.5;
        //EventSystem.MovementLocomotionAction?.Invoke(playerManager.networkID);

        //playerManager.playerAnimatorManager.UpdateAnimatorMovementParameters(0, MoveAmount);
  /*       if (sprintInput)
        {
            EventSystem.UpdateAnimatorParameterAction?.Invoke(playerManager.networkID, AnimatorValueType.FLOAT, "Vertical", 2, false);
        }
        else
        {
            EventSystem.UpdateAnimatorParameterAction?.Invoke(playerManager.networkID, AnimatorValueType.FLOAT, "Horizontal", 0, false);
            EventSystem.UpdateAnimatorParameterAction?.Invoke(playerManager.networkID, AnimatorValueType.FLOAT, "Vertical", MoveAmount, false);
        } */

        //HORIZONTAL WILL USE WHEN LOCKED ON
    }

    private void HandleCameraMovementInput()
    {
        CameraVerticalInput = _cameraMovementInput.y;
        CameraHorizontalInput = _cameraMovementInput.x;
    }

    private void HandleDodgeInput()
    {
        if (dodgeInput)
        {
            dodgeInput = false;
            EventSystem.DodgeAction?.Invoke(playerManager.networkID);
        }
    }

    private void HandleDodgeInputOnPressed(InputAction.CallbackContext callbackContext)
    {
        dodgeInput = true;
        EventSystem.DodgeAction?.Invoke(playerManager.networkID);
        dodgeInput = false;
    }

    private void HandleAttackInput(InputAction.CallbackContext callbackContext)
    {
        if(playerManager == null)
            return;

        attackInput = true;
        EventSystem.HandleBasicAttackAction?.Invoke(playerManager.networkID);
        playerManager.isBasicAttacking = true;
    }

/*     private void HandleSprintInput()
    {
        if (sprintInput)
        {
            EventSystem.SprintAction?.Invoke(playerManager.networkID);
        }
        else
        {
            playerManager.characterNetworkManager.isSprinting.Value = false;
        }
    } */

    private void HandleSprintingInputOnPressHold(InputAction.CallbackContext callbackContext)
    { 
        sprintInput = true;
        playerManager.characterNetworkManager.isSprinting.Value = true;
        EventSystem.SprintAction?.Invoke(playerManager.networkID);
        Debug.Log("pressed sprint");
    }

    private void HandleSprintingInputOnReleased(InputAction.CallbackContext callbackContext)
    {
        sprintInput = false;
        playerManager.characterNetworkManager.isSprinting.Value = false;
        Debug.Log("HandleSprintingInputOnReleased worked");
    }

    private void HandleJumpInput()
    {
        if(jumpInput)
        {
            if(playerManager.characterNetworkManager.currentStamina.Value > 0)
                playerManager.isJumping = true;
            jumpInput = false;
            //EventSystem.JumpAction?.Invoke(playerManager.networkID);
        }
    }
}
