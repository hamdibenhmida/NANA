using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{
    public GameObject UISlot;
    

    private void Awake()
    {
        UpdateSlotUI();
        gameObject.SetActive(false);
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

    private void OnEnable()
    {
        UISlot.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
    }

    private void OnDisable()
    {
        UISlot.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
    }
}
