using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

    public Sprite icon;

    public bool isActiveItem = false;

    

    private void Start()
    {
        gameObject.AddComponent<BoxCollider>();
        gameObject.AddComponent<Outline>().enabled = false;
        Rigidbody rigidbBody = gameObject.AddComponent<Rigidbody>();
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
