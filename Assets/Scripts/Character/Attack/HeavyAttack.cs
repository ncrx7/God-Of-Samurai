using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeavyAttackStrategy", menuName = "ScriptableObjects/BasicAttacks/HeavyAttackStrategy")]
public class HeavyAttack : AttackStrategy
{
    public override void Attack(ulong id)
    {
        EventSystem.PlayTargetAnimationAction?.Invoke(id, AnimationName, true, false, false, true);
        //TODO: REDUCE STAMINA
    }
}
