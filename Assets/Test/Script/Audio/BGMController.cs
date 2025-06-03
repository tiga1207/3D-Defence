using UnityEngine;

public class BGMController : MonoBehaviour
{
    //BGM 파일 추가
    [SerializeField] private AudioClip bgmClip;

    void Start()
    {
        if (bgmClip != null)
        {
            AudioManager.Instance.PlayBGM(bgmClip,true);
        }
    }
}