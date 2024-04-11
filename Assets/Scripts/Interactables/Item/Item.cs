using UnityEngine;

public class Item : Interactable
{

    [Header("Item Settings",order =0)]
    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

    public Sprite icon;

    public bool isActiveItem = false;
    


   

    private void Start()
    {
        if (gameObject.GetComponent<Outline>())
            gameObject.GetComponent<Outline>().enabled = false;
        else
            gameObject.AddComponent<Outline>().enabled = false;

        

        AddRigidbody();
    }

    public void Hovered(bool hovered)
    {
        Outline outline = gameObject.GetComponent<Outline>();
        if (hovered)
        outline.enabled = true;
        else outline.enabled = false;
    }
    public Rigidbody AddRigidbody()
    {
        
        Rigidbody rigidbBody = gameObject.GetComponent<Rigidbody>();

        if (rigidbBody == null )
        {
            rigidbBody= gameObject.AddComponent<Rigidbody>();
        }
        rigidbBody.interpolation = RigidbodyInterpolation.Extrapolate;
        rigidbBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;

        return rigidbBody;
    }
    protected override void interact()
    {
        base.interact();
        InventoryManager.Instance.pickUpItem(this.gameObject);
        
    }

  

}
