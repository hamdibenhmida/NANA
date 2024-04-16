using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMText = TMPro.TMP_Text;

public class GameManager : Singleton<GameManager>
{
    public enum PanelType { GamePanel, PausePanel, DeadPanel, MainPanel, InventoryPanel, MapPanel }

    [Serializable]
    public struct GraphicReference
    {
        public string Name;
        public Behaviour[] Graphics;
    }

    
    public BackgroundFader BackgroundFade;

    [Header("Settings")]
    [Space]
    [Header("Game Panels")]

    #region Panels
    [Space]
    [Header("Main Panels")]
    // Main Panels
    public CanvasGroup GamePanel;
    public CanvasGroup PausePanel;
    public CanvasGroup DeadPanel;

    [Header("Sub Panels")]
    // Sub Panels
    public CanvasGroup HUDPanel;

    [Header("Feature Panels")]
    // Game Panels
    public CanvasGroup InventoryPanel;
    public Transform FloatingIcons;

    #endregion

    [Space]
    [Header("HUD References")]
    #region UserInterface

    [Space]
    [Header("Reticle/Stamina")]
    // Reticle
    public Image ReticleImage;
    public Image InteractProgress;
    public Slider StaminaSlider;

    [Space]
    [Header("Interact/Control Info")]
    // Interaction
    public InteractInfoPanel InteractInfoPanel;

    [Space]
    [Header("Interact Pointer")]
    // Interact Pointer
    public Image PointerImage;
    public Sprite NormalPointer;
    public Sprite HoverPointer;
    public Vector2 NormalPointerSize;
    public Vector2 HoverPointerSize;

    [Space]
    [Header("Pickup Message")]
    // Item Pickup
    public Transform ItemPickupLayout;
    public GameObject ItemPickup;
    public float PickupMessageTime = 2f;

    [Space]
    [Header("Hint Message")]
    // Hint Message
    public CanvasGroup HintMessageGroup;
    public float HintMessageFadeSpeed = 2f;

    [Space]
    [Header("Health Message")]
    // Health
    public Slider HealthBar;
    public Image Hearthbeat;
    public TMText HealthPercent;

    [Space]
    [Header("Paper Panel")]
    // Paper
    public CanvasGroup PaperPanel;
    public TMText PaperText;
    public float PaperFadeSpeed;

    [Space]
    [Header("Examine Panel")]
    // Examine
    public CanvasGroup ExamineInfoPanel;
    public Transform ExamineHotspots;
    public TMText ExamineText;
    public float ExamineFadeSpeed;

    [Space]
    [Header("Overlays")]
    // Overlays
    public GameObject OverlaysParent;
    #endregion
    [Space]
    [Header("Blur Settings")]
    public bool EnableBlur = true;
    public float BlurRadius = 5f;
    public float BlurDuration = 0.15f;
    [Space]
    public GraphicReference[] GraphicReferencesRaw;
    

    private bool isInputLocked;
    private bool showStaminaSlider;

    private bool isPointerShown;
    private int pointerCullLayers;
    private float defaultBlurRadius;

    
    

    

    public bool IsPaused { get; private set; }
    public bool IsInventoryShown { get; private set; }

    

  

    //    [Header("UI Management")]
    //    [SerializeField] private GameObject PausePanel;
    //    [SerializeField] private GameObject PauseMenu;
    //    [SerializeField] private GameObject SettingsMenu;
    //    [SerializeField] private GameObject QuitMenu;
    //
    //    [SerializeField] private GameObject tablet;
    //    [SerializeField] private GameObject tabletUI;
    //
    //
    //
    //
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ResumeGame()
    {
        IsPaused = false;
        ShowPanel(PanelType.GamePanel);

        // reset panels
        SetPanelInteractable(PanelType.GamePanel);

    }

