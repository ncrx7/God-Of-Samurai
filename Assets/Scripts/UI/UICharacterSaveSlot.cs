using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSaveSlot : MonoBehaviour
{
    SaveFileDataWriter _saveFileDataWriter;
    [SerializeField] CharacterSaveSlot _characterSaveSlot;
    [SerializeField] TextMeshProUGUI characterNameText;
    [SerializeField] TextMeshProUGUI _timePlayedText;
    [SerializeField] Button _deleteButton;

    private void OnEnable()
    {
        LoadSaveSlots();
        _deleteButton.onClick.AddListener(DeleteSaveSlotOnClickButton);
    }

    private void LoadSaveSlots()
    {
        _saveFileDataWriter = new SaveFileDataWriter();
        _saveFileDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;


        _saveFileDataWriter.SaveFileName = WorldSaveGameManager.Instance.DecideCharacterFileName(_characterSaveSlot);
        if (_saveFileDataWriter.CheckToSeeFileExist())
        {
            characterNameText.text = WorldSaveGameManager.Instance.savesDictionary[_characterSaveSlot].CharacterName;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void LoadGameOnClickButton()
    {
        WorldSaveGameManager.Instance.currentCharacterSaveSlotBeingUsed = _characterSaveSlot;
        WorldSaveGameManager.Instance.LoadGame();
    }

    public void DeleteSaveSlotOnClickButton()
    {
        TitleScreenManager.Instance.AttemptToDeleteCharacterSlot(_characterSaveSlot);
    }

    public void SelectCurrentSlot()
    {
        TitleScreenManager.Instance.SelectCharacterSlot(_characterSaveSlot);
    }
}
