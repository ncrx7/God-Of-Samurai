using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    [SerializeField] PlayerStatsManager _playerStatsManager;
    [SerializeField] PlayerNetworkManagerr _playerNetworkManager;
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
            WorldSaveGameManager.Instance.playerManager = this;


            characterNetworkManager.currentStamina.OnValueChanged += PlayerUIManager.Instance.SetNewStaminaValue;
            characterNetworkManager.currentStamina.OnValueChanged += _playerStatsManager.ResetStaminaRegenTimer;

            characterNetworkManager.maxStamina.Value = _playerStatsManager.CalculateStaminaBasedOnEnduranceLevel(characterNetworkManager.endurance.Value);
            characterNetworkManager.currentStamina.Value = _playerStatsManager.CalculateStaminaBasedOnEnduranceLevel(characterNetworkManager.endurance.Value);
            PlayerUIManager.Instance.SetMaxStaminaValue(characterNetworkManager.maxStamina.Value);
        }
    }
    
    public void SaveGameDataToCurrentSaveObject(ref CharacterSaveData currentSaveObject)
    {
        currentSaveObject.CharacterName = _playerNetworkManager.characterName.Value.ToString();
        currentSaveObject.xPosition = transform.position.x;
        currentSaveObject.yPosition = transform.position.y;
        currentSaveObject.zPosition = transform.position.z;
    }

    public void LoadGameDataFromCurrentSaveObject(ref CharacterSaveData currentSaveObject)
    {
        _playerNetworkManager.characterName.Value = currentSaveObject.CharacterName;
        Vector3 myPosition = new Vector3(currentSaveObject.xPosition, currentSaveObject.yPosition, currentSaveObject.zPosition); 
        transform.position = myPosition;
    }
}
