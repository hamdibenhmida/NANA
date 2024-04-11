using UnityEngine;


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

    

    public void SwitchToCamera(GameObject TargetCamera)
    {
        
        TargetCamera.SetActive(!TargetCamera.activeSelf);
    }
    
}