using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CancelCraftMenu : MonoBehaviour
{
    public TextMeshProUGUI GraceTimeText;
    public TextMeshProUGUI DescriptionText;
    public CraftingTimer currentTimer;

    public void HideMenu()
    {
        var r = GetComponent<RectTransform>();
        if (r.anchoredPosition == Vector2.zero)
        {
            r.anchoredPosition = Vector2.up * 1000;
            var graceTimer = currentTimer.GetComponent<Timer>();
            graceTimer.cancelCraftMenu = null;
        }
        else
        {
            r.anchoredPosition = Vector2.zero;
        }
    }
}
