using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneController : MonoSingleton<SceneController>, ICrossSceneObject, IInitializable
{
    public SceneModel CurrentScene => currentScene;
    public UnityEvent<SceneModel> OnSceneLoad;
    public UnityEvent<SceneModel> OnSceneUnload;
    [SerializeField] private SceneModel mainScene;
    [Dependency, SerializeField] private SceneLoadingViewModel loadingScreen;
    [SerializeField] private SceneModel[] scenes;
    private SceneModel currentScene;
    
    public void Initialize()
    {
        destroyGameObjectOnDuplicate = true;
        base.Init();
        if(destroyed) return;
        this.Inject();
        HandleDontDestroy();
        currentScene = mainScene;
        //LoadScene(mainScene);
    }

    public void NextScene()
    {
        if (currentScene.id + 1 < scenes.Length)
            StartCoroutine(LoadAsynchronously(scenes[currentScene.id +1]));
        else
            StartCoroutine(LoadAsynchronously(mainScene));;
    }

    public void LoadScene(SceneModel scene)
    {
        StartCoroutine(LoadAsynchronously(scene));
    }

    [EditorButton]
    public void RestartScene()
    {
        LoadScene(currentScene);
    }

    
    private IEnumerator LoadAsynchronously(SceneModel scene)
    {
        float timer = 0f;
        float minLoadTime = loadingScreen.MinLoadTime;
        float displayedProgress = 0;
        loadingScreen.UpdateLoadingBar(0);
        DOTween.To(() => displayedProgress, (a) => displayedProgress = a, .9f, minLoadTime).OnUpdate(() =>
        {
            loadingScreen.UpdateLoadingBar(displayedProgress);
        });
        loadingScreen.Show();
        OnSceneUnload?.Invoke(currentScene);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene.sceneField);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            timer += Time.deltaTime;
            if (timer > minLoadTime && progress >= displayedProgress)
            {
                loadingScreen.UpdateLoadingBar(progress);
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
        loadingScreen.Hide();
        currentScene = scene;
        OnSceneLoad?.Invoke(currentScene);
        yield return null;
    }

    public void HandleDontDestroy()
    {
        this.transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }   
}
