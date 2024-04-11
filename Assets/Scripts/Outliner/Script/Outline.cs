using System;
using System.Linq;
using UnityEngine;

public class Outline : MonoBehaviour
{
    private Renderer outlineRenderer;
    private Material outlineMaterial;
    bool needsUpdate;

    [Range(1f, 10f)]
    [SerializeField] private float thickness;
    [SerializeField] private Color outlineColor = Color.white;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        outlineMaterial = Resources.Load<Material>("Material/Outline");
        outlineRenderer = gameObject.GetComponent<Renderer>();
        needsUpdate = true;

    }
    private void Update()
    {
        if (needsUpdate)
        {
            needsUpdate = false;

            UpdateMaterialProperties();
        }
        
    }
    void OnValidate()
    {

        // Update material properties
        needsUpdate = true;
    }

        private void UpdateMaterialProperties()
    {
        outlineMaterial.SetColor("OutlineColor", outlineColor);
        outlineMaterial.SetFloat("Thickness", thickness);
    }

    private void OnEnable()
    {
        var materials = outlineRenderer.sharedMaterials.ToList();
        materials.Add(outlineMaterial);
        outlineRenderer.materials = materials.ToArray();
        
    }

    private void OnDisable()
    {
        var materials = outlineRenderer.sharedMaterials.ToList();
        materials.Remove(outlineMaterial);
        outlineRenderer.materials = materials.ToArray();
    }

}
