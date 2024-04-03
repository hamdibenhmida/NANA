using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keypad : Interactable
{
    
    [SerializeField] private GameObject door;
    private bool doorOpen;

    [SerializeField] private GameObject key;

    

    public bool doorLocked = true;
    protected override void interact()
    {
       
        if(!key.activeSelf  ) 
        {
            
            useEvents = true;
            
        }
        //if (!doorLocked)
        //{
        //    Debug.Log("unlocked");
        //    doorOpen = !doorOpen;
        //    door.GetComponent<Animator>().SetBool("isOpen", doorOpen);
        //}

       

    }
}
