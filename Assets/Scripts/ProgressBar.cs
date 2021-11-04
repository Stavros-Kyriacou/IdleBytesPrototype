using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public int maximum;
    [Range(0, 100)] public int current;
    public Image mask;
    // public Image fill;
    // public Color fillColour;

    private void Update()
    {
        // GetCurrentFill();
    }
    public void GetCurrentFill(int min, int max)
    {
        // float fillAmount = (float)current / (float)maximum;
        // mask.fillAmount = fillAmount;
        // fill.color = fillColour;
        float fillAmout = (float)min / (float)max;
        Debug.Log(fillAmout);
        mask.fillAmount = fillAmout;
    }
}
