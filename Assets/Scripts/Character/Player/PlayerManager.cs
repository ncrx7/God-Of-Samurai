using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    public PlayerAnimatorManager playerAnimatorManager;
    [SerializeField] PlayerLocomotionManager _playerLocomotionManager;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();

        if(!IsOwner)
        {
            return;
        }

        _playerLocomotionManager.HandleAllMovement();
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
        }
    }
}
