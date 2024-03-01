using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

    public class playerInteract : MonoBehaviour
    {
        

        public Transform cameraTransform;
        [SerializeField] private float distance = 3f;
        

        private playerUI playerUI;
        

        // Start is called before the first frame update
        void Start()
        {
            
            cameraTransform = Camera.main.transform;
            playerUI = GetComponent<playerUI>();

        }

        // Update is called once per frame
        void Update()
        {
            playerUI.updateText(string.Empty);
            playerUI.updatesprite(default ,0f );

            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.blue);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, distance))
            {
                if(hitInfo.collider.tag == "interactable")
                {
                    if (hitInfo.collider.GetComponent<Interactable>() != null)
                    {
                        Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                        playerUI.updateText(interactable.promptMessage);
                        playerUI.updatesprite(interactable.keyToPress, 100f );
                        if (UnityEngine.Input.GetKeyDown ("e"))
                        {
                            interactable.BeseInteract();
                        }
                    }
                }
                
            }
        }
    }

