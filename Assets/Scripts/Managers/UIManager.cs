using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    public GameObject[] UIInventorySlots;

    [SerializeField] private GameObject UIInventoryActiveItemSlot = null;
    [SerializeField] private GameObject UIInventoryAvailableItemSlot = null;
    
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
        
        // Check if the sprite is null
        if (icon == null)
        {
            
            UIInventoryActiveItemSlot.GetComponent<Image>().sprite = icon;
            UIInventoryActiveItemSlot = null;

        }
        else
        {
            
            UIInventoryAvailableItemSlot.GetComponent<Image>().sprite = icon;
        }
        

    }

    public GameObject GetInventoryAvailableSlot()
    {
        int availableslotIndex = Array.IndexOf(InventoryManager.Instance.slots, InventoryManager.Instance.availableItemSlot);
        if (availableslotIndex >=0 && availableslotIndex < UIInventorySlots.Length)
        UIInventoryAvailableItemSlot = UIInventorySlots[availableslotIndex];
        return UIInventoryAvailableItemSlot;
    }

    public void switchInventoryActiveUISlot(int slotNumber)
    {
        
        if (UIInventoryActiveItemSlot != null)
        {
            Image UICurrentItem = UIInventoryActiveItemSlot.GetComponent<Image>();
            UICurrentItem.color = new Color(255, 255, 255, 0.5f); 
        }

        if (slotNumber == -1)
        {
            UIInventoryActiveItemSlot = null;
        }else
        {
            UIInventoryActiveItemSlot = UIInventorySlots[slotNumber];

            Image UINewItem = UIInventoryActiveItemSlot.GetComponent<Image>();  
            UINewItem.color = new Color(255, 255, 255, 1f);

        }

        

        
    } 
    #endregion


}
