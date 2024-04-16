using UnityEngine;
using UnityEngine.UI;


public abstract class Interactable : MonoBehaviour
{
    [Header("Interactable Infos")]
    public string title;
    public string Description;


    [Header("Interact Settings")]
    public bool useEvents;
    
    public string promptMessage;
    public Sprite keyToPress ;

    
    protected virtual void Awake()
    {
        Collider collider = gameObject.GetComponent<Collider>();
        if (!collider)
        {
            gameObject.AddComponent<MeshCollider>().convex = true;
        }
        else if (collider != gameObject.GetComponent<MeshCollider>())
        {
            Destroy(collider);
            gameObject.AddComponent<MeshCollider>().convex = true;
        }
        else
        {
            gameObject.GetComponent<MeshCollider>().convex = true;
        }

        if (keyToPress == null)
        {
            keyToPress = Resources.Load<Sprite>("sprites/Xelu_Free_Controller&Key_Prompts/Keyboard & Mouse/Light/E_Key_Light");
        }
    }
    public void BeseInteract()
    {
        
        if (useEvents) 
            GetComponent<interactionEvent>().OnInteract.Invoke() ;
        interact();
    }
    protected virtual void interact()
    {

    }

    
   
}
