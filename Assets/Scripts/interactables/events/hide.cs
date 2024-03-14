 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hide : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform hidePlace;
    [SerializeField] private Transform freePlace;


    [SerializeField] private bool isHiding = false;
    

    public void EnterHiding()
    {
        
        if (!isHiding)
        {
            // implement animation and change placing character
            /* need to cahnge */ 
            player.GetComponent<CharacterController>().enabled = false;
            player.position = hidePlace.position;
            isHiding = true;
           
        }
        else
        {
            // implement animation and change placing character
            /* need to cahnge */ player.position = freePlace.position;
            player.GetComponent<CharacterController>().enabled = true;
            isHiding = false;
            
        }

    }


}