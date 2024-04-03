using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent (typeof(item))]
public class Phone : MonoBehaviour
{
    private AudioSource AudioSource ;
    private item item;
    [SerializeField] private GameObject UIPhone;
    private void Awake ()
    {
        AudioSource = GetComponent<AudioSource>();
        item = GetComponent<item>();
    }

   
    public void Update()
    {
        
        if (item.isActiveItem) 
        {
           
            UIPhone.SetActive(true);
            //need to change phone navigation

            #region phone navigation
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
            #endregion
        }
        else
        {

            UIPhone.SetActive(false);
            //need to change phone navigation

        }

    }

    public void answerPhone(AudioClip conversation) 
    {
        AudioSource.clip = conversation;
        AudioSource.Play ();
    }
}
