using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class SceneTransitionManager : SingletonMonobehavior<SceneTransitionManager>
{
    private bool isLoading = false;

    [SerializeField] private GameObject _loaderImage = null;

    [SerializeField] private Image _progressBar = null;

    [SerializeField] private int _loadingDelay = 1000;

    public SceneName startingSceneName;

    private IEnumerator Start()
    {

        // Start the first scene loading and wait for it to finish.
        yield return StartCoroutine(LoadSceneAndSetActive(startingSceneName.ToString()));
        // If this event has any subscribers, call it.
        EventHandler.CallAfterSceneLoadEvent();

    }

    public void LoadAndSwitchScenes(string sceneName)
    {
        EventHandler.CallBeforeSceneUnloadEvent();
        // // Unload the current active scene.
        // yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        // Load New Scene
        LoadNewScene(sceneName);

    }

    public async void LoadNewScene(string sceneName)
    {
        AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        scene.allowSceneActivation = false;

        _loaderImage.SetActive(true);
        do
        {
            await Task.Delay(100);
            // _progressBar.fillAmount = scene.progress;

        } while (
            (scene.progress < 0.9f || unloadScene.progress < 0.9f));

        await Task.Delay(_loadingDelay);
        scene.allowSceneActivation = true;
        // Set the newly loaded scene as the active scene (this marks it as the one to be unloaded next). \
        _loaderImage.SetActive(false);
        while (!scene.isDone)
        {
            await Task.Delay(100);
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        // FindObjectOfType<CameraController>().SetCameraPosition(/* targetPosition */);

        EventHandler.CallAfterSceneLoadEvent();
        // Call after scene load event

    }


    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // Find the scene that was most recently loaded (the one at the last index of the loaded scenes).
        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        // Set the newly loaded scene as the active scene (this marks it as the one to be unloaded next).
        SceneManager.SetActiveScene(newlyLoadedScene);
    }



}
