using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    void Awake() => base.SingletonInit();

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    


}
