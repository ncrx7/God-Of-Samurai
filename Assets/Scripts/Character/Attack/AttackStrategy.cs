using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackStrategy : ScriptableObject
{
    public string AnimationName;
    public int AttackDamage;

    public abstract void Attack(ulong id);
    
}
