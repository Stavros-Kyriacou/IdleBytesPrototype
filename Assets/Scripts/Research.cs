using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Research : MonoBehaviour
{
    public ResearchManager ResearchManager;
    public string Title;
    public string Description;
    public int MaxLevel;
    public int CurrentLevel;
    public List<Research> ResearchRequirements;
    public int DurationInSeconds;
    public int ScrapCost;
    public int GemCost;
    public ProgressBar ProgressBar;
    [Header("Text Fields")]
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI LevelText;
    private void Awake()
    {
        TitleText.text = Title;
        LevelText.text = $"{this.CurrentLevel}/{this.MaxLevel}";
        ProgressBar.GetCurrentFill(this.CurrentLevel, this.MaxLevel);

    }
    //a research takes a certain amount of time to complete
    //store the world API time that the research started at
    //when you launch the game, subtract the time started from the current time
    //check how long is remaining
    //  5/10/2021 17:12:50







    //reasearch takes 60 seconds to complete
    //start the research, log off after 30s
    //record time that you log off
    //save game when you log off

    //seconds remaining = 30s

    //log in. GetCurrenDateTime from api
    //calculate time since last login (CurrentTime - LastLogOff) in seconds

    //Log off at 12:00:00 (30s remaining)
    //Log in at 12:00:20
    //time since last log off = 20s

    //seconds remaining - time since last log off
}
