using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapons/Weapon")]
public class Weapon : ScriptableObject
{
   public float AttackDamage; // INCREASE DAMAGE OF BASIC ATTACKS
   public float AbilityPower; // INCREASE DAMAGE OF ABILITES
   public float BlockPower; // BLOCK POWER
   public GameObject slashVfx; // WEAPON SLASH VFX OBJECT
}
