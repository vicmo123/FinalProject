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
    public static Action<SoundListEnum, bool> Loop = null;

    private void Awake() {
        DontDestroyOnLoad(gameObject);

        soundMap = new Dictionary<SoundListEnum, AudioSource>();

        LoadSounds();

        Play = (name) => { PlaySound(name); };
        Stop = (name) => { StopSound(name); };
        SetVolume = (name, volume) => { SetSoundVolume(name, volume); };
        SetPitch = (name, pitch) => { SetSoundPitch(name, pitch); };
        Pause = (name) => { PauseSound(name); };
        UnPause = (name) => { UnPauseSound(name); };
        Loop = (name, loop) => { LoopSound(name, loop); };

        PlayMusic();
    }

    private void LoadSounds() {
        var enumSoundNames = System.Enum.GetValues(typeof(SoundListEnum)).Cast<SoundListEnum>().ToList();

        foreach (var soundName in enumSoundNames) {
            var soundPrefab = Resources.Load<GameObject>(folderPath + soundName.ToString());
            if (soundPrefab != null)
                soundMap.Add(soundName, InstantiateSound(soundPrefab.GetComponent<AudioSource>()));
            else
                Debug.LogWarning($"Could not load sound {soundName.ToString()} from path {folderPath}");

        }
    }

    private AudioSource InstantiateSound(AudioSource original) {
        var soundInstance = Instantiate(original, transform);
        return soundInstance;
    }

    private void PlaySound(SoundListEnum soundName) {
        if (soundMap.ContainsKey(soundName))
            soundMap[soundName].Play();
    }

    private void StopSound(SoundListEnum soundName) {
        if (soundMap.ContainsKey(soundName))
            soundMap[soundName].Stop();
    }

    private void SetSoundVolume(SoundListEnum soundName, float volume) {
        if (soundMap.ContainsKey(soundName))
            soundMap[soundName].volume = volume;
    }

    private void SetSoundPitch(SoundListEnum soundName, float pitch) {
        if (soundMap.ContainsKey(soundName))
            soundMap[soundName].pitch = pitch;
    }

    private void PauseSound(SoundListEnum soundName) {
        if (soundMap.ContainsKey(soundName)) {
            var audioSource = soundMap[soundName];
            if (audioSource.isPlaying)
                audioSource.Pause();
        }
    }

    private void UnPauseSound(SoundListEnum soundName) {
        if (soundMap.ContainsKey(soundName)) {
            var audioSource = soundMap[soundName];
            if (!audioSource.isPlaying)
                audioSource.UnPause();
        }
    }

    private void LoopSound(SoundListEnum soundName, bool loop) {
        if (soundMap.ContainsKey(soundName)) {
            soundMap[soundName].loop = loop;
        }
    }

    private void OnDisable() {
        Play = null;
        Stop = null;
        SetVolume = null;
        SetPitch = null;
        Pause = null;
        UnPause = null;
    }

    private void PlayMusic() {
        int music = UnityEngine.Random.Range(1, 3);
        if (music == 1) {
            SoundManager.Loop(SoundListEnum.musique1, true);
            SoundManager.SetVolume(SoundListEnum.musique1, .5f);
            SoundManager.Play(SoundListEnum.musique1);
        }
        else if (music == 2) {
            SoundManager.Loop(SoundListEnum.musique2, true);
            SoundManager.SetVolume(SoundListEnum.musique2, .5f);
            SoundManager.Play(SoundListEnum.musique2);
        }
        else if (music == 3) {
            SoundManager.Loop(SoundListEnum.musique3, true);
            SoundManager.SetVolume(SoundListEnum.musique3, .5f);
            SoundManager.Play(SoundListEnum.musique3);
        }
    }
}
