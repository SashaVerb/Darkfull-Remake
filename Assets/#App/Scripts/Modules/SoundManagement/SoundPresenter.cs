using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundPresenter : IDisposable
{
    public float SoundVolume
    {
        get => GetMixerVolume(SoundVolumeKey);
        set => SetMixerVolume(SoundVolumeKey, value);
    }
    
    public float MusicVolume
    {
        get => GetMixerVolume(MusicVolumeKey);
        set => SetMixerVolume(MusicVolumeKey, value);
    }
    
    public float MasterVolume
    {
        get => GetMixerVolume(MasterVolumeKey);
        set => SetMixerVolume(MasterVolumeKey, value);
    }
    
    private readonly AudioMixer _mixer;

    private const string SoundVolumeKey = "SoundVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const string MasterVolumeKey = "MasterVolume";

    public SoundPresenter(AudioMixer mixer)
    {
        _mixer = mixer;
        LoadSettings();
    }

    private void LoadSettings()
    {
        SoundVolume = PlayerPrefs.GetFloat(SoundVolumeKey, 0f);
        MusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 0f);
        MasterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 0f);
    }
    
    public void Save()
    {
        PlayerPrefs.SetFloat(SoundVolumeKey, SoundVolume);
        PlayerPrefs.SetFloat(MusicVolumeKey, MusicVolume);
        PlayerPrefs.SetFloat(MasterVolumeKey, MasterVolume);
        PlayerPrefs.Save();
    }

    public void SetSoundVolume(float value)
    {
        SoundVolume = value;
    }
    
    public void SetMusicVolume(float value)
    {
        MusicVolume = value;
    }
    
    public void SetMasterVolume(float value)
    {
        MasterVolume = value;
    }
    
    private void SetMixerVolume(string parameter, float value)
    {
        float clamped01Value = Mathf.Lerp(0f, 1f, value);
        if (Mathf.Approximately(clamped01Value, 0f))
        {
            _mixer.SetFloat(parameter, -80f);
        }
        else
        {
            _mixer.SetFloat(parameter, Mathf.Log10(clamped01Value) * 20f);
        }
    }
    
    private float GetMixerVolume(string parameter)
    {
        _mixer.GetFloat(parameter, out float volume);
        return Mathf.Pow(10f, volume / 20f);;
    }

    public void Dispose()
    {
        Save();
    }
}
