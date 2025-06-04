using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class SFXController : PooledObject
{
    private AudioSource _audioSource;
    private float _currentCount;

    private void Awake() => Init();

    private void OnEnable()
    {
        _audioSource.volume = AudioManager.Instance.sfxSoundVolume;
        // _audioSource.volume = GameManager.Instance.Audio.sfxSoundVolume;
    }
    private void Init()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // 한 프레임이 갱신될때까지의 시간 = DeltaTime
        _currentCount -= Time.deltaTime;

        if (_currentCount <= 0)
        {
            // _audioSource.Stop();
            // _audioSource.clip = null;
            ReturnPool();
        }
    }

    public void Play(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        // _audioSource.volume = AudioManager.Instance.sfxSoundVolume;
        _audioSource.volume = AudioManager.Instance.sfxSoundVolume;

        _audioSource.Play();
        _currentCount = clip.length;
    }
}
