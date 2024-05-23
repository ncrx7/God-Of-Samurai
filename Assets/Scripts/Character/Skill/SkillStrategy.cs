using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillStrategy : ScriptableObject
{
    public float skillCooldown;
    public abstract void CastSkill(Transform origin);
}
