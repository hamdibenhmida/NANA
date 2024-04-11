using UnityEngine;

public class DoorContainer : MonoBehaviour
{
    public bool closeDoorWhenEnter = false;
    private void OnTriggerExit(Collider collider)
    {

        if (closeDoorWhenEnter)
        {
            GameObject door = transform.GetChild(0).gameObject;
            Animator animator = door.GetComponent<Animator>();

            if (collider.CompareTag("Player"))
                if (animator != null)
                    animator.SetTrigger("open/close");
                else Debug.LogWarning(door.name + " dont have animator component"); 
        }

    }    
}
