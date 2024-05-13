using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    [SerializeField] private PlayerManager _playerManager;
    private float _verticalMovement;
    private float _horizontalMovement;
    private float _moveAmount;


    [Header("Movement Settings")]
    private Vector3 _targetRotationDireciton;
    private Vector3 _moveDirection;
    [SerializeField] private float _runningSpeed = 5;
    [SerializeField] private float _sprintingSpeed = 7.5f;
    [SerializeField] private float _walkingSpeed = 2;
    [SerializeField] private float _rotationSpeed = 15;
    [SerializeField] private int _sprintingStaminaCost = 2;

    [Header("Jump")]
    [SerializeField] float _jumpStaminaCost = 5;
    [SerializeField] private float _jumpHeight = 4f;
    [SerializeField] float _jumpForwardSpeed = 5f;
    [SerializeField] float _freeFallVelocity = 2;
    private Vector3 jumpDirection;


    [Header("Dodge Settings")]
    private Vector3 _rollDirection;
    [SerializeField] float _dodgeStaminaCost = 25;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        EventSystem.MovementLocomotionActionOnGround += HandleGroundedMovement;
        EventSystem.MovementLocomotionActionOnGround += HandleRotation;
        EventSystem.MovementLocomotionActionOnAir += HandleJumpingMovement;
        EventSystem.MovementLocomotionActionOnAir += HandleFreeFallMovement;
        EventSystem.DodgeAction += HandleDodge;
        EventSystem.SprintAction += HandleSprint;
        EventSystem.JumpAction += HandleJump;
    }
    private void OnDisable()
    {
        EventSystem.MovementLocomotionActionOnGround -= HandleGroundedMovement;
        EventSystem.MovementLocomotionActionOnAir -= HandleJumpingMovement;
        EventSystem.MovementLocomotionActionOnAir -= HandleFreeFallMovement;
        EventSystem.MovementLocomotionActionOnGround -= HandleRotation;
        EventSystem.DodgeAction -= HandleDodge;
        EventSystem.SprintAction -= HandleSprint;
        EventSystem.JumpAction -= HandleJump;
    }

    protected override void Update()
    {
        base.Update();

        //HandleJumpingMovement();
        //HandleFreeFallMovement();

        if (_playerManager.IsOwner)
        {
            _playerManager.characterNetworkManager.animatorVerticalValue.Value = _verticalMovement;
            _playerManager.characterNetworkManager.animatorHorizontalValue.Value = _horizontalMovement;
            _playerManager.characterNetworkManager.networkMoveAmount.Value = _moveAmount;
        }
        else
        {
            _verticalMovement = _playerManager.characterNetworkManager.animatorVerticalValue.Value;
            _horizontalMovement = _playerManager.characterNetworkManager.animatorHorizontalValue.Value;
            _moveAmount = _playerManager.characterNetworkManager.networkMoveAmount.Value;

            //_playerManager.playerAnimatorManager.UpdateAnimatorMovementParameters(0, _moveAmount);

            if (_playerManager.characterNetworkManager.isSprinting.Value)
            {
                EventSystem.UpdateAnimatorParameterAction?.Invoke(_playerManager.networkID, AnimatorValueType.FLOAT, "Vertical", 2, false);
            }
            else
            {
                EventSystem.UpdateAnimatorParameterAction?.Invoke(_playerManager.networkID, AnimatorValueType.FLOAT, "Horizontal", 0, false);
                EventSystem.UpdateAnimatorParameterAction?.Invoke(_playerManager.networkID, AnimatorValueType.FLOAT, "Vertical", _moveAmount, false);
            }
            //HORIZONTAL WILL USE WHEN LOCKED ON
        }

        UpdateStaminaValueOnSprint();
    }
    public void HandleAllMovement()
    {
        //HandleGroundedMovement();
        //HandleRotation();
    }

    private void GetMovementValues()
    {
        _verticalMovement = PlayerInputManager.Instance.VerticalInput;
        _horizontalMovement = PlayerInputManager.Instance.HorizontalInput;
        _moveAmount = PlayerInputManager.Instance.MoveAmount;
    }

    private void HandleGroundedMovement(ulong id)
    {
        // Debug.Log("movement working");
        //GetMovementValues();
        if (id == _playerManager.networkID)
        {
            if (!_playerManager.canMove)
                return;

            _moveDirection = PlayerCamera.Instance.transform.forward * PlayerInputManager.Instance.VerticalInput;
            _moveDirection += PlayerCamera.Instance.transform.right * PlayerInputManager.Instance.HorizontalInput;
            _moveDirection.Normalize();
            _moveDirection.y = 0;

            if (_playerManager.characterNetworkManager.isSprinting.Value)
            {
                _playerManager.characterController.Move(_moveDirection * _sprintingSpeed * Time.deltaTime);
            }
            else
            {
                if (PlayerInputManager.Instance.MoveAmount <= 0.5)
                {
                    _playerManager.characterController.Move(_moveDirection * _walkingSpeed * Time.deltaTime);
                }
                else if (PlayerInputManager.Instance.MoveAmount > 0.5)
                {
                    _playerManager.characterController.Move(_moveDirection * _runningSpeed * Time.deltaTime);
                }
            }
        }
    }

    public void HandleJumpingMovement(ulong id)
    {
        if (id == _playerManager.networkID)
        {
            if (!_playerManager.isGrounded)
            {
                _playerManager.characterController.Move(jumpDirection * _jumpForwardSpeed * Time.deltaTime);
            }
        }
    }

    public void HandleFreeFallMovement(ulong id)
    {
        if (id == _playerManager.networkID)
        {
            if (!_playerManager.isGrounded)
            {
                Vector3 freeFallDirection;

                freeFallDirection = PlayerCamera.Instance.transform.forward * PlayerInputManager.Instance.VerticalInput;
                freeFallDirection += PlayerCamera.Instance.transform.right * PlayerInputManager.Instance.HorizontalInput;
                freeFallDirection.y = 0;

                _playerManager.characterController.Move(freeFallDirection * _freeFallVelocity * Time.deltaTime);
            }
        }
    }
    private void HandleRotation(ulong id)
    {
        if (id == _playerManager.networkID)
        {
            if (!_playerManager.canRotate)
                return;

            _targetRotationDireciton = Vector3.zero;
            _targetRotationDireciton = PlayerCamera.Instance.CameraObject.transform.forward * PlayerInputManager.Instance.VerticalInput;
            _targetRotationDireciton += PlayerCamera.Instance.CameraObject.transform.right * PlayerInputManager.Instance.HorizontalInput;
            _targetRotationDireciton.Normalize();
            _targetRotationDireciton.y = 0;

            if (_targetRotationDireciton == Vector3.zero)
            {
                _targetRotationDireciton = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(_targetRotationDireciton);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            transform.rotation = rotation;
        }
    }

    private void HandleDodge(ulong id)
    {
        if (id == _playerManager.networkID)
        {

            if (_playerManager.isPerformingAction)
                return;

            if (_playerManager.characterNetworkManager.currentStamina.Value <= 0)
            {
                return;
            }

            if (PlayerInputManager.Instance.MoveAmount > 0)
            {
                _rollDirection = PlayerCamera.Instance.CameraObject.transform.forward * PlayerInputManager.Instance.VerticalInput;
                _rollDirection += PlayerCamera.Instance.CameraObject.transform.right * PlayerInputManager.Instance.HorizontalInput;
                _rollDirection.y = 0;
                _rollDirection.Normalize();

                Quaternion playerRotation = Quaternion.LookRotation(_rollDirection);
                _playerManager.transform.rotation = playerRotation;

                //ROLL ANIMATION
                EventSystem.PlayTargetAnimationAction?.Invoke(_playerManager.networkID, "Roll_Forward_01", true, false, false, true);
            }
            else
            {
                //BACKSTEP ANIMATION
                Vector3 targetPosition = transform.position - (transform.forward * 3f);
                _playerManager.characterController.enabled = false;
                transform.position = targetPosition;
                _playerManager.characterController.enabled = true;
                //_playerManager.characterController.Move(-transform.forward * 1f);
                EventSystem.PlayTargetAnimationAction?.Invoke(_playerManager.networkID, "Back_Step_01", true, false, false, true);
            }

            _playerManager.characterNetworkManager.currentStamina.Value -= _dodgeStaminaCost;
        }
    }

    public void HandleSprint(ulong id)
    {
        if (id == _playerManager.networkID)
        {
            if (_playerManager.isPerformingAction)
            {
                _playerManager.characterNetworkManager.isSprinting.Value = false;
            }

            if (_playerManager.characterNetworkManager.currentStamina.Value <= 0)
            {
                _playerManager.characterNetworkManager.isSprinting.Value = false;
                return;
            }

            if (_moveAmount >= 0)
            {
                _playerManager.characterNetworkManager.isSprinting.Value = true;
            }
            else
            {
                _playerManager.characterNetworkManager.isSprinting.Value = false;
            }
        }
    }

    private void UpdateStaminaValueOnSprint()
    {
        if (_playerManager.characterNetworkManager.isSprinting.Value)
        {
            _playerManager.characterNetworkManager.currentStamina.Value -= _sprintingStaminaCost * Time.deltaTime;
            //Debug.Log("current stamina" + _playerManager.characterNetworkManager.currentStamina.Value);
        }

        if (_playerManager.characterNetworkManager.currentStamina.Value <= 0)
        {
            _playerManager.characterNetworkManager.isSprinting.Value = false;
            return;
        }
    }

    #region JUMP
    public void HandleJump(ulong id)
    {
        Debug.Log("handle jump");
        if (id == _playerManager.networkID)
        {
            Debug.Log("handle jump inside id if state");
            if (_playerManager.isPerformingAction)
                return;

            if (_playerManager.characterNetworkManager.currentStamina.Value <= 0)
                return;

/*             if (_playerManager.isJumping)
                return; */

            if (!_playerManager.isGrounded)
                return;


            EventSystem.PlayTargetAnimationAction?.Invoke(_playerManager.networkID, "JumpingBeginning", true, false, false, true);

            //_playerManager.isJumping = true;

            _playerManager.characterNetworkManager.currentStamina.Value -= _jumpStaminaCost;


            jumpDirection = PlayerCamera.Instance.CameraObject.transform.forward * PlayerInputManager.Instance.VerticalInput;
            jumpDirection += PlayerCamera.Instance.CameraObject.transform.right * PlayerInputManager.Instance.HorizontalInput;
            jumpDirection.y = 0;

            if (jumpDirection != Vector3.zero)
            {
                if (_playerManager.characterNetworkManager.isSprinting.Value)
                {
                    jumpDirection *= 1;
                }
                else if (PlayerInputManager.Instance.MoveAmount > 0.5)
                {
                    jumpDirection *= 0.5f;
                }
                else if (PlayerInputManager.Instance.MoveAmount <= 0.5f)
                {
                    jumpDirection *= 0.25f;
                }
            }
        }
    }

    public void ApplyJumpingVelocity()
    {
        yVelocity.y = Mathf.Sqrt(_jumpHeight * -2 * gravityForce);
        
    }
    #endregion
}
