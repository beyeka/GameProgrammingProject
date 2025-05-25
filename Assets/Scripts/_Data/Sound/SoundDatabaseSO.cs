using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(SoundDatabaseSO), menuName = "Data/SoundDatabase")]
public class SoundDatabaseSO : ScriptableObject
{
    public List<SoundData> sounds;

    public AudioClip GetClip(SFXKeys sfxKeys)
    {
        foreach (var soundData in sounds)
        {
            if (soundData.sfxKey == sfxKeys)
                return soundData.audioClip;
        }

        return null;
    }
}