using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "LightningAttackStrategy", menuName = "ScriptableObjects/Skills/LightningAttackSkillStrategy")]
public class LightningAttackSkillStrategy : SkillStrategy
{
    public GameObject lightningPrefab;
    public Transform fxTransform;
    public float rangeDistance;
    public float manaCost;
    
    public override void CastSkill(Transform origin)
    {
        Debug.Log("Lightning Attack performed");

        CharacterSkillManager characterSkillManager= origin.gameObject.GetComponent<CharacterSkillManager>();
        EventSystem.PlayTargetAnimationAction?.Invoke(origin.GetComponent<CharacterManager>().networkID, "QLightningFurry", true, false, false, true);

        fxTransform = characterSkillManager.GetLightningFurryAbilityTargetTransform();
        Debug.Log("fx transform name: " + fxTransform.name);

        Vector3 lightningFurryRotationVector = new Vector3(fxTransform.rotation.eulerAngles.x, fxTransform.rotation.eulerAngles.y / 2, fxTransform.rotation.eulerAngles.z);
        Quaternion lightningFurryRotation = Quaternion.Euler(lightningFurryRotationVector.x, lightningFurryRotationVector.y, lightningFurryRotationVector.z);

        GameObject lightningForwardObject = Instantiate(lightningPrefab, fxTransform.position, lightningFurryRotation, fxTransform);
        lightningForwardObject.transform.parent = null;

        //ADJUST BOLT ROTATION SEPARATELY FROM PARENT OBJECT, BECAUSE LIGHTNING VFX ROTATION AND NORMAL OBJECT ROTATION DON'T SYNCHRON
        Transform bolt = lightningForwardObject.transform.Find("bolt");
        bolt.rotation = fxTransform.rotation;
        bolt.parent = null;
        
        Vector3 targetPosition = origin.transform.position + origin.transform.forward * rangeDistance + new Vector3(0, 2f, 0);
        lightningForwardObject.transform.DOMove(targetPosition, 0.5f);
        bolt.DOMove(targetPosition, 1.5f);

        Destroy(lightningForwardObject, 1.5f);
        Destroy(bolt.gameObject, 1.5f);
    }
}
