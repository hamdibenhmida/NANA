
using System;
using UnityEngine;




[RequireComponent(typeof(PlayerUI))]
public class playerInteract : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Interact Settings")]
    public InputReference UseAction;
    public InputReference ExamineAction;

    [SerializeField] private float distance = 3f;
    
    private Transform cameraTransform;

    private PlayerUI playerUI;

    private GameObject hoveredInteractable = null;
    private Item item= null;
    private Interactable interactable = null;

    [SerializeField] private LayerMask layerMask;
    // Start is called before the first frame update

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    void Start()
    {
        
        cameraTransform = Camera.main.transform;
        playerUI = GetComponent<PlayerUI>();

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.blue);
        
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo, distance, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hitInfo.collider.tag == "Interactable")
            {
                hoveredInteractable = hitInfo.collider.gameObject;
                interactable = hoveredInteractable.GetComponent<Interactable>();

                item = hoveredInteractable.GetComponent<Item>();

                if (item)
                {
                    item.Hovered(true);
                }

                OnInteractUI();
                
                //playerUI.updateText(interactable.promptMessage);
                //playerUI.updatesprite(interactable.keyToPress, 100f );

                //interact with interactable objects
                if (Input.GetKeyDown("e"))// need to  change this input to the new input system
                {
                    interactable.BeseInteract();
                    hoveredInteractable = null;
                    interactable = null;
                    item = null;
                }
            }
            else 
            {
                //hide interact UI
                gameManager.InteractInfoPanel.HideInfo();

                // disable outline when not looking at an interactable object
                if ( item)
                {
                    hoveredInteractable.GetComponent<Item>().Hovered(false);
                }
                hoveredInteractable = null;
                item = null;
            }
        }
        else 
        {
            //hide interact UI
            gameManager.InteractInfoPanel.HideInfo();

            // disable outline when not looking at an interactable object
            if (item)
            {
                hoveredInteractable.GetComponent<Item>().Hovered(false);
            }
        }
    }

    private void OnInteractUI()
    {
        if (interactable == null)
            return;

        string titleText = null;
        string button1Text = null;
       

        titleText = interactable.title;

        InputReference button1Action = UseAction;



        gameManager.InteractInfoPanel.ShowInfo(new()
        {
            ObjectName = titleText,
            interactable = interactable
        }) ;
    }
}

