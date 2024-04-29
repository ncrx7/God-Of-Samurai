using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager Instance { get; private set;}

    [SerializeField] Slider _slider;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
}
