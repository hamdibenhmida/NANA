using UnityEngine;

public class Item : Interactable
{

    [Header("Item Settings",order =0)]
    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

    public Sprite icon;

    Outline outline;





    private void Start()
    {
        AddRigidbody();
    }

    private void OnEnable()
    {
        if (transform.parent && transform.parent.CompareTag("Slot"))
        {
            if (outline)
            Destroy(outline);
        }
        else
        {
            outline = outline ? gameObject.GetComponent<Outline>() : gameObject.AddComponent<Outline>();

            outline.enabled = false;

        }
    }

    public void Hovered(bool hovered) => outline.enabled = hovered;


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
        InventoryManager.Instance.PickUpItem(this.gameObject);
        
    }

  

}
