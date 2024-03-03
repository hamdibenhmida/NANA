using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; set; }

    public GameObject[] slots;

    public GameObject activeItemSlot = null;
    public GameObject availableItemSlot = null;

    public int i = 1;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance );
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        activeItemSlot = slots[0];
        availableItemSlot = slots[0];
    }

    private void Update()
    {
        foreach(GameObject slot in slots) 
        { 
            if( slot == activeItemSlot )
            {
                slot.SetActive(true);
            }
            else
            {
                slot.SetActive(false);
            }
        }
        
        while (availableItemSlot.transform.childCount !=0 && i < slots.Length )
        {
            availableItemSlot = slots[i];
            i++;
        }

     

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchActiveSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchActiveSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            switchActiveSlot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            switchActiveSlot(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            switchActiveSlot(4);
        }
        
            
    }

    

    public void pickUpItem(GameObject pickedUpItem)
    {
        AddItemIntoInvetory(pickedUpItem);
    }

    private void AddItemIntoInvetory(GameObject pickedUpItem)
    {
        pickedUpItem.transform.SetParent(availableItemSlot.transform,false);

        item item = pickedUpItem.GetComponent<item>();

        pickedUpItem.transform.GetComponent<BoxCollider>().enabled = false; //need to change the collider component later
        pickedUpItem.transform.localPosition = new Vector3(item.spawnPosition.x , item.spawnPosition.y,item.spawnPosition.z);
        pickedUpItem.transform.localRotation = Quaternion.Euler(item.spawnRotation.x, item.spawnRotation.y,item.spawnRotation.z);

        item.isActiveItem = true;
    }

    private void switchActiveSlot(int slotNumber)
    {
        if(activeItemSlot.transform.childCount > 0)
        {
            item currentItem = activeItemSlot.transform.GetChild(0).GetComponent<item>();
            currentItem.isActiveItem = false;
        }

        activeItemSlot = slots[slotNumber];
        if(activeItemSlot.transform.childCount == 0)
        {
            item newItem = activeItemSlot.transform.GetChild(0).GetComponent<item>();
            newItem.isActiveItem = true;
        }
    }
}
