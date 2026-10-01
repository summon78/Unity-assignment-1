using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using Mono.Cecil.Cil;

public class SceneLoadingManager : MonoBehaviour
{
    [Header("UI Reference")]
    public Slider progressBar;
    public TextMeshProUGUI loadingText;
    public GameObject loadingPanel;

    [Header("Settings")]
    public string sceneToLoad = "Level";
    public float minimumDisplayTime = 1.5f;

    private bool isLoading = false;
    private static SceneLoadingManager instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (loadingPanel != null)
            loadingPanel.SetActive(false);


    }

    public static async Awaitable LoadScene(string sceneName)
    {
        if (instance == null)
        {
            Debug.LogError("LoadingSceneManager  is not found.");
            return;
        }

        await instance.InternalLoadScene(sceneName);

    }

    private async Awaitable InternalLoadScene(string sceneName)
    {
        if (isLoading) return;
        isLoading = true;

        loadingPanel.SetActive(true);
        loadingText.text = "Loading...0%";
        progressBar.value = 0f;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float startTime = Time.time;

        while (operation.progress < 0.9f)
        {
            float normalizedProgress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = normalizedProgress;
            loadingText.text = $"Loading...{normalizedProgress * 100:F0}%";

            await Awaitable.NextFrameAsync();
        }

        float elapsedTime = Time.time - startTime;
        if (elapsedTime < minimumDisplayTime)
        {
            float remainingTime = minimumDisplayTime - elapsedTime;
            loadingText.text = "Loading Complete!";
            await Awaitable.WaitForSecondsAsync(remainingTime);

        }

        loadingText.text = "Starting Level...";
        await Awaitable.WaitForSecondsAsync(0.3f);

        loadingPanel.SetActive(false);

        operation.allowSceneActivation = true;

        isLoading = false;
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }

    }

}

