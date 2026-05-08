using UIManagement;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class SoundView : UIPanel
{
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _masterSlider;

    private SoundPresenter _soundPresenter;
    
    [Inject]
    private void Configure(SoundPresenter soundPresenter)
    {
        _soundPresenter = soundPresenter;
    }

    private void Awake()
    {
        _soundSlider.value = _soundPresenter.SoundVolume;
        _musicSlider.value = _soundPresenter.MusicVolume;
        _masterSlider.value = _soundPresenter.MasterVolume;
    }

    private void OnEnable()
    {
        _soundSlider.onValueChanged.AddListener(_soundPresenter.SetSoundVolume);
        _musicSlider.onValueChanged.AddListener(_soundPresenter.SetMusicVolume);
        _masterSlider.onValueChanged.AddListener(_soundPresenter.SetMasterVolume);
    }
    
    private void OnDisable()
    {
        _soundSlider.onValueChanged.RemoveListener(_soundPresenter.SetSoundVolume);
        _musicSlider.onValueChanged.RemoveListener(_soundPresenter.SetMusicVolume);
        _masterSlider.onValueChanged.RemoveListener(_soundPresenter.SetMasterVolume);
    }
}
