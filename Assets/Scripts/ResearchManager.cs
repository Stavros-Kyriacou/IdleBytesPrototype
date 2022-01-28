using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResearchManager : MonoBehaviour
{
    public List<ResearchTimer> ResearchTimers;
    public RectTransform ResearchPopupMenu;
    public float offset;
    public Vector2 Offscreen
    {
        get
        {
            return Vector2.up * offset;
        }
    }
    [Header("Research Popup Menu")]
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI ResearchCostText;
    public TextMeshProUGUI CompleteNowCostText;
    public Button StartResearchButton;
    public Button CompleteNowButton;


    private void Awake()
    {

    }
    public void ShowResearchPopup(bool visible)
    {
        if (visible)
        {
            ResearchPopupMenu.anchoredPosition = Vector2.zero;
        }
        else
        {
            ResearchPopupMenu.anchoredPosition = Offscreen;
        }
    }
    public void UpdateResearchPopup(Research selectedResearch)
    {
        if (selectedResearch != null)
        {
            TitleText.text = selectedResearch.Title;
            LevelText.text = $"Level: {selectedResearch.CurrentLevel}/{selectedResearch.MaxLevel}";
            DescriptionText.text = selectedResearch.Description;
            ResearchCostText.text = $"Cost: {selectedResearch.ScrapCost} Scrap";
            CompleteNowCostText.text = $"Cost: {selectedResearch.GemCost} Gems";

            StartResearchButton.onClick.RemoveAllListeners();
            ResearchTimers[0].CurrentResearch = selectedResearch;
            StartResearchButton.onClick.AddListener(ResearchTimers[0].StartResearch);
        }
    }
}