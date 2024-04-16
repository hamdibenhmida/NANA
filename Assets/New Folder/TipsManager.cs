using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TipsManager : MonoBehaviour
{
    public string[] TipsList;

    [Header("References")]
    public CanvasGroup TipsGroup;
    public TMP_Text TipText;

    [Header("Settings")]
    public float TipTime = 5f;
    public float TipChangeSpeed = 1f;

    private int lastTip;

    private void Awake()
    {
        for (int i = 0; i < TipsList.Length; i++)
        {
           // TipsList[i].SubscribeGloc();
        }
    }

    public void StopTips()
    {
        StopAllCoroutines();
    }

    IEnumerator Start()
    {
        if (TipsList.Length == 1)
        {
            TipText.text = TipsList[0];
            TipsGroup.alpha = 1f;
        }
        else if (TipsList.Length > 1)
        {
            TipsGroup.alpha = 0f;

            while (true)
            {
                lastTip = GameTools.RandomUnique(0, TipsList.Length, lastTip);
                TipText.text = TipsList[lastTip];

                yield return CanvasGroupFader.StartFade(TipsGroup, true, TipChangeSpeed);
                yield return new WaitForSeconds(TipTime);
                yield return CanvasGroupFader.StartFade(TipsGroup, false, TipChangeSpeed);
            }
        }
    }
}
