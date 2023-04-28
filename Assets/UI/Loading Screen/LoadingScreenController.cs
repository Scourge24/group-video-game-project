using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadingScreenController : MonoBehaviour
{
    private static string SceneToLoad;
    // Destroys the current scene, shows the loading screen, then loads the target scene while updating a progress bar.
    public static void LoadSceneWithLoadingScreen(string sceneName)
    {
        if (SceneToLoad != null)
        {
            Debug.LogError("Tried to load a scene while another scene load was already in progress");
            return;
        }
        SceneToLoad = sceneName;
        SceneManager.LoadSceneAsync("LoadingScreen").completed += LoadingScreenReady;
    }

    private static void LoadingScreenReady(AsyncOperation obj)
    {
        LoadingScreenController instance = FindObjectOfType<LoadingScreenController>();
        instance.DoSceneLoadWithLoadingScreen(SceneToLoad);
        SceneToLoad = null;
    }

    /*
    public static void LoadSceneWithLoadingScreen(string sceneName)
    {
        SceneManager.LoadScene("LoadingScreen");
        LoadingScreenController instance = FindObjectOfType<LoadingScreenController>();
        instance.DoSceneLoadWithLoadingScreen(sceneName);
    }
    */

    private VisualElement Root;
    private ProgressBar SceneLoadProgressBar;

    private AsyncOperation SceneLoadOperation;

    private void Start()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;
        SceneLoadProgressBar = Root.Q<ProgressBar>(nameof(SceneLoadProgressBar));
    }

    private void Update()
    {
        if (SceneLoadOperation != null)
        {
            SceneLoadProgressBar.value = SceneLoadOperation.progress * 100;
        }
    }

    private void DoSceneLoadWithLoadingScreen(string sceneName)
    {
        // Load the target scene additively so we can keep the loading screen around until we're done with any sort of
        // fade-in, "press to continue", etc. We'll unload it ourselves later.
        SceneLoadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        SceneLoadOperation.completed += SceneLoadOperation_completed;
    }

    private void SceneLoadOperation_completed(AsyncOperation obj)
    {
        SceneLoadOperation = null;
        SceneManager.UnloadSceneAsync("LoadingScreen");
    }
}
