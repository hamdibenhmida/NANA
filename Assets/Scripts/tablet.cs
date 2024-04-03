using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class tablet : MonoBehaviour
{
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Openmenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void Closemenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    public void UnlockDoor(GameObject door)
    {
        keypad[] keypads = door.GetComponentsInChildren<keypad>();
        changeScreenColor[] changeScreenColor = door.GetComponentsInChildren<changeScreenColor>();
        foreach (var screen in keypads)
        {
            screen.doorLocked = false;
        }
        foreach (var screen in changeScreenColor)
        {
            screen.ChangeColor();
        }
    }

    public void SwitchToCamera(GameObject TargetCamera)
    {
        
        TargetCamera.SetActive(!TargetCamera.activeSelf);
    }
    
}