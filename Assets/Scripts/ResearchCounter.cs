using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchCounter : MonoBehaviour
{
    public Research selectedResearch;
    public Text title;
    public Text timeRemaining;
    private int hours;
    private int minutes;
    private int seconds;

    private void Update()
    {
        if (selectedResearch == null)
        {
            return;
        }
        selectedResearch.secondsRemaining -= Time.deltaTime;
        UpdateText();

        if (selectedResearch.secondsRemaining <= 0)
        {
            selectedResearch.CompleteResearch();
            Debug.Log(selectedResearch + " completed");
            selectedResearch.timeStarted = DateTime.MinValue;
            selectedResearch = null;
        }
        
    }
    private void UpdateText()
    {
        hours = TimeSpan.FromSeconds(selectedResearch.secondsRemaining).Hours;
        minutes = TimeSpan.FromSeconds(selectedResearch.secondsRemaining).Minutes;
        seconds = TimeSpan.FromSeconds(selectedResearch.secondsRemaining).Seconds;

        timeRemaining.text = $"{this.hours}:{this.minutes}:{this.seconds}";
    }
}
