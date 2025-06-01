using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingUI;      // 로딩 UI 패널 (SetActive용)
    [SerializeField] private Image loadingBarFill;      // Fill 타입 이미지

    public void LoadGameScene()
    {
        StartCoroutine(LoadSceneAsync("TestsScene")); // 실제 씬 이름
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        loadingUI.SetActive(true); // 로딩 UI 보여주기

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingBarFill.fillAmount = progress;
            yield return null;
        }

        // 마지막 10%는 allowSceneActivation 이후 로드
        loadingBarFill.fillAmount = 1f;

        yield return new WaitForSeconds(0.5f); // UX 딜레이 (선택)
        asyncLoad.allowSceneActivation = true;
    }
}