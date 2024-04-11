using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public abstract class Interactable : MonoBehaviour
{
    [Header("Interact Settings")]
    public bool useEvents;
    
    public string promptMessage;
    public Sprite keyToPress ;


    protected virtual void Awake()
    {
        gameObject.GetComponent<MeshCollider>().convex = true;
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
