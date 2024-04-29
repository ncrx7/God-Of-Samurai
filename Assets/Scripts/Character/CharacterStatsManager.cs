using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    [SerializeField] CharacterManager _characterManager;

    [Header("Stamina Regeneration")]
    private float _staminaRegenarationTimer = 0;
    private float _staminaTickTimer = 0;
    [SerializeField] float _staminaRegenarationDelay = 2;
    [SerializeField] float _staminaRegenerationAmount = 2;

    public int CalculateStaminaBasedOnEnduranceLevel(int endurance)
    {
        float stamina = 0;

        stamina = endurance * 10;
        return Mathf.RoundToInt(stamina);
    }

    public virtual void RegenarateStamina()
    {
        if (!_characterManager.IsOwner)
        {
            return;
        }

        if (_characterManager.characterNetworkManager.isSprinting.Value)
        {
            return;
        }

        if (_characterManager.isPerformingAction)
        {
            return;
        }

        _staminaRegenarationTimer += Time.deltaTime;

        if (_staminaRegenarationTimer >= _staminaRegenarationDelay)
        {
            if (_characterManager.characterNetworkManager.currentStamina.Value < _characterManager.characterNetworkManager.maxStamina.Value)
            {
                _staminaTickTimer += Time.deltaTime;

                if (_staminaTickTimer >= 0.1)
                {
                    _staminaTickTimer = 0;
                    _characterManager.characterNetworkManager.currentStamina.Value += _staminaRegenerationAmount;
                }
            }
        }
    }
    
    public virtual void ResetStaminaRegenTimer(float oldValue, float newValue)
    {
        if(newValue < oldValue)
        {
            _staminaRegenarationTimer = 0;
        }
    }
}
