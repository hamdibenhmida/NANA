using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeScreenColor : MonoBehaviour
{
    public Material material;
    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();    
        
    }
    public void ChangeColor()
    {
        renderer.sharedMaterial = material;
    }
}
