using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CharacterManager : NetworkBehaviour
{
    private IState currentState;
    public CharacterController characterController;
    public CharacterLocomotionManager characterLocomotionManager;
    public CharacterNetworkManager characterNetworkManager;
    public NetworkObject networkObject;
    public ulong networkID;

    [Header("Character Flags")]
    public bool isPerformingAction = false;
    public bool isAttacking = false;
    public bool isRunning = false;
    public bool isJumping = false;
    public bool isGrounded = true;
    public bool canRotate = true;
    public bool canMove = true;
    public bool applyRootMotion = false;



    //public Animator animator;
    protected virtual void Awake()
    {
        //EDITOR'DEN SERIALIZE EDILIRSE DAHA OPTIMIZE OLUR
        DontDestroyOnLoad(this);
        characterController = GetComponent<CharacterController>();
        characterNetworkManager = GetComponent<CharacterNetworkManager>();
        //animator = GetComponent<Animator>();
        networkObject = GetComponent<NetworkObject>();
    }

    protected virtual void Start()
    {
        networkID = networkObject.NetworkObjectId;
        ChangeState(new IdleState());
        Debug.Log("network ıd of char: " + networkID);
        
        HeadsUpDisplay.Instance._characterManager = this;
        HeadsUpDisplay.Instance.enabled = true;
    }

    protected virtual void Update()
    {
        EventSystem.UpdateAnimatorParameterAction?.Invoke(networkID, AnimatorValueType.BOOL, "isGrounded", 0, isGrounded);
        currentState.UpdateState(this);

        if (IsOwner) // If local character (our character)
        {
            characterNetworkManager.networkPosition.Value = transform.position;
            characterNetworkManager.networkRotation.Value = transform.rotation;
        }
        else // If network character who came from network (client character)
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

    public void ChangeState(IState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
}
