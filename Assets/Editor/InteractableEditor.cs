using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        if (target.GetType() == typeof(eventOnlyInteractable))
        {
            interactable.promptMessage = EditorGUILayout.TextField("prompt Message", interactable.promptMessage);
            EditorGUILayout.ObjectField("Key To Press", interactable.keyToPress, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.HelpBox("eventOnlyInteract can ONLY use UnityEvent.", MessageType.Info);
            if (interactable.GetComponent<interactionEvent>() == null)
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<interactionEvent>();
            }
        }
        else
        {
            base.OnInspectorGUI();
            if (interactable.useEvents)
            {
                if (interactable.GetComponent<interactionEvent>() == null)
                    interactable.gameObject.AddComponent<interactionEvent>();
            }
            else
            {
                if (interactable.GetComponent<interactionEvent>() != null)
                    DestroyImmediate(interactable.GetComponent<interactionEvent>());
            }
        }

    }
}