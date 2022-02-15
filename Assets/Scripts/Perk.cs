using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Perk : MonoBehaviour
{
    [Header("Perk Details")]
    public string Title;
    public string Description;
    public int MaxLevel;
    public int Cost
    {
        get
        {
            return (currentLevel + 1) * 5;
        }
        private set { }
    }

    [Header("UI")]
    public Button PerkButton;
    public ProgressBar LevelProgressBar;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI LevelText;

    [Header("References")]
    public PerkMenu PerkMenu;

    //Local Variables
    [HideInInspector] public int currentLevel;

    public Perk()
    {
        this.currentLevel = 0;
    }
    private void OnEnable()
    {
        this.PerkButton?.onClick.AddListener(() => PerkMenu.TogglePerkPopup(true));
        this.PerkButton?.onClick.AddListener(() => PerkMenu.UpdatePerkPopupUI(this));
    }
    private void OnDisable()
    {
        this.PerkButton.onClick.RemoveAllListeners();
    }
    private void Start()
    {
        this.TitleText.text = this.Title;
        this.LevelText.text = $"{this.currentLevel}/{this.MaxLevel}";
    }
    public void IncreaseLevel()
    {
        if (this.currentLevel < this.MaxLevel)
        {
            this.currentLevel++;
            UpdateLevelProgressBar();
        }
    }
    public void UpdateLevelProgressBar()
    {
        this.LevelText.text = $"{this.currentLevel}/{this.MaxLevel}";
        this.LevelProgressBar.UpdateFill(this.currentLevel, this.MaxLevel);
    }
}
