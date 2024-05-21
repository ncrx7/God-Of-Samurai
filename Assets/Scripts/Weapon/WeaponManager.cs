using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _slashTransform;


    public void HanleWeaponSlash()
    {
        GameObject slash = Instantiate(_weapon.slashVfx, _slashTransform.position, _slashTransform.rotation, _slashTransform);
        slash.transform.parent = null;
        Destroy(slash, 2f);
    }
}
