using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int removeAmount;
    public int addAmount;
    public WorkbenchTierMenu tierMenu;
    public void RemoveComponent()
    {
        if (Inventory.Instance.RemoveComponent(1, 1, 1, removeAmount))
        {

        }
        else
        {
            Debug.Log("You do not have enough of that component");
        }
    }
    public void AddComponent()
    {
        Inventory.Instance.AddComponent(1, 1, 1, addAmount);
    }
    public void TimerAvailable()
    {
        var timer = tierMenu.CraftingSlots[0];
        Debug.Log($"Timer 1 available: {timer.TimerAvailable}");
    }
}