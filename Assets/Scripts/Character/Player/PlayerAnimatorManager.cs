using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : CharacterAnimatorManager
{
    PlayerManager _playerManager;
    protected override void Awake()
    {
        base.Awake();
        _playerManager = GetComponent<PlayerManager>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    private void OnAnimatorMove()
    {
        if(_playerManager.applyRootMotion)
        {
            Vector3 velocity = animator.deltaPosition;
            _playerManager.characterController.Move(velocity);
            _playerManager.transform.rotation *= animator.deltaRotation;
        }
    }
}
