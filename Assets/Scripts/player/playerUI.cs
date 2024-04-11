using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image key;
    [SerializeField] private TextMeshProUGUI promptText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void updateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }
    public void updatesprite(Sprite keyToPress, float Opacity) 
    {
        key.sprite = keyToPress;
        key.color = new Color(key.color.r, key.color.g, key.color.b, Opacity);
    }
}
