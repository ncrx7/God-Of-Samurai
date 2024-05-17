using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LightAttackStrategy", menuName = "ScriptableObjects/BasicAttacks/LightAttackStrategy")]
public class LightAttack : AttackStrategy
{
    public override void Attack(ulong id)
    {
        EventSystem.PlayTargetAnimationAction?.Invoke(id, AnimationName, true, false, false, true);
        //TODO: REDUCE STAMINA
    }
}
