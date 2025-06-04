using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;
using System;

public class GameTimeManager : Singleton<GameTimeManager>
{
    private float m_currentTimeScale = 1f;
    private bool m_isPaused = false;

    public bool IsPaused => m_isPaused;

    public event Action OnGamePaused;
    public event Action OnGameResumed;
    // public event Action<float> OnTimeScaleChanged;

    void Awake() => Init();
    private void Init()
    {
        base.SingletonInit();
    }

    public void PauseGame()
    {
        //Pause 상태인 상태에서 중복호출 방지
        if (m_isPaused) return;

        //timeScale을 0으로 만들어서 게임 정지 시키기.
        Time.timeScale = 0f;
        m_isPaused = true;

        //게임 멈춤 이벤트 발생.
        OnGamePaused?.Invoke();
    }

    public void ResumeGame()
    {
        if (!m_isPaused) return;


        //현재 게임 속도
        Time.timeScale = m_currentTimeScale;
        m_isPaused = false;

        //게임 재시작 호출
        OnGameResumed?.Invoke();
    }

    public void GameOnlyStop()
    {
        //Pause 상태인 상태에서 중복호출 방지
        if (m_isPaused) return;

        //timeScale을 0으로 만들어서 게임 정지 시키기.
        Time.timeScale = 0f;
        m_isPaused = true;
    }
    public void GameOnlyStart()
    {
        //Pause 상태인 상태에서 중복호출 방지
        if (!m_isPaused) return;


        Time.timeScale = 1f;
        m_isPaused = false;
        Debug.Log("게임 온니 스타트 실행");
        GameManager.Instance.CursorUnLock();
    }
}

// public void SetTimeScale(float scale)
// {
//     //게임 속도는 최대 5f
//     m_currentTimeScale = Mathf.Clamp(scale, 0f, 5f);

//     //정지상태가 아닐 경우 게임 속도 설정.
//     if (!m_isPaused)
//         Time.timeScale = m_currentTimeScale;

//     //게임 속도 변경 이벤트 호출
//     OnTimeScaleChanged?.Invoke(m_currentTimeScale);
// }


