using UnityEngine;
using UnityEngine.UI;

public class AudioSettingUI : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

        bgmSlider.value = AudioManager.Instance.bgmSoundVolume;
        sfxSlider.value = AudioManager.Instance.sfxSoundVolume;
    }

    private void OnBGMVolumeChanged(float value)
    {
        AudioManager.Instance.SetBGMSoundVolume(value);
    }

    private void OnSFXVolumeChanged(float value)
    {
        AudioManager.Instance.SetSFXSoundVolume(value);
    }
}
