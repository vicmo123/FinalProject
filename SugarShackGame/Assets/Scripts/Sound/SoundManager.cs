using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;

public class SoundManager : MonoBehaviour
{
    private Dictionary<SoundListEnum, AudioSource> soundMap;
    private const string folderPath = "Sounds/AudioPrefabs/";

    public static Action<SoundListEnum> Play = null;
    public static Action<SoundListEnum> Stop = null;
    public static Action<SoundListEnum, float> SetVolume = null;
    public static Action<SoundListEnum, float> SetPitch = null;
    public static Action<SoundListEnum> Pause = null;
    public static Action<SoundListEnum> UnPause = null;

    private void Awake()
    {
        soundMap = new Dictionary<SoundListEnum, AudioSource>();

        LoadSounds();
        InstantiateSounds();

        Play = (name) => { PlaySound(name); };
        Stop = (name) => { StopSound(name); };
        SetVolume = (name, volume) => { SetSoundVolume(name, volume); };
        SetPitch = (name, pitch) => { SetSoundPitch(name, pitch); };
        Pause = (name) => { PauseSound(name); };
        UnPause = (name) => { UnPauseSound(name); };
    }

    private void LoadSounds()
    {
        var enumSoundNames = System.Enum.GetValues(typeof(SoundListEnum)).Cast<SoundListEnum>().ToList();

        foreach (var soundName in enumSoundNames)
        {
            var soundPrefab = Resources.Load<GameObject>(folderPath + soundName.ToString());
            if (soundPrefab != null)
            {
                var soundInstance = Object.Instantiate(soundPrefab.GetComponent<AudioSource>());
                soundInstance.transform.SetParent(transform);

                soundMap.Add(soundName, soundInstance);
            }
            else
            {
                Debug.LogWarning($"Could not load sound {soundName.ToString()} from path {folderPath}");
            }
        }
    }

    private void InstantiateSounds()
    {
        foreach (var sound in soundMap)
        {
            
        }
    }

    private void PlaySound(SoundListEnum soundName)
    {
        if (soundMap.ContainsKey(soundName))
        {
            soundMap[soundName].Play();
        }
    }

    private void StopSound(SoundListEnum soundName)
    {
        if (soundMap.ContainsKey(soundName))
        {
            soundMap[soundName].Stop();
        }
    }

    private void SetSoundVolume(SoundListEnum soundName, float volume)
    {
        if (soundMap.ContainsKey(soundName))
        {
            soundMap[soundName].volume = volume;
        }
    }

    private void SetSoundPitch(SoundListEnum soundName, float pitch)
    {
        if (soundMap.ContainsKey(soundName))
        {
            soundMap[soundName].pitch = pitch;
        }
    }

    private void PauseSound(SoundListEnum soundName)
    {
        if (soundMap.ContainsKey(soundName))
        {
            var audioSource = soundMap[soundName];
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }

    private void UnPauseSound(SoundListEnum soundName)
    {
        if (soundMap.ContainsKey(soundName))
        {
            var audioSource = soundMap[soundName];
            if (!audioSource.isPlaying)
            {
                audioSource.UnPause();
            }
        }
    }

    private void OnDisable()
    {
        Play = null;
        Stop = null;
        SetVolume = null;
        SetPitch = null;
        Pause = null;
        UnPause = null;
    }
}
