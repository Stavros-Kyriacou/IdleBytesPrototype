using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CraftingTimer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeRemainingText;
    [SerializeField]
    private TextMeshProUGUI craftInfoText;
    [SerializeField]
    private Image collectButtonImage;
    private int craftTime;
    private int timeRemaining;
    private int componentType;
    private int componentTier;
    private int componentLevel;
    private bool craftComplete;
    private bool timerAvailable = true;
    public bool TimerAvailable
    {
        get
        {
            return this.timerAvailable;
        }
    }
    
    public void StartCraft(int duration, int tier, int level, int type)
    {
        this.craftTime = duration;
        this.timeRemaining = this.craftTime;
        this.componentType = type;
        this.componentTier = tier;
        this.componentLevel = level;
        this.craftComplete = false;
        this.timerAvailable = false;
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
                this.collectButtonImage.color = Color.green;
                this.craftInfoText.text = $"Tier: {this.componentTier} Level: {this.componentLevel} {Ext.ComponentType(this.componentType)}";
                break;
            }
        }
    }
    public void CollectCraft()
    {
        if (this.craftComplete)
        {
            Debug.Log($"Tier: {this.componentTier} Level: {this.componentLevel} {Ext.ComponentType(this.componentType)} added to inventory!");
            Inventory.Instance.AddComponent(this.componentType, this.componentTier, this.componentLevel, 1);
            ResetTimer();
            this.timerAvailable = true;
        }
        else
        {
            Debug.Log("Craft is not complete");
        }
    }
    public void CompleteNow()
    {
        Debug.Log("You must pay $300 :)");
    }
    public void ResetTimer()
    {
        this.craftTime = 0;
        this.timeRemaining = 0;
        this.componentType = 0;
        this.componentTier = 0;
        this.componentLevel = 0;
        this.craftInfoText.text = "Craft Info";
        this.timeRemainingText.text = "Timer Remaining: 00:00:00";
        this.collectButtonImage.color = Color.white;
    }
}