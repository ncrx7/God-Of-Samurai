using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterSaveData
{
    [Header("Character")]
    public string CharacterName = "Character";
    public float SecondsPlayed;
    public float xPosition;
    public float yPosition;
    public float zPosition;
    public int sceneIndex;
}
