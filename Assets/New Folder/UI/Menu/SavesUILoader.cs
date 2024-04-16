using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SavesUILoader : MonoBehaviour
{
    public BackgroundFader BackgroundFader;
    public Button ContinueButton;
    public Button LoadButton;

    

    [Header("Settings")]
    public bool FadeOutAtStart;
    public bool LoadAtStart;
    public float FadeSpeed;

    [Header("Events")]
    public UnityEvent OnSavesBeingLoaded;
    public UnityEvent OnSavesLoaded;
    public UnityEvent OnSavesEmpty;





    private void Start()
    {
        if (LoadAtStart)
        {

            if (FadeOutAtStart)
                StartCoroutine(BackgroundFader.StartBackgroundFade(true));
        }
    }



    public void LoadLastSave()
    {
        // do : add load last save logic
       StartCoroutine(FadeAndLoadGame());
      
    }

    public void LoadSelectedSave()
    {
      // do : add load selected save logic
       StartCoroutine(FadeAndLoadGame());
     
    }


    IEnumerator FadeAndLoadGame()
    {
        if (BackgroundFader != null) yield return BackgroundFader.StartBackgroundFade(false, fadeSpeed: FadeSpeed);
        SceneManager.LoadScene("LevelManager");
    }
}
