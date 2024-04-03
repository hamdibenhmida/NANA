using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; set; }

    public GameObject[] slots;

    public GameObject activeItemSlot ;//{ get; private set; }
    public GameObject availableItemSlot ;//{ get; private set; }

    
    

    public Transform FPSCamera;
    public float dropForwardForce;
    public float dropUpwardForce;

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
        
        
       
    }

    private void Update()
    {
        //get active item into player hand
        GetActiveItem();

        //get empty slot
        GetAvailableSlot();

        //need to change the inputs to the new input system
        #region inputs 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveSlot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetActiveSlot(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetActiveSlot(4);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            DropItem();
        }
        #endregion 


    }

    private void GetActiveItem()
    {
        foreach (GameObject slot in slots)
        {
            if (slot == activeItemSlot)
            {
                slot.SetActive(true);
            }
            else
            {
                slot.SetActive(false);
            }
        }
    }

    private void GetAvailableSlot()
    {
        

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount == 0) 
            { 
                availableItemSlot = slots[i];
                break;
            }
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
        // make collider istrigger
        Collider itemCollider = pickedUpItem.GetComponent<Collider>();
        if (itemCollider != null)
        {
            itemCollider.isTrigger = true;
        }

        // Disable the Rigidbody's gravity and set kinematic to true
        Rigidbody itemRigidbody = pickedUpItem.GetComponent<Rigidbody>();
        if (itemRigidbody != null)
        {
            
            itemRigidbody.isKinematic = true;
        }


        pickedUpItem.transform.localPosition = new Vector3(item.spawnPosition.x , item.spawnPosition.y,item.spawnPosition.z);
        pickedUpItem.transform.localRotation = Quaternion.Euler(item.spawnRotation.x, item.spawnRotation.y,item.spawnRotation.z);

        item.isActiveItem = true;

        UIManager.Instance.updateInventoryItemIcon(item.icon);
        
    }

   
    private void SetActiveSlot(int slotNumber)
    {

        if (slots[slotNumber].transform.childCount > 0)
        {
            
            if (activeItemSlot != null && activeItemSlot.transform.childCount > 0)
            {
                item currentItem = activeItemSlot.transform.GetChild(0).GetComponent<item>();
                currentItem.isActiveItem = false;
            }

            if (activeItemSlot == slots[slotNumber]) // Check if the selected slot is already active
            {
                activeItemSlot = null; // Deactivate the slot
                UIManager.Instance.switchInventoryActiveUISlot(-1); // Pass -1 to indicate no active slot
                return;
            }

            activeItemSlot = slots[slotNumber];
            UIManager.Instance.switchInventoryActiveUISlot(slotNumber);

            if (activeItemSlot.transform.childCount > 0)
            {
                item newItem = activeItemSlot.transform.GetChild(0).GetComponent<item>();
                newItem.isActiveItem = true;
            } 
        }

    }

    private void DropItem()
    {
        
        if (activeItemSlot != null && activeItemSlot.transform.childCount > 0)
        {

            item currentItem = activeItemSlot.transform.GetChild(0).GetComponent<item>();
            currentItem.transform.SetParent(null);
            currentItem.isActiveItem = false;

            // Reset the activeItemSlot and update the UI to reflect no active slot
            activeItemSlot = null;
            UIManager.Instance.updateInventoryItemIcon(null); // Pass null to indicate no item is being held

            Rigidbody itemRigidbody = currentItem.GetComponent<Rigidbody>();
            if (itemRigidbody != null)
            {
                // Add force to throw the item forward
                currentItem.GetComponent<BoxCollider>().isTrigger = false;// reset collider to normal
                itemRigidbody.isKinematic = false; // Ensure the item's Rigidbody is not kinematic

                itemRigidbody.AddForce(FPSCamera.forward * dropForwardForce, ForceMode.Impulse);
                itemRigidbody.AddForce(FPSCamera.up * dropUpwardForce, ForceMode.Impulse);
            }
            else
            {
                Debug.LogWarning("Rigidbody component not found on the item.");
            }
            
        }
        
        



    }


}
