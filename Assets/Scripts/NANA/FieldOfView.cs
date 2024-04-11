using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    [Header("General")]
    public Transform head;
      
    [Range(1, 50)]
    public float distanceView = 10;
    
    [Header("Raycast")]
    public string enemiesTag = "Respawn";
    [Range(2, 180)]
    public float extraRaysByLayer = 20;
    [Range(5, 180)]
    public float ViewAngle = 120;
    [Range(1, 10)]
    public int numberOfLayers = 3;
    [Range(0.02f, 0.15f)]
    public float distanceBetweenLayers = 0.1f;
        
    [Space(20)]
    public List<Transform> visibleEnemies = new List<Transform>();
    List<Transform> temporaryCollisionList = new List<Transform>();

    [Header("Edit Mode Only")]
    public bool drawRays = true;
    public bool drawDetectRays = true;

    private void Start()
    {
        
        if (!head)
        {
            head = transform;
        }
        
        
    }

    void Update()
    {
        
        CheckEnemies();
        
    }

    private void CheckEnemies()
    {
        
        float limitLayers = numberOfLayers * 0.5f;
        for (int x = 0; x <= extraRaysByLayer; x++)
        {
            for (float y = -limitLayers + 0.5f; y <= limitLayers; y++)
            {
                float angleToRay = x * (ViewAngle / extraRaysByLayer) + ((180.0f - ViewAngle) * 0.5f);
                Vector3 directionMultipl = (-head.right  ) + (head.up * y * distanceBetweenLayers )  ;
                Vector3 rayDirection = Quaternion.AngleAxis(angleToRay, head.up ) * directionMultipl  ;
                

                RaycastHit hitRaycast;
                if (Physics.Raycast(head.position, rayDirection, out hitRaycast, distanceView))
                {
                    if (!hitRaycast.transform.IsChildOf(transform.root) && !hitRaycast.collider.isTrigger)
                    {
                        if (hitRaycast.collider.gameObject.CompareTag(enemiesTag))
                        {
                            if(drawDetectRays)
                                Debug.DrawLine(head.position, hitRaycast.point, Color.red);
                            
                            if (!temporaryCollisionList.Contains(hitRaycast.transform))
                            {
                                temporaryCollisionList.Add(hitRaycast.transform);
                            }
                            if (!visibleEnemies.Contains(hitRaycast.transform))
                            {
                                visibleEnemies.Add(hitRaycast.transform);
                            }
                        }
                    }
                }
                else
                {
                    if(drawRays)
                        Debug.DrawRay(head.position, rayDirection * distanceView , Color.green);
                    
                }
            }
        }


        //Remove enemies that are not visible from the list
        for (int x = 0; x < visibleEnemies.Count; x++)
        {
            if (!temporaryCollisionList.Contains(visibleEnemies[x]))
            {
                visibleEnemies.Remove(visibleEnemies[x]);
            }
        }
        temporaryCollisionList.Clear();
    }


}
