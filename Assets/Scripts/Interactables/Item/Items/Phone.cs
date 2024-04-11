using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(AudioSource))]

public class Phone : Item 
{
    private AudioSource AudioSource;

    [Header("Phone Settings")]
    
    [SerializeField] private AudioClip conversation;
    [SerializeField] private GameObject UIPhone;

    protected override void Awake()
    {
        base.Awake();

        AudioSource = GetComponent<AudioSource>();
    }

    protected override void interact()
    {
        base.interact();
        answerPhone(conversation);
    }

    public void answerPhone(AudioClip conversation) 
    {
        AudioSource.clip = conversation;
        AudioSource.Play ();
    }
}
