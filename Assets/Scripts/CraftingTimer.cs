using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CraftingTimer : MonoBehaviour
{
    public TextMeshProUGUI timeRemainingText;
    public TextMeshProUGUI craftInfoText;
    public int craftTime;
    public int timeRemaining;
    public int componentType;
    public int componentTier;
    public int componentLevel;
    public bool craftComplete = false;
    public void StartCraft(int duration, int tier, int level, int type)
    {
        this.craftTime = duration;
        this.timeRemaining = this.craftTime;
        this.componentType = type;
        this.componentTier = tier;
        this.componentLevel = level;
        craftInfoText.text = $"Tier: {this.componentTier} {Ext.ComponentType(this.componentType)}";
        TimeSpan time = TimeSpan.FromSeconds(this.timeRemaining);
        timeRemainingText.text = "Time Remaining: " + time.ToString(@"hh\:mm\:ss");
        StartCoroutine("CraftTimer");
    }
    IEnumerator CraftTimer()
    {
        while (this.timeRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            this.timeRemaining--;
            TimeSpan time = TimeSpan.FromSeconds(this.timeRemaining);
            timeRemainingText.text = "Time Remaining: " + time.ToString(@"hh\:mm\:ss");
            if (this.timeRemaining <= 0)
            {
                StopCoroutine("CraftTimer");
                this.craftComplete = true;
                this.craftInfoText.text = $"Tier: {this.componentTier} Level: {this.componentLevel} {Ext.ComponentType(this.componentType)}";
                break;
            }
        }
    }
    public void CollectCraft()
    {
        if (this.craftComplete)
        {
            //collect
            switch (this.componentType)
            {
                case 1:
                    Inventory.Instance.cpuInventory[this.componentTier - 1, this.componentLevel - 1]++;
                    break;
                case 2:
                    Inventory.Instance.gpuInventory[this.componentTier - 1, this.componentLevel - 1]++;
                    break;
                case 3:
                    Inventory.Instance.ramInventory[this.componentTier - 1, this.componentLevel - 1]++;
                    break;
                case 4:
                    Inventory.Instance.hddInventory[this.componentTier - 1, this.componentLevel - 1]++;
                    break;
                default:
                    break;
            }
            ResetTimer();
        }
        else
        {
            Debug.Log("Craft is not complete");
        }
    }
    public void CompleteNow()
    {
        //must pay premium currency
        Debug.Log("You must pay $300 :)");
    }
    public void ResetTimer()
    {
        this.craftComplete = false;
        this.craftTime = 0;
        this.timeRemaining = 0;
        this.componentType = 0;
        this.componentTier = 0;
        this.componentLevel = 0;
        this.craftInfoText.text = "Craft Info";
    }
}
