using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractButton : MonoBehaviour
{
    public TMP_Text InteractInfo;
    public Image ButtonImage;
  
    public void SetButton(Interactable interactable)
    {
        gameObject.SetActive(true);
        InteractInfo.text = interactable.promptMessage;
        ButtonImage.sprite = interactable.keyToPress;
    }

    public void HideButton()
    {
        gameObject.SetActive(false);
    }

}
