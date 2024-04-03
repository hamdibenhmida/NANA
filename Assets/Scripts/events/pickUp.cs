using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSource ;

    private void Awake ()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    
    public void answerPhone(AudioClip conversation) 
    {
        
        AudioSource.clip = conversation;
        AudioSource.Play ();
    }
}
