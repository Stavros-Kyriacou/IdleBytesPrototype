using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Research : MonoBehaviour, IPointerDownHandler
{
    public string title;
    public string description;
    public int maxLevel;
    [HideInInspector] public int currentLevel;
    [SerializeField] private List<Research> researchRequirements;
    [SerializeField] private Text titleText;
    public double timeToComplete;
    public double secondsRemaining;
    public DateTime timeStarted;
    public int scrapCost;
    public bool isUnlocked = false;
    // public DateTime currentTime;
    private void Awake()
    {
        currentLevel = 0;
        // timeStarted = new DateTime(2021, 10, 16, 13, 33, 0);
        // currentTime = new DateTime(2021, 10, 5, 17, 20, 0);
        // var secondsRemaining = (currentTime - timeStarted).TotalSeconds;
    }
    private void Start()
    {
        titleText.text = title;
        if (researchRequirements.Count > 0)
        {
            description += "\nRequirements:\n";
            for (int i = 0; i < researchRequirements.Count; i++)
            {
                description += researchRequirements[i].title + "\n";
            }
        }
    }
    public void CalculateTimeRemaining()
    {
        if (this.timeStarted == DateTime.MinValue)
        {
            Debug.Log("time started is null");
            this.secondsRemaining = this.timeToComplete;
        }
        else
        {
            this.secondsRemaining = (WorldTimeAPI.Instance.GetCurrentDateTime() - timeStarted).TotalSeconds;
        }
        Debug.Log("Seconds remaining: " + secondsRemaining);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CalculateTimeRemaining();
        ResearchTree.Instance.SelectResearch(this);
    }
    public void StartResearch()
    {
        List<Research> lockedResearch = new List<Research>();

        if (researchRequirements.Count > 0)
        {
            foreach (var r in researchRequirements)
            {
                if (!r.isUnlocked)
                {
                    lockedResearch.Add(r);
                }
            }
        }
        if (lockedResearch.Count > 0)
        {
            Debug.Log("Research cannot be unlocked, requirements not met");
            foreach (var r in lockedResearch)
            {
                Debug.Log(r.title + " still locked");
            }
        }
        else
        {
            if (ResearchTree.Instance.scrap >= this.scrapCost && this.currentLevel < this.maxLevel)
            {
                ResearchTree.Instance.scrap -= this.scrapCost;
                this.timeStarted = WorldTimeAPI.Instance.GetCurrentDateTime();
                Debug.Log("Time started: " + this.timeStarted);
            }
            else if (this.currentLevel >= this.maxLevel)
            {
                Debug.Log("Research is max level");
            }
            else
            {
                Debug.Log("Not enough scrap available");
            }
        }
    }
    public void CompleteResearch()
    {
        this.isUnlocked = true;
        this.currentLevel++;
        ResearchTree.Instance.UpdateDescriptionBox();
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
