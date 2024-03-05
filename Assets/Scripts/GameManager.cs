using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Management")]
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject SettingsMenu;
    [SerializeField] private GameObject QuitMenu;

    [SerializeField] private GameObject tablet;
    [SerializeField] private GameObject tabletUI;

    
    

    void Start()
    {
        

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        

        

        OpenTablet();

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (!PausePanel.activeSelf)
                Pause();
            else Resume();
        }

        
    }

    private void OpenTablet()
    {
        if (Input.GetKeyDown(KeyCode.T) && tablet.activeSelf == false )
        {
            tabletUI.SetActive(!tabletUI.activeSelf);
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Pause()
    {
        
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Settings()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void BackToPauseMenu()
    {
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }


    public void Quit()
    {
        PauseMenu.SetActive(false);
        QuitMenu.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        //load main menu scene
    }

    public void QuitToDesktop()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}