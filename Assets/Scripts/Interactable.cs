using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool useEvents;
    public string promptMessage;
   
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
