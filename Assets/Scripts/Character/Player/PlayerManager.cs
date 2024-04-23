using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
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
}
