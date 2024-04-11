 using UnityEngine;

public class HidePlace : Interactable
{
    [Header ("Hiding Place Settings")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform hidePlace;
    [SerializeField] private Transform freePlace;


    [SerializeField] private bool playerHidingHere = false;

    protected override void interact()
    {
        base.interact();
        EnterHiding();
    }

    public void EnterHiding()
    {
        
        if (!playerHidingHere)
        {
            // implement animation and change placing character
            /* need to cahnge */ 
            player.GetComponent<CharacterController>().enabled = false;
            player.position = hidePlace.position;
            playerHidingHere = true;
           
        }
        else
        {
            // implement animation and change placing character
            /* need to cahnge */ player.position = freePlace.position;
            player.GetComponent<CharacterController>().enabled = true;
            playerHidingHere = false;
            
        }

    }

 
}