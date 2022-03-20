using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixer mixer;
    public static SoundManager soundInstance;
    private bool muted = false;

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        if (soundInstance == null)
            soundInstance = this;
        else
            Destroy(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;
            sound.source.outputAudioMixerGroup = sound.audioMixer;
        }

        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            LoadValueSoundControl();
        }
        else
        {
            LoadValueSoundControl();
        }
    }
    private void Start()
    {

        AudioListener.pause = muted;
        Play("BGM");
    }

    private void LoadValueSoundControl() 
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void SaveValueSoundControl() 
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }

    public float LoadVolumeSound(string Soundkey) 
    {
        float volumeLevel = PlayerPrefs.GetFloat(Soundkey, 1);
        return volumeLevel;
    }
    public void SaveVolumeSound(string Soundkey, float value)
    {
        PlayerPrefs.SetFloat(Soundkey, value);
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " Not found!");
            return;
        }
        s.source.Play();
    }
    public void OnSoundControl(bool mute)
    {
        if (!mute)
        {
            muted = true;
            AudioListener.pause = true;
            Debug.Log("Mute");
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
            Debug.Log("UnMute");
        }
        SaveValueSoundControl();
    }
}

[System.Serializable]
public class Sound 
{
    public string name;
    public AudioClip clip;
    public AudioMixerGroup audioMixer;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public bool loop;
    public bool playOnAwake;
    [HideInInspector]
    public AudioSource source;
}
