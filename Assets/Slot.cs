using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour
{
    public GameObject UISlot;

    private void Awake()
    {
        UpdateSlotUI();
    }
    private void Start()
    {
        
    }

    public void UpdateSlotUI() 
    {
        Image UISlotImage = UISlot.GetComponent<Image>();
        if (transform.childCount > 0)
        {
            Item item = transform.GetChild(0).GetComponent<Item>();
            if (item.icon != null)
            {
                UISlotImage.sprite = item.icon;
            }
        }
        else UISlotImage.sprite = default;


    }
}
