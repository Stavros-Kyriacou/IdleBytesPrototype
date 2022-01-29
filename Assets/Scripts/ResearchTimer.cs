using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

public class ResearchTimer : MonoBehaviour
{
    public Timer Timer;
    public ProgressBar ProgressBar;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI TimeRemainingText;
    public Button CompleteNowButton;
    public Research CurrentResearch;
    public void StartResearch(Research selectedResearch)
    {
        this.CurrentResearch = selectedResearch;
        
        TitleText.text = CurrentResearch.Title;

        TimeSpan time = TimeSpan.FromSeconds(CurrentResearch.DurationInSeconds);
        TimeRemainingText.text = time.ToString(@"hh\:mm\:ss");
        ProgressBar.GetCurrentFill(0, CurrentResearch.DurationInSeconds);

        Timer.StartTimer(CurrentResearch.DurationInSeconds);
        Timer.OnTimerComplete.AddListener(CompleteResearch);
    }
    public void UpdateTimeRemaining()
    {
        ProgressBar.GetCurrentFill(CurrentResearch.DurationInSeconds - Timer.TimeRemaining, CurrentResearch.DurationInSeconds);
        TimeSpan time = TimeSpan.FromSeconds(Timer.TimeRemaining);
        TimeRemainingText.text = time.ToString(@"hh\:mm\:ss");
    }
    public void CompleteResearch()
    {
        //increase the level of current research
        CurrentResearch.IncreaseLevel();
        ResetResearchTimer();
    }
    public void ResetResearchTimer()
    {
        Timer.ResetTimer();
        TitleText.text = "";
        TimeRemainingText.text = "00:00:00";
        ProgressBar.GetCurrentFill(0, 1);
        CurrentResearch = null;
        Timer.OnTimerComplete.RemoveAllListeners();
        //timer.ResetTimer
        //reset title
        //reset time remaining text
        //reset progressbar
        //current research = null
    }

}
