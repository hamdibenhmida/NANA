using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public struct InteractInfo
{
    public string ObjectName;
    public Interactable interactable;
}




public class InteractInfoPanel : MonoBehaviour
{
    public CanvasGroup CanvasGroup;
    public TMP_Text InteractName;
    public InteractButton interactButton;

    [Header("Fading")]
    public float FadeSpeed = 5f;

    private bool fadeState;
   

    private void Update()
    {
        CanvasGroup.alpha = Mathf.MoveTowards(CanvasGroup.alpha, fadeState ? 1 : 0, Time.deltaTime * FadeSpeed);
    }

    public void ShowInfo(InteractInfo interactInfo)
    {
        
        // interact name
        if (!string.IsNullOrEmpty(interactInfo.ObjectName))
            InteractName.text = interactInfo.ObjectName;

        interactButton.gameObject.SetActive(true);

        interactButton.SetButton(interactInfo.interactable);

        // show info
        fadeState = true;
    }

    public void HideInfo()
    {
        
        fadeState = false;
    }
}
