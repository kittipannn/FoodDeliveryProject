using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressSlider;
    public Text progressText;

    public void LoadingScene(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }
    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            progressSlider.value = progress;
            progressText.text = Mathf.FloorToInt(progress * 100) + " %";

            yield return null;
        }
    }

}
