using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CraftingSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeRemainingText;
    [SerializeField] private TextMeshProUGUI craftInfoText;
    [SerializeField] private Image collectButtonImage;
    public Button CancelButton;
    public Timer CraftTimer;
    public Timer GraceTimer;
    private int _timeRemaining;
    private int _componentType;
    private int _componentTier;
    private int _componentLevel;
    private bool _craftComplete;
    private bool _timerAvailable = true;
    private int[,] craftingComponents;
    private int _scrapCost;
    public bool TimerAvailable
    {
        get
        {
            return this._timerAvailable;
        }
    }

    private void Awake()
    {
        CraftTimer.OnTimerStarted.AddListener(UpdateTimeRemainingText);
        CraftTimer.OnTimerCountdown.AddListener(UpdateTimeRemainingText);
        CraftTimer.OnTimerComplete.AddListener(CraftCompleted);
    }
    public void StartCraft(int durationInSeconds, int tier, int level, int type, int scrapCost, int[,] craftingComponents)
    {
        this._componentType = type;
        this._componentTier = tier;
        this._componentLevel = level;
        this._scrapCost = scrapCost;
        this.craftingComponents = craftingComponents;
        this.craftInfoText.text = $"Tier: {this._componentTier} {Ext.ComponentType(this._componentType)}";

        CraftTimer.StartTimer(durationInSeconds);
        GraceTimer.StartTimer(10);
    }
    public void CollectCraft()
    {
        if (this.CraftTimer.IsComplete)
        {
            Debug.Log($"Tier: {this._componentTier} Level: {this._componentLevel} {Ext.ComponentType(this._componentType)} added to inventory!");
            Inventory.Instance.AddComponent(this._componentType, this._componentTier, this._componentLevel, 1);
            this.CraftTimer.ResetTimer();
            this.craftInfoText.text = "Craft Info";
            this.collectButtonImage.color = Color.white;
        }
        else
        {
            Debug.Log("Craft is not complete");
        }
    }
    public void CraftCompleted()
    {
        this.craftInfoText.text = $"Tier: {this._componentTier} Level: {this._componentLevel} {Ext.ComponentType(this._componentType)}";
        this.collectButtonImage.color = Color.green;
    }
    public void CompleteNow()
    {
        Debug.Log("You must pay $300 :)");
    }
    public void UpdateTimeRemainingText()
    {
        TimeSpan time = TimeSpan.FromSeconds(CraftTimer.TimeRemaining);
        timeRemainingText.text = "Time Remaining: " + time.ToString(@"hh\:mm\:ss");
    }
}