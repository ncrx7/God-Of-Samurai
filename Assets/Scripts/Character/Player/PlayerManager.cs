using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    [SerializeField] PlayerStatsManager _playerStatsManager;
    //public PlayerAnimatorManager playerAnimatorManager;
    //[SerializeField] PlayerLocomotionManager _playerLocomotionManager;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if(!IsOwner)
        {
            return;
        }

        //_playerLocomotionManager.HandleAllMovement();
        _playerStatsManager.RegenarateStamina();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        PlayerCamera.Instance.HandleCameraActions();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if(IsOwner)
        {
            PlayerCamera.Instance.playerManager = this;
            PlayerInputManager.Instance.playerManager = this;
            characterNetworkManager.currentStamina.OnValueChanged += PlayerUIManager.Instance.SetNewStaminaValue;
            characterNetworkManager.currentStamina.OnValueChanged += _playerStatsManager.ResetStaminaRegenTimer;

            characterNetworkManager.maxStamina.Value = _playerStatsManager.CalculateStaminaBasedOnEnduranceLevel(characterNetworkManager.endurance.Value);
            characterNetworkManager.currentStamina.Value = _playerStatsManager.CalculateStaminaBasedOnEnduranceLevel(characterNetworkManager.endurance.Value);
            PlayerUIManager.Instance.SetMaxStaminaValue(characterNetworkManager.maxStamina.Value);
        }
    }
}
