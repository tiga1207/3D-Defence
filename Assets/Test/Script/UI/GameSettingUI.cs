using UnityEngine;
using UnityEngine.UI;

public class GameSettingUI : MonoBehaviour
{
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private Button resumeBtn;

    void Awake()
    {
        if (PauseUI != null)
            PauseUI.SetActive(false);

        if (resumeBtn != null)
            resumeBtn.onClick.AddListener(OnClickResume);
    }
    private void OnEnable()
    {
        GameTimeManager.Instance.OnGamePaused += ShowUI;
        GameTimeManager.Instance.OnGameResumed += HideUI;
    }

    private void OnDisable()
    {
        GameTimeManager.Instance.OnGamePaused -= ShowUI;
        GameTimeManager.Instance.OnGameResumed -= HideUI;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameTimeManager.Instance.IsPaused)
                GameTimeManager.Instance.ResumeGame();
            else
                GameTimeManager.Instance.PauseGame();
        }
    }

    public void OnClickResume()
    {
        GameTimeManager.Instance.ResumeGame();
    }
    private void ShowUI()
    {
        if (PauseUI != null)
        {
            PauseUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void HideUI()
    {
        if (PauseUI != null)
        {
            PauseUI?.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}
