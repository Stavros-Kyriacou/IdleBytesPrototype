using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

public class ResearchTimer : MonoBehaviour
{
    public Timer Timer;
    public ProgressBar ProgressBar;
    public TextMeshProUGUI TimeRemainingText;
    public Research CurrentResearch;
    private void Awake()
    {
        Timer.OnTimerCountdown.AddListener(() => ProgressBar.GetCurrentFill(CurrentResearch.DurationInSeconds - Timer.TimeRemaining, CurrentResearch.DurationInSeconds));
    }
    public void StartResearch()
    {
        if (CurrentResearch != null)
        {
            Timer.StartTimer(CurrentResearch.DurationInSeconds);
        }
    }
    public void UpdateTimeRemainingText()
    {
        TimeSpan time = TimeSpan.FromSeconds(Timer.TimeRemaining);
        TimeRemainingText.text = time.ToString(@"hh\:mm\:ss");
    }

}
