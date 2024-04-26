using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void PlayRollSoundFx()
    {
        _audioSource.PlayOneShot(WorldSoundFxManager.Instance.rollSFX);
    }
}
