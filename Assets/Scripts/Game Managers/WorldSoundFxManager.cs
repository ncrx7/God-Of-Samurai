using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSoundFxManager : MonoBehaviour
{
    public static WorldSoundFxManager Instance { get; set;}

    [Header("Action Sounds")]
    public AudioClip rollSFX;

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
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
