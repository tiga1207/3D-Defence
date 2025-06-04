using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingUI;     
    [SerializeField] private Slider loadingSlider;

    [SerializeField] private string loadSceneName;

    public void LoadGameScene()
    {
        // StartCoroutine(LoadSceneAsync("TestsScene")); 
        StartCoroutine(LoadSceneAsync(loadSceneName)); // 실제 씬 이름, 인스펙터에서 입력하기.
    }
    public void LoadGameScene(string _sceneName)
    {
        // StartCoroutine(LoadSceneAsync("TestsScene")); 
        StartCoroutine(LoadSceneAsync(_sceneName)); // 실제 씬 이름, 인스펙터에서 입력하기.
    }
    public void ReLoadThisScene()
    {
        // StartCoroutine(LoadSceneAsync("TestsScene")); 
        string nowSceneName = gameObject.scene.name;
        StartCoroutine(LoadSceneAsync(nowSceneName)); // 실제 씬 이름, 인스펙터에서 입력하기.
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        loadingUI.SetActive(true); // 로딩 UI 보여주기

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingSlider.value = progress;
            yield return null;
        }
        // 마지막 10%는 allowSceneActivation 이후 로드

        float timer = 0f;
        float duration = 2f;
        float startValue = loadingSlider.value;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            loadingSlider.value = Mathf.Lerp(startValue, 1f, timer / duration);
            yield return null;
        }
        loadingSlider.value = 1f;

        yield return new WaitForSeconds(1f);
        asyncLoad.allowSceneActivation = true;
    }
}