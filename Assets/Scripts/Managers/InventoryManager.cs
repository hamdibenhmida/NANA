
using UnityEngine;



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
        GetAvailableSlot();
        if (FPSCamera == null)
            FPSCamera = Camera.main.transform;
       UpdateInventoryUI();
    }

    private void Update()
    {
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

    

    private void UpdateInventoryUI()
    {
        foreach (GameObject Slot in slots)
        {
            Slot.GetComponent<Slot>().UpdateSlotUI();
        }

    }

    public void PickUpItem(GameObject pickedUpItem)
    {
        if (availableItemSlot.transform.childCount == 0)
        {
            pickedUpItem.transform.SetParent(availableItemSlot.transform, false);


            availableItemSlot.GetComponent<Slot>().UpdateSlotUI();
            GetAvailableSlot();

            Item item = pickedUpItem.GetComponent<Item>();
            // make collider istrigger
            MeshCollider itemCollider = pickedUpItem.GetComponent<MeshCollider>();

            itemCollider.isTrigger = true;

            // Disable the Rigidbody's gravity and set kinematic to true
            Rigidbody itemRigidbody = pickedUpItem.GetComponent<Rigidbody>();
            Destroy(itemRigidbody);

            pickedUpItem.transform.localPosition = new Vector3(item.spawnPosition.x, item.spawnPosition.y, item.spawnPosition.z);
            pickedUpItem.transform.localRotation = Quaternion.Euler(item.spawnRotation.x, item.spawnRotation.y, item.spawnRotation.z);
        }
    }

   
    private void SetActiveSlot(int slotNumber)
    {
        if (slots[slotNumber].transform.childCount > 0)
        {
            if (activeItemSlot != null && activeItemSlot.transform.childCount > 0)
            {
                activeItemSlot.SetActive(false);
            }

            if (activeItemSlot == slots[slotNumber]) // Check if the selected slot is already active
            {
                activeItemSlot.SetActive(false);
                activeItemSlot = null; // Deactivate the slot
                
                return;
            }

            activeItemSlot = slots[slotNumber];
            activeItemSlot.SetActive(true);

        }

    }

    private void DropItem()
    {
        if (activeItemSlot != null && activeItemSlot.transform.childCount > 0)
        {
            Item currentItem = activeItemSlot.transform.GetChild(0).GetComponent<Item>();
            
            currentItem.transform.parent.gameObject.SetActive(false);
            currentItem.transform.SetParent(null);
            
            currentItem.GetComponent<Collider>().isTrigger = false;// reset collider to normal

            currentItem.AddRigidbody();

            Rigidbody itemRigidbody = currentItem.GetComponent<Rigidbody>();
            itemRigidbody.AddForce(FPSCamera.forward * dropForwardForce, ForceMode.Impulse);
            itemRigidbody.AddForce(FPSCamera.up * dropUpwardForce, ForceMode.Impulse);

            activeItemSlot.GetComponent<Slot>().UpdateSlotUI();

            // Reset the activeItemSlot and update the UI to reflect no active slot
            activeItemSlot = null;
       
        }

        GetAvailableSlot();
        
    }

}
