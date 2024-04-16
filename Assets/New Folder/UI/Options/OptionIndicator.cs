using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OptionIndicator : MonoBehaviour
{
    public Image[] Indicators;

    [Header("Colors")]
    public Color EnabledColor = Color.white;
    public Color DisabledColor = Color.white;

    public void SetIndicator(OptionsRadio optionsRadio)
    {
        int index = optionsRadio.Current;

        for (int i = 0; i < Indicators.Length; i++)
        {
            var indicator = Indicators[i];
            indicator.color = i == index
                ? EnabledColor : DisabledColor;
        }
    }
}
