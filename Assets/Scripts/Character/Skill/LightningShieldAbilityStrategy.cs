using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "LightningShieldSkillStrategy", menuName = "ScriptableObjects/Skills/LightningShieldSkillStrategy")]
public class LightningShieldAbilityStrategy : SkillStrategy
{
    public GameObject lightningShieldObject;
    public float protectionAmount;
    public float shieldDuration;
    public float manaCost;
    public override void CastSkill(Transform origin)
    {
        GameObject lightningShield = Instantiate(lightningShieldObject, origin.transform.position, Quaternion.identity, origin);

        Transform sphereChild = lightningShield.transform.Find("SphereLightningShield");
        if (sphereChild != null)
        {
            // Child objesinin materyalini al
            Renderer renderer = sphereChild.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material shieldMaterial = renderer.material;

                // Tiling Y değerini sürekli döngü içinde değiştiren animasyonu başlat
                ShieldTilingAnimation(shieldMaterial);
            }
        }
        else
        {
            Debug.LogError("SphereLightningShield child objesi bulunamadı.");
        }

        Destroy(lightningShield, shieldDuration);
    }

    void ShieldTilingAnimation(Material shieldMaterial)
    {
        if (shieldMaterial != null)
        {
            DOTween.Sequence()
                .Append(DOTween.To(() => shieldMaterial.GetTextureScale("_Main_Tex").y,
                                    y => shieldMaterial.SetTextureScale("_Main_Tex", new Vector2(1, y)), 1.85f, 0.6f))
                .Append(DOTween.To(() => shieldMaterial.GetTextureScale("_Main_Tex").y,
                                    y => shieldMaterial.SetTextureScale("_Main_Tex", new Vector2(1, y)), 0.25f, 0.6f))
                .SetLoops(-1, LoopType.Yoyo); 
        }
    }
}
