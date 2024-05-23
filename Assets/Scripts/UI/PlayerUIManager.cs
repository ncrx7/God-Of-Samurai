using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager Instance { get; private set; }

    [SerializeField] Slider _slider;
    [SerializeField] Image lightningFurryCooldownImage;
    [SerializeField] Image lightningShieldCooldownImage;
    [SerializeField] Image lastStandOfLightningCooldownImage;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        EventSystem.OnSkillButtonPressed += SetCooldownImages;
    }

    private void OnDisable()
    {
        EventSystem.OnSkillButtonPressed -= SetCooldownImages;
    }

    public void SetNewStaminaValue(float oldValue, float newValue)
    {
        SetStat(Mathf.RoundToInt(newValue));
    }

    public void SetMaxStaminaValue(int maxValue)
    {
        SetMaxStat(maxValue);
    }

    public void SetStat(int newValue)
    {
        _slider.value = newValue;
    }

    public void SetMaxStat(int maxValue)
    {
        _slider.maxValue = maxValue;
        _slider.value = maxValue;
    }

    private void SetCooldownImages(ulong id, int skillIndex)
    {
        // ID CHECK
        switch (skillIndex)
        {
            case 0:
                Debug.Log("set cool down performed");
                lightningFurryCooldownImage.enabled = true;
                StartCoroutine(HandleCooldown(3f, lightningFurryCooldownImage));
                break;
            case 1:
                lightningShieldCooldownImage.enabled= true;
                StartCoroutine(HandleCooldown(6f, lightningShieldCooldownImage));
                break;
        }
    }

    IEnumerator HandleCooldown(float cooldown, Image image)
    {
        float skillCooldown = cooldown;
        do
        {
            float fillAmount = skillCooldown / cooldown;
            image.fillAmount = fillAmount;
            Debug.Log("skillCooldown / cooldown: " + skillCooldown + "/" + cooldown + "=" + fillAmount);
            skillCooldown--;
            Debug.Log("fill amount: " + image.fillAmount);
            yield return new WaitForSeconds(1);
        } while (skillCooldown >= 0);
    }
}
