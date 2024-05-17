using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterCombatManager : MonoBehaviour
{
    [SerializeField] CharacterManager _characterManager;
    [SerializeField] AttackStrategy[] _lightAttacks;
    public string[] clipNames;

    [Header("Attack Stats")] //TODO: MOVE THIS FIELDS TO CHARACTER STATS MANAGER
    private int _attackIndex = 0;
    private float _attackSpeed = 2; //TODO: HAVE TO COME FROM CHARACTERSTATS
    private float _attackLength;
    private float _lastAttackTime;
    //[SerializeField] AttackStrategy[] _heavyAttacks; 

    private void Start()
    {
        _attackLength = 1 / _attackSpeed;

        clipNames = new string[_lightAttacks.Length];
        for (int i = 0; i < _lightAttacks.Length; i++)
        {
            clipNames[i] = _lightAttacks[i].AnimationName;
        }

        SetAttackAnimationSpeed();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            HandleAttack(_characterManager.networkID);
        }
    }
    private void HandleAttack(ulong id)
    {
        Debug.Log("Before id attack performed");
        if (id != _characterManager.networkID)
            return;
        Debug.Log("After id attack performed");
        if (Time.time >= _lastAttackTime + _attackLength)
        {
            Debug.Log("attack inside performed");
            _lightAttacks[_attackIndex].Attack(_characterManager.networkID);
            _attackIndex++;
            _lastAttackTime = Time.time;

            if (_attackIndex >= _lightAttacks.Length)
            {
                _attackIndex = 0;
            }
        }
    }

    private void SetAttackAnimationSpeed()
    {
        EventSystem.SetAnimationSpeedAction(_characterManager.networkID, clipNames, _attackSpeed);
    }
}
