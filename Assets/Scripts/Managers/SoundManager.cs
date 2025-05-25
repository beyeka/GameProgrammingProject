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

    private void Awake()
    {
        Instance = this;
    }

    public void Initialize()
    {
        CreateAudioSources();

        SetSoundLevels();

        isSfxOn = true;
        isMusicOn = true;
    }

    public void CreateAudioSources()
    {
        _audioSources = new List<AudioSource>(audioSourcePoolSize);

        for (int i = 0; i < audioSourcePoolSize; i++)
        {
            AddNewAudioSource();
        }
    }

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

    public void PlayMusic()
    {
        if (!isMusicOn)
            return;
    }

    private void SetSoundLevels()
    {
        gameplayMusicAudioSource.volume = defaultMusicVolume;
        menuMusicAudioSource.volume = defaultMusicVolume;
        bossMusicAudioSource.volume = defaultMusicVolume;
    }

    public void MusicOnOffChanged()
    {
        gameplayMusicAudioSource.volume = isMusicOn ? defaultMusicVolume : 0;
        menuMusicAudioSource.volume = isMusicOn ? defaultMusicVolume : 0;
        bossMusicAudioSource.volume = isMusicOn ? defaultMusicVolume : 0;
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach (var source in _audioSources)
        {
            if (!source.isPlaying)
                return source;
        }

        return null;
    }

    private AudioSource AddNewAudioSource()
    {
        if (_audioSources.Count >= _maxSize)
            return null;

        var newSource = gameObject.AddComponent<AudioSource>();
        newSource.playOnAwake = false;
        _audioSources.Add(newSource);
        return newSource;
    }

    public void PlayMainMenuMusic()
    {
        StopAllMusics();
        
        menuMusicAudioSource.Play();
    }

    public void PlayGameplayMusic()
    {
        StopAllMusics();
        
        gameplayMusicAudioSource.Play();
    }

    public void PlayBossFightMusic()
    {
        // TODO : Call when boss comes
        StopAllMusics();
        
        bossMusicAudioSource.Play();
    }

    public void StopGameplayMusic()
    {
        gameplayMusicAudioSource.Stop();
    }

    private void StopAllMusics()
    {
        gameplayMusicAudioSource.Stop();
        bossMusicAudioSource.Stop();
        menuMusicAudioSource.Stop();
    }
    private void OnDestroy()
    {
        Instance = null;
    }
}

[Serializable]
public class SoundData
{
    public SFXKeys sfxKey;
    public AudioClip audioClip;
}

public enum SFXKeys
{
    UI_Button_Click,
    PowerUp_PickUp,
    Riffle_Shoot,
    HeavyShoot,
}