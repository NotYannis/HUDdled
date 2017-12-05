using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBarMonitor : ResourcesMonitor {
    public Image fillBar;
    private Color baseColor;

    void Start()
    {
        if(fillBar != null)
        {
            baseColor = fillBar.color;
        }
        keyPressedFeedback = Resources.Load<Sprite>("GRAPH/UI/keyPressed");
        keyReleasedFeedback = Resources.Load<Sprite>("GRAPH/UI/keyReleased");
    }

    public override void UpdateUI()
    {
        Fill((float)currentAmount / (float)maxAmount);
    }

    public void Fill(float percent)
    {
        fillBar.fillAmount = percent;
    }

    public void ChangeColor(Color newColor)
    {
        fillBar.color = newColor;
    }
    
    public void ResetColor()
    {
        ChangeColor(baseColor);
    }
}
