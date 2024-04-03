using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

    public bool isActiveItem = false;
    private void Update()
    {
        if (isActiveItem)
        {
            gameObject.GetComponent<Outline>().enabled = false;
        }
    }
}
