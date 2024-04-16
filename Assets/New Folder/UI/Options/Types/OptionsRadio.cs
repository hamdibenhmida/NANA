using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class OptionsRadio : OptionBehaviour
{
    public TMP_Text RadioText;
    public int Current = 0;
   
    [Header("Radio Options")]
    public bool IsCustomData;
    public string[] Options;

    

    [Header("Events")]
    public UnityEvent OnChange;
   
    private void Start()
    {
        if (IsCustomData)
            return;
   
        
        for (int i = 0; i < Options.Length; i++)
        {
            
   
                
                if (i == Current)
                    RadioText.text = Options[i];
            
        }
        
        SetOption(Current);
        
    }

     public void ChangeOption(int change)
     {
         int nextOption = GameTools.Wrap(Current + change, 0, Options.Length);
         SetOption(nextOption);
     }
    
    public void SetOption(int index)
    {
        
        Current = index;
        RadioText.text = Options[Current];

        OnChange?.Invoke();
        IsChanged = true;
    }

    
   
    public override object GetOptionValue()
    {
        return Current;
    }
   
    public override void SetOptionValue(object value)
    {
        int radio = Convert.ToInt32(value);
        SetOption(radio);
        IsChanged = false;
    }
}

