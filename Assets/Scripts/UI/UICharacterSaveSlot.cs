using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICharacterSaveSlot : MonoBehaviour
{
    SaveFileDataWriter _saveFileDataWriter;
    [SerializeField] CharacterSaveSlot _characterSaveSlot;
    [SerializeField] TextMeshProUGUI characterNameText;
    [SerializeField] TextMeshProUGUI _timePlayedText;

    private void OnEnable()
    {
        LoadSaveSlots();
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
}
