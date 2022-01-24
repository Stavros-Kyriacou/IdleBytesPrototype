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
    [HideInInspector] public CancelCraftMenu cancelCraftMenu;
    [HideInInspector] public WorkbenchManager workbenchManager;
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
            this.CancelButton.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Craft is not complete");
        }
    }
    public void CancelCraft()
    {
        if (this.GraceTimer.IsComplete)
        {
            //no components, just some scrap
            Inventory.Instance.Scrap += 500;
        }
        else
        {
            //add components back to inventory, add less scrap
            for (int i = 0; i < craftingComponents.GetLength(0); i++)
            {
                //Crafting ComponentsRows: Tier, Level, ComponentType
                Inventory.Instance.AddComponent(craftingComponents[i, 2], craftingComponents[i, 0], craftingComponents[i, 1], 1);
            }
            //maybe need to reset craftingCompoents[]
            Inventory.Instance.Scrap += 200;
        }
        workbenchManager.UpdateScrapText();
        cancelCraftMenu.ToggleMenu(false);
        ResetCraftSlot();
    }
    public void CraftCompleted()
    {
        this.craftInfoText.text = $"Tier: {this._componentTier} Level: {this._componentLevel} {Ext.ComponentType(this._componentType)}";
        this.collectButtonImage.color = Color.green;
        this.cancelCraftMenu.ToggleMenu(false);
        this.CancelButton.gameObject.SetActive(false);
    }
    public void ResetCraftSlot()
    {
        this.craftInfoText.text = "Craft Info";
        this.timeRemainingText.text = "Time Remaining: 00:00:00";
        this.CraftTimer.ResetTimer();
        this.GraceTimer.ResetTimer();
        this.collectButtonImage.color = Color.white;
        this.CancelButton.gameObject.SetActive(true);
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