using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    public GameObject[] UIInventorySlots;

    public GameObject UIInventoryActiveItemSlot = null;
    public GameObject UIInventoryAvailableItemSlot = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        UIInventoryActiveItemSlot = UIInventorySlots[0];
        UIInventoryAvailableItemSlot = UIInventorySlots[0];
    }

    private void Update()
    {
        highlightInventoryActiveItemIcon();

        //get empty slot
        GetInventoryAvailableSlot();
    }
    #region INVENTORY UI METHODS

    private void highlightInventoryActiveItemIcon()
    {
        foreach (GameObject UISlot in UIInventorySlots)
        {
            if (UISlot == UIInventoryActiveItemSlot)
            {
                UISlot.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            }
            else
            {
                UISlot.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
            }
        }
    }

    public void updateInventoryItemIcon(Sprite icon)
    {
        UIInventoryAvailableItemSlot.GetComponent<Image>().sprite = icon;

    }

    public GameObject GetInventoryAvailableSlot()
    {
        UIInventoryAvailableItemSlot = UIInventorySlots[Array.IndexOf(InventoryManager.Instance.slots, InventoryManager.Instance.availableItemSlot)];
        return UIInventoryAvailableItemSlot;
    }

    public void switchInventoryActiveUISlot(int slotNumber)
    {
        Image UICurrentItem = UIInventoryActiveItemSlot.GetComponent<Image>();
        UICurrentItem.color = new Color(255, 255, 255, 0.5f);

        UIInventoryActiveItemSlot = UIInventorySlots[slotNumber];

        Image UINewItem = UIInventoryActiveItemSlot.GetComponent<Image>();
        UINewItem.color = new Color(255, 255, 255, 1f);
    } 
    #endregion


}
