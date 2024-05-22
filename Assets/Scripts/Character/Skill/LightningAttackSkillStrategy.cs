using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LightningAttackStrategy", menuName = "ScriptableObjects/Skills/LightningAttackSkillStrategy")]
public class LightningAttackSkillStrategy : SkillStrategy
{
    public GameObject lightningPrefab;
    public Transform fxTransform;
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
        Destroy(lightningForwardObject, 3f);
    }
}
