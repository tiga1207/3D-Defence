using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource bgmSource;
    private ObjectPool _sfxPool;
    [SerializeField] private SFXController _sfxPrefab;

    public float bgmSoundVolume = 100f;
    public float sfxSoundVolume = 100f;


    private void Awake()
    {
        base.SingletonInit();
        Init();
    }

    private void Init()
    {
        bgmSource = GetComponent<AudioSource>();
        _sfxPool = new ObjectPool(transform, _sfxPrefab, 20);
    }
    #region BGM
    //BGM Play
    public void PlayBGM(AudioClip _clip, bool _loop = true)
    {
        if (_clip == null || bgmSource.clip == _clip) return;
        bgmSource.clip = _clip;
        bgmSource.loop = _loop;
        bgmSource.volume = bgmSoundVolume;

        bgmSource.Play();
    }

    //Control BGM Sound
    public void SetBGMSoundVolume(float _volume)
    {
        bgmSoundVolume = _volume;
        bgmSource.volume = bgmSoundVolume;
    }

    #endregion


    #region SFX

    public void SetSFXSoundVolume(float _volume)
    {
        sfxSoundVolume = _volume;
    }

    public SFXController GetSFX()
    {
        // 풀에서 꺼내와서 반환
        PooledObject po = _sfxPool.PopPool();
        return po as SFXController;
    }
    #endregion




}
