using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CharacterManager : NetworkBehaviour
{
    public CharacterController characterController;
    public CharacterNetworkManager characterNetworkManager;
    public Animator animator;
    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);
        characterController = GetComponent<CharacterController>();
        characterNetworkManager = GetComponent<CharacterNetworkManager>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if(IsOwner) // If local character (our character)
        {
            characterNetworkManager.networkPosition.Value = transform.position;
            characterNetworkManager.networkRotation.Value = transform.rotation;
        }
        else // If network character who came from network (other character)
        {
            transform.position = Vector3.SmoothDamp(transform.position,
             characterNetworkManager.networkPosition.Value, 
             ref characterNetworkManager.NetworkPositionVelocity, 
             characterNetworkManager.NetworkPositionSmoothTime);

             transform.rotation = Quaternion.Slerp
             (transform.rotation, characterNetworkManager.networkRotation.Value, characterNetworkManager.NetworkRotationSmoothTime);
        }
    }

    protected virtual void LateUpdate()
    {
        
    }
}
