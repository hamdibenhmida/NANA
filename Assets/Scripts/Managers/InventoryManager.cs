
using System;
using UnityEngine;
using UnityEngine.UI;



public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; set; }

    [SerializeField] private GameObject[] slots;

    [SerializeField] private GameObject activeItemSlot ;
    [SerializeField] private GameObject availableItemSlot ;

    


    [SerializeField] private Transform FPSCamera;
    [SerializeField] private float dropForwardForce;
    [SerializeField] private float dropUpwardForce;

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

        if (FPSCamera == null)
            FPSCamera = Camera.main.transform;
       UpdateInventoryUI();
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
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
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

        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        foreach (GameObject Slot in slots)
        {
            Image slotIcon = Slot.GetComponent<Slot>().UISlot.GetComponent<Image>();

            if (Slot == activeItemSlot)
            {
                slotIcon.color = new Color(255, 255, 255, 1f);
            }
            else
            {
                slotIcon.color = new Color(255, 255, 255, 0.5f);
            }
        }

        

    }

    private void AddItemIntoInvetory(GameObject pickedUpItem)
    {
        Outline outline = pickedUpItem.GetComponent<Outline>();
        Destroy(outline);


        pickedUpItem.transform.SetParent(availableItemSlot.transform,false);

        Item item = pickedUpItem.GetComponent<Item>();
        // make collider istrigger
        MeshCollider itemCollider = pickedUpItem.GetComponent<MeshCollider>();
        
            itemCollider.isTrigger = true;
     

        // Disable the Rigidbody's gravity and set kinematic to true
        Rigidbody itemRigidbody = pickedUpItem.GetComponent<Rigidbody>();
        Destroy(itemRigidbody);
       


        pickedUpItem.transform.localPosition = new Vector3(item.spawnPosition.x , item.spawnPosition.y,item.spawnPosition.z);
        pickedUpItem.transform.localRotation = Quaternion.Euler(item.spawnRotation.x, item.spawnRotation.y,item.spawnRotation.z);




        activeItemSlot.GetComponent<Slot>().UpdateSlotUI();
        
    }

   
    private void SetActiveSlot(int slotNumber)
    {

        if (slots[slotNumber].transform.childCount > 0)
        {
            
            if (activeItemSlot != null && activeItemSlot.transform.childCount > 0)
            {
                Item currentItem = activeItemSlot.transform.GetChild(0).GetComponent<Item>();
                currentItem.isActiveItem = false;
            }

            if (activeItemSlot == slots[slotNumber]) // Check if the selected slot is already active
            {
                activeItemSlot = null; // Deactivate the slot
               // InventoryUI.Instance.switchInventoryActiveUISlot(-1); // Pass -1 to indicate no active slot
                return;
            }

            activeItemSlot = slots[slotNumber];
        //    InventoryUI.Instance.switchInventoryActiveUISlot(slotNumber);

            if (activeItemSlot.transform.childCount > 0)
            {
                Item newItem = activeItemSlot.transform.GetChild(0).GetComponent<Item>();
                newItem.isActiveItem = true;
            } 
        }

    }

    private void DropItem()
    {

        

        if (activeItemSlot != null && activeItemSlot.transform.childCount > 0)
        {

            Item currentItem = activeItemSlot.transform.GetChild(0).GetComponent<Item>();
            currentItem.gameObject.AddComponent<Outline>().enabled = false;

            currentItem.transform.SetParent(null);
            currentItem.isActiveItem = false;

            currentItem.GetComponent<Collider>().isTrigger = false;// reset collider to normal

            currentItem.AddRigidbody();


            Rigidbody itemRigidbody = currentItem.GetComponent<Rigidbody>();
            itemRigidbody.AddForce(FPSCamera.forward * dropForwardForce, ForceMode.Impulse);
            itemRigidbody.AddForce(FPSCamera.up * dropUpwardForce, ForceMode.Impulse);

            

            // Reset the activeItemSlot and update the UI to reflect no active slot
            activeItemSlot = null;
      //      InventoryUI.Instance.updateInventoryItemIcon(null); // Pass null to indicate no item is being held

            
            
        }
        
        



    }


}
