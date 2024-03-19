using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Rigidbody))]
public class item : MonoBehaviour
{
    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

    public Sprite icon;

    public bool isActiveItem = false;

    

    private void Start()
    {
        gameObject.GetComponent<BoxCollider>();
        gameObject.GetComponent<Outline>().enabled = false;
        Rigidbody rigidbBody = gameObject.GetComponent<Rigidbody>();
        rigidbBody.interpolation = RigidbodyInterpolation.Extrapolate;
        rigidbBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;


    }
    private void Update()
    {
        if (isActiveItem)
        {
            gameObject.GetComponent<Outline>().enabled = false;
        }
    }
}
