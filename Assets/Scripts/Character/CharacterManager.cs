using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CharacterManager : NetworkBehaviour
{
    public CharacterController characterController;
    CharacterNetworkManager _characterNetworkManager;
    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);
        characterController = GetComponent<CharacterController>();
        _characterNetworkManager = GetComponent<CharacterNetworkManager>();
    }

    protected virtual void Update()
    {
        if(IsOwner) // If local character (our character)
        {
            _characterNetworkManager.networkPosition.Value = transform.position;
            _characterNetworkManager.networkRotation.Value = transform.rotation;
        }
        else // If network character who came from network (other character)
        {
            transform.position = Vector3.SmoothDamp(transform.position,
             _characterNetworkManager.networkPosition.Value, 
             ref _characterNetworkManager.NetworkPositionVelocity, 
             _characterNetworkManager.NetworkPositionSmoothTime);

             transform.rotation = Quaternion.Slerp
             (transform.rotation, _characterNetworkManager.networkRotation.Value, _characterNetworkManager.NetworkRotationSmoothTime);
        }
    }
}
