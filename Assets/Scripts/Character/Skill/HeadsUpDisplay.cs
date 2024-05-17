using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadsUpDisplay : MonoBehaviour
{
    public static HeadsUpDisplay Instance { get; set; }
    [SerializeField] public CharacterManager _characterManager;
    [SerializeField] Button[] _skillButtons;

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

        Instance.enabled = false;
    }

    private void OnEnable()
    {
        for (int i = 0; i < _skillButtons.Length; i++)
        {
            int index = i;
            _skillButtons[i].onClick.AddListener(() => HandleSkillButtonPressed(index));
        }
    }

    private void HandleSkillButtonPressed(int skillIndex)
    {
        Debug.Log("skill button pressed");
        EventSystem.OnSkillButtonPressed?.Invoke(_characterManager.networkID, skillIndex); //Input managerda q w e girdisi kullanılarak bu action çağrılabilir
    }

}
