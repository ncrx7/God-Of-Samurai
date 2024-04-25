using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    private float _verticalMovement;
    private float _horizontalMovement;
    private float _moveAmount;

    private Vector3 _moveDirection;
    private Vector3 _targetRotationDireciton;
    [SerializeField] private float _runningSpeed = 5;
    [SerializeField] private float _walkingSpeed = 3;
    [SerializeField] private float _rotationSpeed = 15;
    [SerializeField] private PlayerManager _playerManager;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();

        if (_playerManager.IsOwner)
        {
            _playerManager.characterNetworkManager.animatorVerticalValue.Value = _verticalMovement;
            _playerManager.characterNetworkManager.animatorHorizontalValue.Value = _horizontalMovement;
            _playerManager.characterNetworkManager.networkMoveAmount.Value = _moveAmount;
        }
        else
        {
            _verticalMovement = _playerManager.characterNetworkManager.animatorVerticalValue.Value;
            _horizontalMovement =  _playerManager.characterNetworkManager.animatorHorizontalValue.Value;
            _moveAmount = _playerManager.characterNetworkManager.networkMoveAmount.Value;

            _playerManager.playerAnimatorManager.UpdateAnimatorMovementParameters(0, _moveAmount);

            //HORIZONTAL WILL USE WHEN LOCKED ON
        }
    }
    public void HandleAllMovement()
    {
        HandleGroundedMovement();
        HandleRotation();
    }

    private void GetMovementValues()
    {
        _verticalMovement = PlayerInputManager.Instance.VerticalInput;
        _horizontalMovement = PlayerInputManager.Instance.HorizontalInput;
        _moveAmount = PlayerInputManager.Instance.MoveAmount;
    }

    private void HandleGroundedMovement()
    {
        GetMovementValues();

        _moveDirection = PlayerCamera.Instance.transform.forward * _verticalMovement;
        _moveDirection += PlayerCamera.Instance.transform.right * _horizontalMovement;
        _moveDirection.Normalize();
        _moveDirection.y = 0;

        if (PlayerInputManager.Instance.MoveAmount <= 0.5)
        {
            _playerManager.characterController.Move(_moveDirection * _walkingSpeed * Time.deltaTime);
        }
        else if (PlayerInputManager.Instance.MoveAmount > 0.5)
        {
            _playerManager.characterController.Move(_moveDirection * _runningSpeed * Time.deltaTime);
        }
    }

    private void HandleRotation()
    {
        _targetRotationDireciton = Vector3.zero;
        _targetRotationDireciton = PlayerCamera.Instance.CameraObject.transform.forward * _verticalMovement;
        _targetRotationDireciton += PlayerCamera.Instance.CameraObject.transform.right * _horizontalMovement;
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
