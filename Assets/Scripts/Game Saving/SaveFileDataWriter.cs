using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveFileDataWriter
{
    public string SaveDataDirectoryPath = "";
    public string SaveFileName = "";

    public bool CheckToSeeFileExist()
    {
        if (File.Exists(Path.Combine(SaveDataDirectoryPath, SaveFileName)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DeleteSaveFile()
    {
        File.Delete(Path.Combine(SaveDataDirectoryPath, SaveFileName));
    }

    public void CreateNewCharacterSaveFile(CharacterSaveData characterSaveData)
    {
        string savePath = Path.Combine(SaveDataDirectoryPath, SaveFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            Debug.Log("Creating Save File At: " + savePath);

            string dataToStore = JsonUtility.ToJson(characterSaveData, true);

            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                using (StreamWriter fileWriter = new StreamWriter(stream))
                {
                    fileWriter.Write(dataToStore);
                }
            }
        }
        catch (System.Exception ex)
        {

            Debug.LogError("GAME NOT SAVED" + savePath + "" + ex.Message);
        }
    }

    public CharacterSaveData LoadSaveFile()
    {
        CharacterSaveData characterSaveData = null;
        string loadPath = Path.Combine(SaveDataDirectoryPath, SaveFileName);

        if (File.Exists(loadPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                {
                    using (StreamReader fileReader = new StreamReader(stream))
                    {
                        dataToLoad = fileReader.ReadToEnd();
                    }
                }

                characterSaveData = JsonUtility.FromJson<CharacterSaveData>(dataToLoad);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        return characterSaveData;
    }
}
