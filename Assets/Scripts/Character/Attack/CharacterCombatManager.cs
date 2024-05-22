using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterCombatManager : MonoBehaviour
{
    [SerializeField] CharacterManager _characterManager;
    [SerializeField] AttackStrategy[] _comboAttacks;
    //[SerializeField] AttackStrategy[] _heavyAttacks; 

    [Header("Attack Stats")] //TODO: MOVE THIS FIELDS TO CHARACTER STATS MANAGER
    private int _attackIndex = 0;
    private float _attackSpeed = 2.5f; // HOW MANY ATTACKS PER SECOND
    private float _attackTimeLength; // HOW MANY TIME AN ATTACK TAKE
    private float _lastAttackTime; // LAST ATTACK TIME
    private float _comboResetTime = 2; //IF THE COMBO IS NOT CONTINUED WITHIN THIS TIME, COMBO WILL BE RESET
    private float _comboCooldown = 2f; // THE COMBO COOLDOWN, AFTER MAKE A COMBO


    private bool _canCombo = true;

    private void OnEnable()
    {
        EventSystem.HandleBasicAttackAction += HandleAttack;
    }

    private void OnDisable()
    {
        EventSystem.HandleBasicAttackAction -= HandleAttack;
    }

    private void Awake()
    {
        _attackTimeLength = 1 / _attackSpeed;
    }
 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            HandleAttack(_characterManager.networkID);
        }

        SetAttackAnimationSpeed();
    }
    //TODO: ADJUST HANDLEATTACK FUNCTION ACCORDING TO WEAPON BASED
    //TODO: CAN CHANGE COMBO WHEN JUMP AND FALLING STATE
    private void HandleAttack(ulong id)
    {
        //Debug.Log("Before id attack performed");
        if (id != _characterManager.networkID)
            return;

        if(!_canCombo)
            return;

        if(_characterManager.isPerformingAction)
            return;

        //WHEN COMBO STATE HAVE BEEN ADDED, TIME.TIME CAN REMOVE. CAN ADD COUNTER THAT CAN PROCCES JUST COMBAT STATE
        if(Time.time - _lastAttackTime > _comboResetTime) 
        {
            _attackIndex = 0;
        }
        
        if (Time.time >= _lastAttackTime + _attackTimeLength)
        {
            //Debug.Log("attack inside performed");
            _comboAttacks[_attackIndex].Attack(_characterManager.networkID);
            _attackIndex++;
            _lastAttackTime = Time.time;

            if (_attackIndex >= _comboAttacks.Length)
            {
                StartCoroutine(SetComboCooldown());
                _attackIndex = 0;
            }
        }
    }

    private void SetAttackAnimationSpeed()
    {
        //EventSystem.SetAnimationSpeedAction(_characterManager.networkID, clipNames, _attackSpeed);
        EventSystem.UpdateAnimatorParameterAction?.Invoke(_characterManager.networkID, AnimatorValueType.FLOAT, "basicAttackSpeed", _attackSpeed , false);
    }

    IEnumerator SetComboCooldown()
    {
        _canCombo = false;
        yield return new WaitForSeconds(_comboCooldown);
        _canCombo = true;
    }
}
