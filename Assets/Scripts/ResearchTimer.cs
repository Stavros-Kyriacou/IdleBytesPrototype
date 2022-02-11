using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

public class ResearchTimer : MonoBehaviour
{
    public Research CurrentResearch;
    public Timer Timer;
    public ProgressBar ProgressBar;
    public Button CompleteNowButton;

    [Header("UI Management")]
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI TimeRemainingText;
    public TextMeshProUGUI CompleteNowCostText;
    public void StartResearch(Research selectedResearch)
    {
        this.CurrentResearch = selectedResearch;

        TitleText.text = CurrentResearch.Title;

        TimeSpan time = TimeSpan.FromSeconds(CurrentResearch.DurationInSeconds);
        TimeRemainingText.text = time.ToString(@"hh\:mm\:ss");
        CompleteNowCostText.text = $"{10 * CurrentResearch.DurationInSeconds} Gems";
        ProgressBar.UpdateFill(0, CurrentResearch.DurationInSeconds);

        Timer.StartTimer(CurrentResearch.DurationInSeconds);
        Timer.OnTimerComplete.AddListener(CompleteResearch);
    }
    public void UpdateTimeRemaining()
    {
        ProgressBar.UpdateFill(CurrentResearch.DurationInSeconds - Timer.TimeRemaining, CurrentResearch.DurationInSeconds);
        TimeSpan time = TimeSpan.FromSeconds(Timer.TimeRemaining);
        TimeRemainingText.text = time.ToString(@"hh\:mm\:ss");
        CompleteNowCostText.text = $"{10 * this.Timer.TimeRemaining} Gems";
    }
    public void CompleteResearch()
    {
        if (this.CurrentResearch != null)
        {
            this.CurrentResearch.IncreaseLevel();
            ResetResearchTimer();
        }
        else
        {
            Debug.Log("A research has not been selected");
        }
    }
    public void CompleteResearchInstantly()
    {
        if (this.CurrentResearch != null)
        {

            if (!this.Timer.IsComplete)
            {
                if (Inventory.Instance.RemoveGems(10 * this.Timer.TimeRemaining))
                {
                    CompleteResearch();
                }
                else
                {
                    Debug.Log("Not enough gems");
                }
            }
            else
            {
                Debug.Log("Research is already complete");
            }
        }
        else
        {
            Debug.Log("A research has not been selected");
        }
    }
    public void ResetResearchTimer()
    {
        this.Timer.ResetTimer();
        this.TitleText.text = "";
        this.TimeRemainingText.text = "00:00:00";
        this.CompleteNowCostText.text = "0 Gems";
        this.ProgressBar.UpdateFill(0, 1);
        this.CurrentResearch = null;
        this.Timer.OnTimerComplete.RemoveAllListeners();
    }
}