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
    [Header("Research Details")]
    public string Title;
    public string Description;
    public int MaxLevel;
    [HideInInspector] public int CurrentLevel;
    public int DurationInSeconds;
    public int ScrapCost;
    public int GemCost;
    [Header("Research Requirements")]
    [Tooltip("The level this research must be if it is a prerequisite for other researches")]
    public int MinLevel;
    public List<Research> ResearchRequirements;

    [Header("References")]
    public ResearchManager ResearchManager;
    public ProgressBar ProgressBar;
    public Button MainButton;

    [Header("Text Fields")]
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI LevelText;
    private void OnEnable()
    {
        this.MainButton?.onClick.AddListener(() => ResearchManager.ShowResearchPopup(true));
        this.MainButton?.onClick.AddListener(() => ResearchManager.UpdateResearchPopup(this));
    }
    private void OnDisable()
    {
        this.MainButton?.onClick.RemoveAllListeners();
    }
    private void Awake()
    {
        this.CurrentLevel = 0;
        if (this.MinLevel > this.MaxLevel)
        {
            this.MinLevel = this.MaxLevel;
        }
        this.TitleText.text = Title;
        this.LevelText.text = $"{this.CurrentLevel}/{this.MaxLevel}";
        this.ProgressBar.GetCurrentFill(this.CurrentLevel, this.MaxLevel);
    }
    public void IncreaseLevel()
    {
        if (this.CurrentLevel < this.MaxLevel)
        {
            this.CurrentLevel++;
            UpdateLevelProgressBar();
        }
    }
    public void UpdateLevelProgressBar()
    {
        this.LevelText.text = $"{this.CurrentLevel}/{this.MaxLevel}";
        this.ProgressBar.GetCurrentFill(this.CurrentLevel, this.MaxLevel);
    }
    public bool RequirementsComplete()
    {
        foreach (var req in ResearchRequirements)
        {
            if (req.CurrentLevel < req.MinLevel)
            {
                return false;
            }
        }
        return true;
    }
}
