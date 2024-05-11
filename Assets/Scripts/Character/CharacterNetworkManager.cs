using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CharacterNetworkManager : NetworkBehaviour
{
    private CharacterManager _characterManager;
    private CharacterAnimatorManager _characterAnimatorManager;


    [Header("Position")]
    public NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<Quaternion> networkRotation = new NetworkVariable<Quaternion>(Quaternion.identity, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public Vector3 NetworkPositionVelocity;
    public float NetworkPositionSmoothTime = 0.1f;
    public float NetworkRotationSmoothTime = 0.1f;

    [Header("Animator")]
    public NetworkVariable<float> animatorHorizontalValue = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> animatorVerticalValue = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> networkMoveAmount = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [Header("Flags")]
    public NetworkVariable<bool> isSprinting = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [Header("Stats")]
    public NetworkVariable<int> endurance = new NetworkVariable<int>(10, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> currentStamina = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner) ;
    public NetworkVariable<int> maxStamina = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner) ;

    protected virtual void Awake()
    {
        _characterManager = GetComponent<CharacterManager>();
        _characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
    }

    [ServerRpc]
    public void NotiftTheServerOfActionAnimationServerRPC(ulong clientID, string animationID, bool applyRootMotion)
    {
        if(IsServer)
        {
            PlayActionAnimationForAllClientsClientRPC(clientID, animationID, applyRootMotion);
        }
    }

    [ClientRpc]
    public void PlayActionAnimationForAllClientsClientRPC(ulong clientID, string animationID, bool applyRootMotion)
    {
        if(clientID != NetworkManager.Singleton.LocalClientId)
        {
            PerformActionAnimationFromServer(animationID, applyRootMotion);
        }
    }

    private void PerformActionAnimationFromServer(string animationID, bool applyRootMotion)
    {
        _characterManager.applyRootMotion = applyRootMotion;
        _characterAnimatorManager.animator.CrossFade(animationID, 0.2f);
    }
}
