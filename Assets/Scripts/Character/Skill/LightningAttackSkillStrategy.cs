using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LightningAttackStrategy", menuName = "ScriptableObjects/Skills/LightningAttackSkillStrategy")]
public class LightningAttackSkillStrategy : SkillStrategy
{
    public GameObject lightningPrefab;
    public override void CastSkill(Transform origin)
    {
        Debug.Log("Lightning Attack performed");
    }
}
