
using UnityEngine;


[RequireComponent(typeof(PlayerUI))]
public class playerInteract : MonoBehaviour
{
    

    
    [SerializeField] private float distance = 3f;
    
    private Transform cameraTransform;

    private PlayerUI playerUI;

    private GameObject hoveredInteractable = null;
    private Item item= null;

    [SerializeField] private LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
        cameraTransform = Camera.main.transform;
        playerUI = GetComponent<PlayerUI>();

    }

    // Update is called once per frame
    void Update()
    {
        playerUI.updateText(string.Empty);
        playerUI.updatesprite(default ,0f );

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.blue);
        
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo, distance, layerMask, QueryTriggerInteraction.Ignore))
        {
            
            if (hitInfo.collider.tag == "Interactable")
            {
                hoveredInteractable = hitInfo.collider.gameObject;
                Interactable interactable = hoveredInteractable.GetComponent<Interactable>();
                

                
                item = hoveredInteractable.GetComponent<Item>();

                
                if (item)
                {

                    item.Hovered(true);
                }

                //interact with interactable objects


                playerUI.updateText(interactable.promptMessage);
                    playerUI.updatesprite(interactable.keyToPress, 100f );

                    if (Input.GetKeyDown ("e"))// need to  change this input to the new input system
                    {
                        interactable.BeseInteract();
                        hoveredInteractable = null;
                        interactable = null;
                        item = null;
                    }
                
            }
            else // disable outline when not looking at an interactable object
            {
                
                if ( item)
                {
                    hoveredInteractable.GetComponent<Item>().Hovered(false);
                }
                hoveredInteractable = null;
                item = null;
            }

        }
        else // disable outline when not looking at an interactable object
        {
            if (item)
            {
                hoveredInteractable.GetComponent<Item>().Hovered(false);
            }
        }

    }
}

