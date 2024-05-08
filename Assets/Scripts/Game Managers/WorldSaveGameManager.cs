using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager Instance;

    public PlayerManager playerManager;

    [Header("Current Save")]
    public CharacterSaveData currentSaveData;
    public CharacterSaveSlot currentCharacterSaveSlotBeingUsed;
    private string _fileName;
    public Dictionary<CharacterSaveSlot, CharacterSaveData> savesDictionary = new Dictionary<CharacterSaveSlot, CharacterSaveData>();

    [Header("Character Save Slots")]
    public CharacterSaveData characterSaveSlot01;
    public CharacterSaveData characterSaveSlot02;
    public CharacterSaveData characterSaveSlot03;
    public CharacterSaveData characterSaveSlot04;
    public CharacterSaveData characterSaveSlot05;
    public CharacterSaveData characterSaveSlot06;
    public CharacterSaveData characterSaveSlot07;
    public CharacterSaveData characterSaveSlot08;
    public CharacterSaveData characterSaveSlot09;
    public CharacterSaveData characterSaveSlot10;

    // 01 02 03.....10

    [Header("Data Writer")]
    SaveFileDataWriter _saveFileDataWriter;

    [Header("SAVE/LOAD")]
    [SerializeField] bool saveGame;
    [SerializeField] bool loadGame;

    [Header("World Scene Index")]
    [SerializeField] int worldSceneIndex = 1;

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

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadAllCharacterSaveProfile();
    }

    private void Update()
    {
        if (saveGame)
        {
            saveGame = false;
            SaveGame();
        }

        if (loadGame)
        {
            loadGame = false;
            LoadGame();
        }
    }

    private void PopulateSavesDictionary()
    {
        savesDictionary.Add(CharacterSaveSlot.CharacterSlot_01, characterSaveSlot01);
        savesDictionary.Add(CharacterSaveSlot.CharacterSlot_02, characterSaveSlot02);
        savesDictionary.Add(CharacterSaveSlot.CharacterSlot_03, characterSaveSlot03);
        savesDictionary.Add(CharacterSaveSlot.CharacterSlot_04, characterSaveSlot04);
        savesDictionary.Add(CharacterSaveSlot.CharacterSlot_05, characterSaveSlot05);
        savesDictionary.Add(CharacterSaveSlot.CharacterSlot_06, characterSaveSlot06);
        savesDictionary.Add(CharacterSaveSlot.CharacterSlot_07, characterSaveSlot07);
        savesDictionary.Add(CharacterSaveSlot.CharacterSlot_08, characterSaveSlot08);
        savesDictionary.Add(CharacterSaveSlot.CharacterSlot_09, characterSaveSlot09);
        savesDictionary.Add(CharacterSaveSlot.CharacterSlot_10, characterSaveSlot10);


    }

    public string DecideCharacterFileName(CharacterSaveSlot characterSaveSlot)
    {
        string fileName = "";
        switch (characterSaveSlot)
        {
            case CharacterSaveSlot.CharacterSlot_01:
                fileName = "characterSlot_01";
                break;
            case CharacterSaveSlot.CharacterSlot_02:
                fileName = "characterSlot_02";
                break;
            case CharacterSaveSlot.CharacterSlot_03:
                fileName = "characterSlot_03";
                break;
            case CharacterSaveSlot.CharacterSlot_04:
                fileName = "characterSlot_04";
                break;
            case CharacterSaveSlot.CharacterSlot_05:
                fileName = "characterSlot_05";
                break;
            case CharacterSaveSlot.CharacterSlot_06:
                fileName = "characterSlot_06";
                break;
            case CharacterSaveSlot.CharacterSlot_07:
                fileName = "characterSlot_07";
                break;
            case CharacterSaveSlot.CharacterSlot_08:
                fileName = "characterSlot_08";
                break;
            case CharacterSaveSlot.CharacterSlot_09:
                fileName = "characterSlot_09";
                break;
            case CharacterSaveSlot.CharacterSlot_10:
                fileName = "characterSlot_10";
                break;

        }
        return fileName;
    }

    public void AttemptToCreateNewGame()
    {
        _saveFileDataWriter = new SaveFileDataWriter();

        _saveFileDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_01);

        if (!_saveFileDataWriter.CheckToSeeFileExist())
        {
            currentCharacterSaveSlotBeingUsed = CharacterSaveSlot.CharacterSlot_01;
            currentSaveData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_02);

        if (!_saveFileDataWriter.CheckToSeeFileExist())
        {
            currentCharacterSaveSlotBeingUsed = CharacterSaveSlot.CharacterSlot_02;
            currentSaveData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_03);

        if (!_saveFileDataWriter.CheckToSeeFileExist())
        {
            currentCharacterSaveSlotBeingUsed = CharacterSaveSlot.CharacterSlot_03;
            currentSaveData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_04);

        if (!_saveFileDataWriter.CheckToSeeFileExist())
        {
            currentCharacterSaveSlotBeingUsed = CharacterSaveSlot.CharacterSlot_04;
            currentSaveData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_05);

        if (!_saveFileDataWriter.CheckToSeeFileExist())
        {
            currentCharacterSaveSlotBeingUsed = CharacterSaveSlot.CharacterSlot_05;
            currentSaveData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_06);

        if (!_saveFileDataWriter.CheckToSeeFileExist())
        {
            currentCharacterSaveSlotBeingUsed = CharacterSaveSlot.CharacterSlot_06;
            currentSaveData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_07);

        if (!_saveFileDataWriter.CheckToSeeFileExist())
        {
            currentCharacterSaveSlotBeingUsed = CharacterSaveSlot.CharacterSlot_07;
            currentSaveData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_08);

        if (!_saveFileDataWriter.CheckToSeeFileExist())
        {
            currentCharacterSaveSlotBeingUsed = CharacterSaveSlot.CharacterSlot_08;
            currentSaveData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_09);

        if (!_saveFileDataWriter.CheckToSeeFileExist())
        {
            currentCharacterSaveSlotBeingUsed = CharacterSaveSlot.CharacterSlot_09;
            currentSaveData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_10);

        if (!_saveFileDataWriter.CheckToSeeFileExist())
        {
            currentCharacterSaveSlotBeingUsed = CharacterSaveSlot.CharacterSlot_10;
            currentSaveData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }


        TitleScreenManager.Instance.DisplayNoFreeCharacterSlotsPopUp();
    }

    public void LoadGame()
    {
        _fileName = DecideCharacterFileName(currentCharacterSaveSlotBeingUsed);

        _saveFileDataWriter = new SaveFileDataWriter();
        _saveFileDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        _saveFileDataWriter.SaveFileName = _fileName;
        currentSaveData = _saveFileDataWriter.LoadSaveFile();

        StartCoroutine(LoadWorldScene());
    }

    public void SaveGame()
    {
        _fileName = DecideCharacterFileName(currentCharacterSaveSlotBeingUsed);

        _saveFileDataWriter = new SaveFileDataWriter();
        _saveFileDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        _saveFileDataWriter.SaveFileName = _fileName;

        playerManager.SaveGameDataToCurrentSaveObject(ref currentSaveData);

        _saveFileDataWriter.CreateNewCharacterSaveFile(currentSaveData);
    }

    public void DeleteGame(CharacterSaveSlot characterSaveSlot)
    {
        _fileName = DecideCharacterFileName(characterSaveSlot);

        _saveFileDataWriter = new SaveFileDataWriter();
        _saveFileDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        _saveFileDataWriter.SaveFileName = _fileName;
        _saveFileDataWriter.DeleteSaveFile();
    }

    public void LoadAllCharacterSaveProfile()
    {
        _saveFileDataWriter = new SaveFileDataWriter();
        _saveFileDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_01);
        characterSaveSlot01 = _saveFileDataWriter.LoadSaveFile();

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_02);
        characterSaveSlot02 = _saveFileDataWriter.LoadSaveFile();

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_03);
        characterSaveSlot03 = _saveFileDataWriter.LoadSaveFile();

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_04);
        characterSaveSlot04 = _saveFileDataWriter.LoadSaveFile();

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_05);
        characterSaveSlot05 = _saveFileDataWriter.LoadSaveFile();

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_06);
        characterSaveSlot06 = _saveFileDataWriter.LoadSaveFile();

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_07);
        characterSaveSlot07 = _saveFileDataWriter.LoadSaveFile();

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_08);
        characterSaveSlot08 = _saveFileDataWriter.LoadSaveFile();

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_09);
        characterSaveSlot09 = _saveFileDataWriter.LoadSaveFile();

        _saveFileDataWriter.SaveFileName = DecideCharacterFileName(CharacterSaveSlot.CharacterSlot_10);
        characterSaveSlot10 = _saveFileDataWriter.LoadSaveFile();

        PopulateSavesDictionary();
    }

    public IEnumerator LoadWorldScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

        //IF we want to load character world index from save file, use this
        //AsyncOperation loadOperation = SceneManager.LoadSceneAsync(currentSaveData.sceneIndex);
        playerManager.LoadGameDataFromCurrentSaveObject(ref currentSaveData);
        yield return null;
    }

    public int GetWorldSceneIndex()
    {
        return worldSceneIndex;
    }
}
