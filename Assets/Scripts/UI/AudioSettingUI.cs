using UnityEngine;
using UnityEngine.UI;

public class AudioSettingUI : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider mousedSlider;

    private void Start()
    {
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        mousedSlider.onValueChanged.AddListener(OnMouseValueChanged);

        // bgmSlider.value = GameManager.Instance.Audio.bgmSoundVolume;
        // sfxSlider.value = GameManager.Instance.Audio.sfxSoundVolume;
        bgmSlider.value = AudioManager.Instance.bgmSoundVolume;
        sfxSlider.value = AudioManager.Instance.sfxSoundVolume;
        mousedSlider.value = PlayerData.Instance.mouseSensitivity;
    }

    private void OnBGMVolumeChanged(float value)
    {
        // GameManager.Instance.Audio.SetBGMSoundVolume(value);
        AudioManager.Instance.SetBGMSoundVolume(value);
    }

    private void OnSFXVolumeChanged(float value)
    {
        // GameManager.Instance.Audio.SetSFXSoundVolume(value);
        AudioManager.Instance.SetSFXSoundVolume(value);
    }
    private void OnMouseValueChanged(float value)
    {
        //TODO:
        PlayerData.Instance.SetMouseSensitivity(value);
    }
}
