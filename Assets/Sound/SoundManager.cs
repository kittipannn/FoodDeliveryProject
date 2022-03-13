using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
    public static SoundManager soundInstance;
    private bool muted = false;
    public AudioMixer mixer;

    [Header("SLider")]
    public Slider volumeSlider;
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
        
    }
    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate { setVolume(volumeSlider.value); });
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            LoadValueSoundCOntrol();
        }
        else
        {
            LoadValueSoundCOntrol();
        }
        AudioListener.pause = muted;
        Play("BGM");
    }
    private void LoadValueSoundCOntrol() 
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void SaveValueSoundControl() 
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
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
    public void OnButtonSoundControl()
    {
        if (!muted)
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
    private void setVolume(float sliderValue) 
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20 );
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
