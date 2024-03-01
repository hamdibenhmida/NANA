using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hide : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform hidePlace;
    [SerializeField] private Transform freePlace;


    private bool isHiding = false;
    

    public void EnterHiding()
    {
        
        if (!isHiding)
        {
            // implement animation and change placing character
            /* need to cahnge */ player.position = hidePlace.position;
            isHiding = true;
           
        }
        else
        {
            // implement animation and change placing character
            /* need to cahnge */ player.position = freePlace.position;

            isHiding = false;
            
        }

    }


}