    /// <summary>
    /// Show the game panel.
    /// </summary>
    /// <param name="panel"></param>
    public void ShowPanel(PanelType panel)
    {
        switch (panel)
        {
            case PanelType.PausePanel:
                SetPanelInteractable(panel);
                GamePanel.alpha = 0;
                PausePanel.alpha = 1;
                DeadPanel.alpha = 0;
                break;
            case PanelType.GamePanel:
                SetPanelInteractable(panel);
                GamePanel.alpha = 1;
                PausePanel.alpha = 0;
                DeadPanel.alpha = 0;
                break;
            case PanelType.DeadPanel:
                SetPanelInteractable(panel);
                GamePanel.alpha = 0;
                PausePanel.alpha = 0;
                DeadPanel.alpha = 1;
                break;
            case PanelType.MainPanel:
                SetPanelInteractable(PanelType.GamePanel);
                GamePanel.alpha = 1;
                PausePanel.alpha = 0;
                DeadPanel.alpha = 0;
                DisableAllGamePanels();
                HUDPanel.alpha = 1;
                break;
            case PanelType.InventoryPanel:
                SetPanelInteractable(PanelType.GamePanel);
                GamePanel.alpha = 1;
                PausePanel.alpha = 0;
                DeadPanel.alpha = 0;
                DisableAllGamePanels();
                InventoryPanel.alpha = 1;
                
                IsInventoryShown = true;
                break;
        }
    }
    /// <summary>
    /// Set the panel as interactable.
    /// </summary>
    public void SetPanelInteractable(PanelType panel)
    {
        GamePanel.interactable = panel == PanelType.GamePanel;
        GamePanel.blocksRaycasts = panel == PanelType.GamePanel;

        PausePanel.interactable = panel == PanelType.PausePanel;
        PausePanel.blocksRaycasts = panel == PanelType.PausePanel;

        DeadPanel.interactable = panel == PanelType.DeadPanel;
        DeadPanel.blocksRaycasts = panel == PanelType.DeadPanel;
    }

    /// <summary>
    /// Disable all game panels. (HUD, Tab, Inventory etc.)
    /// </summary>
    public void DisableAllGamePanels()
    {
        HUDPanel.alpha = 0;
        
        InventoryPanel.alpha = 0;
        IsInventoryShown = false;
    }

    //
    //    private void Update()
    //    {
    //
    //
    //
    //
    //        OpenTablet();
    //
    //        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
    //        {
    //            if (PausePanel != null) 
    //            if (!PausePanel.activeSelf)
    //                Pause();
    //            else Resume();
    //        }
    //
    //
    //    }
    //
    //    private void OpenTablet()
    //    {
    //        if (Input.GetKeyDown(KeyCode.T) && tablet.activeSelf == false)
    //        {
    //            tabletUI.SetActive(!tabletUI.activeSelf);
    //            if (Cursor.lockState == CursorLockMode.Locked)
    //                Cursor.lockState = CursorLockMode.None;
    //            else Cursor.lockState = CursorLockMode.Locked;
    //        }
    //    }
    //
    //    public void Pause()
    //    {
    //
    //        PausePanel.SetActive(true);
    //        Time.timeScale = 0;
    //        Cursor.lockState = CursorLockMode.None;
    //    }
    //
    //    public void Resume()
    //    {
    //
    //        PausePanel.SetActive(false);
    //        Time.timeScale = 1;
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //
    //    public void Settings()
    //    {
    //        PauseMenu.SetActive(false);
    //        SettingsMenu.SetActive(true);
    //    }
    //
    //    public void BackToPauseMenu()
    //    {
    //        PauseMenu.SetActive(true);
    //        SettingsMenu.SetActive(false);
    //    }
    //
    //
    //    public void Quit()
    //    {
    //        PauseMenu.SetActive(false);
    //        QuitMenu.SetActive(true);
    //    }
    //
    //    public void QuitToMainMenu()
    //    {
    //        //load main menu scene
    //    }
    //
    //    public void QuitToDesktop()
    //    {
    //#if UNITY_EDITOR
    //        UnityEditor.EditorApplication.isPlaying = false;
    //#endif
    //        Application.Quit();
    //    }
}