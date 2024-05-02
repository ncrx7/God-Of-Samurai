using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterSaveData
{
    [Header("Character")]
    public string CharacterName;
    public float SecondsPlayed;
    public float xPosition;
    public float yPosition;
    public float zPosition;
}
