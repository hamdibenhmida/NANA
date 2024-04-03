using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class playerInteract : MonoBehaviour
{
    

    public Transform cameraTransform;
    [SerializeField] private float distance = 3f;
    

    private playerUI playerUI;

    private GameObject hoveredInteractable = null;
    

    // Start is called before the first frame update
    void Start()
    {
        
        cameraTransform = Camera.main.transform;
        playerUI = GetComponent<playerUI>();

    }

    // Update is called once per frame
    void Update()
    {
        playerUI.updateText(string.Empty);
        playerUI.updatesprite(default ,0f );

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.blue);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            if(hitInfo.collider.tag == "interactable")
            {
               
                // outline interactable object when looking at (if it is an item it have outline script)
                hoveredInteractable = hitInfo.collider.gameObject;
                if (hoveredInteractable.GetComponent<item>() )
                {
                    hoveredInteractable.GetComponent<Outline>().enabled = true; 
                }
                

                //interact with interactable objects
                if (hitInfo.collider.GetComponent<Interactable>() != null)
                {
                    Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                    playerUI.updateText(interactable.promptMessage);
                    playerUI.updatesprite(interactable.keyToPress, 100f );

                    if (UnityEngine.Input.GetKeyDown ("e"))// need to  change this input to the new input system
                    {
                        interactable.BeseInteract();

                        if (hoveredInteractable.GetComponent<item>())//check if the interactable is an item
                        {
                            InventoryManager.Instance.pickUpItem(hoveredInteractable); 
                        }
                    }
                }
            }
            else // disable outline when not looking at an interactable object
            {
                if (hoveredInteractable && hoveredInteractable.GetComponent<item>())
                {
                    hoveredInteractable.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
}

