using System;
using UnityEngine;

public class Door : Interactable
{
    Animator animator;
    [Header("Door Settings")]
    public bool isOpen = false;

    [SerializeField]private string openedDoorPromptMessage;
    [SerializeField]private string closeedDoorPromptMessage;  

    protected override void Awake()
    {
        base.Awake();
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("IsOpen", isOpen);
    }
    protected override void interact()
    {
        base.interact();
        OpenCloseDoor();
       

    }

    private void OpenCloseDoor()
    {
        if(animator != null) 
            animator.SetTrigger("open/close");
        else Debug.LogWarning(gameObject.name + " dont have animator component");
        isOpen = !isOpen;
        if (isOpen)
            promptMessage = openedDoorPromptMessage;
        else promptMessage = closeedDoorPromptMessage;
        
    }

    

   
}
