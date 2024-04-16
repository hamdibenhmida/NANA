using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SaveGameManager;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMText = TMPro.TMP_Text;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    
    [Serializable]
    public struct GameInfo
    {
        public string SceneName;
        public string Title;
        public string Description;
        public Sprite Background;
    }
    [Header("Game Scene Infos")]
    public GameInfo gameInfos;
    [Header("UI")]
    public TMText Title;
    public TMText Description;
    public Image Background;
    public BackgroundFader FadingBackground;

    [Header("Settings")]
    /// <summary>
    /// Priority of background loading thread. <br><see href="https://docs.unity3d.com/ScriptReference/Application-backgroundLoadingPriority.html"></see></br>
    /// </summary>
    public ThreadPriority LoadPriority = ThreadPriority.High;

    public float FadeSpeed;
    public bool SwitchManually;
    public bool FadeBackground;
    public bool Debugging;
    [Header("Switch Panel")]
    public bool SwitchPanels;
    public float SwitchFadeSpeed;
    public CanvasGroup CurrentPanel;
    public CanvasGroup NewPanel;

    [Header("Events")]
    public UnityEvent<float> OnProgressUpdate;
    public UnityEvent OnLoadingDone;

    private void Start()
    {
        Time.timeScale = 1f;
        Application.backgroundLoadingPriority = LoadPriority;

        string sceneName = LoadSceneName;
        if (!string.IsNullOrEmpty(sceneName))
        {

            if (this.gameInfos.SceneName == sceneName)
            {
                Background.sprite = gameInfos.Background;
                Description.text = gameInfos.Description;
                Title.text = gameInfos.Title;
            }

            StartCoroutine(LoadLevelAsync(sceneName));
        }
    }

    private IEnumerator LoadLevelAsync(string sceneName)
    {
        yield return FadingBackground.StartBackgroundFade(true, fadeSpeed: FadeSpeed);
        yield return new WaitForEndOfFrame();

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        asyncOp.allowSceneActivation = false;

        while (!asyncOp.isDone)
        {
            float progress = asyncOp.progress / 0.9f;
            OnProgressUpdate?.Invoke(progress);

            if (progress >= 1f) break;
            yield return null;
        }

     

        if (SwitchManually)
        {
            OnLoadingDone?.Invoke();

            if (SwitchPanels)
            {
                yield return CanvasGroupFader.StartFade(CurrentPanel, false, SwitchFadeSpeed);
                yield return CanvasGroupFader.StartFade(NewPanel, true, SwitchFadeSpeed);
            }

            yield return new WaitUntil(() => Input.anyKeyDown); // need to change input to new input system

            if (FadeBackground)
            {
                yield return FadingBackground.StartBackgroundFade(false, fadeSpeed: FadeSpeed);
                yield return new WaitForEndOfFrame();
            }
        }

        asyncOp.allowSceneActivation = true;
        yield return null;
    }

    
    
}