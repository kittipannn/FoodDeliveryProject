using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    public AudioMixer mixer;
    public string Master_Key = "MusicVol";

    [Header("Slider")]
    public Slider volumeSlider;

    [Header("Toggle")]
    public Toggle soundToggle;
    bool soundMuted;
    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(setVolume);
        volumeSlider.value = SoundManager.soundInstance.LoadVolumeSound(Master_Key);

        soundToggle.onValueChanged.AddListener(SoundManager.soundInstance.OnSoundControl);
        soundMuted = PlayerPrefs.GetInt("muted") == 1;
        soundToggle.isOn = !soundMuted;

    }
    private void setVolume(float sliderValue)
    {
        mixer.SetFloat(Master_Key, Mathf.Log10(sliderValue) * 20);
        SoundManager.soundInstance.SaveVolumeSound(Master_Key, sliderValue);
    }
}
