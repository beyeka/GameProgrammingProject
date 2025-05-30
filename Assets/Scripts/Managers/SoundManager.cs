// Central sound manager handling music and SFX playback with pooling and toggles. Supports main menu, gameplay, and boss themes.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private int audioSourcePoolSize = 50;

    [SerializeField] private AudioSource gameplayMusicAudioSource;
    [SerializeField] private AudioSource menuMusicAudioSource;
    [SerializeField] private AudioSource bossMusicAudioSource;

    private List<AudioSource> _audioSources;

    public SoundDatabaseSO soundDatabase;

    private int _maxSize = 200;

    public bool isSfxOn;
    public bool isMusicOn;

    public float defaultMusicVolume = 0.2f;

    // Sets up singleton instance.
    private void Awake()
    {
        Instance = this;
    }

    // Creates audio source pool and sets initial sound levels + toggles.
    public void Initialize()
    {
        CreateAudioSources();

        SetSoundLevels();

        isSfxOn = true;
        isMusicOn = true;
    }

    // Fills the audio source pool for reuse.
    public void CreateAudioSources()
    {
        _audioSources = new List<AudioSource>(audioSourcePoolSize);

        for (int i = 0; i < audioSourcePoolSize; i++)
        {
            AddNewAudioSource();
        }
    }

    // Plays a sound effect using a pooled audio source if SFX is enabled.
    public void PlaySound(SFXKeys sfxKey)
    {
        if (!isSfxOn)
            return;

        var clip = soundDatabase.GetClip(sfxKey);
        if (clip == null)
        {
            Debug.LogWarning($"Clip for key {sfxKey} not found!");
            return;
        }

        var source = GetAvailableAudioSource() ?? AddNewAudioSource();

        if (source == null)
            return;

        source.clip = clip;
        source.Play();
    }
    
    // Sets default volume for all music sources.
    private void SetSoundLevels()
    {
        gameplayMusicAudioSource.volume = defaultMusicVolume;
        menuMusicAudioSource.volume = defaultMusicVolume;
        bossMusicAudioSource.volume = defaultMusicVolume;
    }

    // Updates volume of music sources based on isMusicOn toggle.
    public void MusicOnOffChanged()
    {
        gameplayMusicAudioSource.volume = isMusicOn ? defaultMusicVolume : 0;
        menuMusicAudioSource.volume = isMusicOn ? defaultMusicVolume : 0;
        bossMusicAudioSource.volume = isMusicOn ? defaultMusicVolume : 0;
    }

    // Returns the first non-playing audio source from the pool.
    private AudioSource GetAvailableAudioSource()
    {
        foreach (var source in _audioSources)
        {
            if (!source.isPlaying)
                return source;
        }

        return null;
    }

    // Adds and configures a new audio source to the pool, unless max size is reached.
    private AudioSource AddNewAudioSource()
    {
        if (_audioSources.Count >= _maxSize)
            return null;

        var newSource = gameObject.AddComponent<AudioSource>();
        newSource.playOnAwake = false;
        _audioSources.Add(newSource);
        return newSource;
    }

    // Stops all music and plays main menu track.
    public void PlayMainMenuMusic()
    {
        StopAllMusics();

        menuMusicAudioSource.Play();
    }

    // Stops all music and plays gameplay track.
    public void PlayGameplayMusic()
    {
        StopAllMusics();

        gameplayMusicAudioSource.Play();
    }

    // Stops all music and plays boss fight track.DONT HAVE IT YET.
    public void PlayBossFightMusic()
    {
        
        StopAllMusics();

        bossMusicAudioSource.Play();
    }

    // Stops the gameplay music source.
    public void StopGameplayMusic()
    {
        gameplayMusicAudioSource.Stop();
    }

    // Stops all music sources.
    private void StopAllMusics()
    {
        gameplayMusicAudioSource.Stop();
        bossMusicAudioSource.Stop();
        menuMusicAudioSource.Stop();
    }

    // Clears singleton reference on destruction.

    private void OnDestroy()
    {
        Instance = null;
    }
}

// Serializable class that maps a sound effect key to an AudioClip.
[Serializable]
public class SoundData
{
    public SFXKeys sfxKey;
    public AudioClip audioClip;
}

// Enum for identifying different sound effects.
public enum SFXKeys
{
    UI_Button_Click,
    PowerUp_PickUp,
    Riffle_Shoot,
    HeavyShoot,
    EnemyDeath,
    PlayerDeath,
}