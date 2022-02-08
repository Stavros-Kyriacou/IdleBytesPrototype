using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResearchManager : MonoBehaviour
{
    private Research SelectedResearch;
    public List<Research> ResearchList;
    public float offset;
    public Vector2 Offscreen
    {
        get
        {
            return Vector2.up * offset;
        }
    }

    [Header("Research Timers")]
    public List<ResearchTimer> ResearchTimers;
    public int TimerCost;
    public TextMeshProUGUI TimerCostText;
    private bool SecondTimerLocked = true;
    public Button UnlockTimerButton;

    [Header("Research Popup Menu")]
    public RectTransform ResearchPopupMenu;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI ResearchCostText;
    public TextMeshProUGUI CompleteNowCostText;
    public Button StartResearchButton;
    public Button CompleteNowButton;
    public List<TextMeshProUGUI> RequirementsTexts;

    private void Awake()
    {
        //set references on all researches
        foreach (var r in ResearchList)
        {
            r.ResearchManager = this;
        }

        //disable second research timer
        this.ResearchTimers[1].gameObject.SetActive(false);
        this.TimerCostText.text = $"Cost: {TimerCost} Gems";
    }
    private void Start()
    {
        UpdateConnectionLines();
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
            //Update text fields to match the selected Research details
            TitleText.text = selectedResearch.Title;
            LevelText.text = $"Level: {selectedResearch.CurrentLevel}/{selectedResearch.MaxLevel}";
            DescriptionText.text = selectedResearch.Description;
            ResearchCostText.text = $"Cost: {selectedResearch.ScrapCost} Scrap";
            CompleteNowCostText.text = $"Cost: {selectedResearch.InstantCompleteCost} Gems";

            //Clear all requirements text fields
            for (int i = 0; i < RequirementsTexts.Count; i++)
            {
                RequirementsTexts[i].text = string.Empty;
            }

            //Update requirements texts fields if there are any
            if (selectedResearch.ResearchRequirements.Count > 0)
            {
                for (int i = 0; i < selectedResearch.ResearchRequirements.Count; i++)
                {
                    RequirementsTexts[i].text = $"{selectedResearch.ResearchRequirements[i].Title}: Lvl {selectedResearch.ResearchRequirements[i].MinLevel}";
                }
            }

            this.SelectedResearch = selectedResearch;
        }
    }
    public void StartResearch()
    {
        var availableTimer = GetAvailableResearchTimer();
        if (availableTimer != null)
        {
            if (SelectedResearch.RequirementsComplete())
            {
                if (!IsAlreadyResearching())
                {
                    if (SelectedResearch.CurrentLevel < SelectedResearch.MaxLevel)
                    {
                        if (Inventory.Instance.RemoveScrap(SelectedResearch.ScrapCost))
                        {
                            availableTimer.StartResearch(this.SelectedResearch);
                            ShowResearchPopup(false);
                        }
                        else
                        {
                            Debug.Log("Not enough scrap");
                        }
                    }
                    else
                    {
                        Debug.Log("Research already at max level");
                    }
                }
                else
                {
                    Debug.Log("That research is already being researched");
                }
            }
            else
            {
                Debug.Log("Research Requirements not met");
            }
        }
        else
        {
            Debug.Log("No timer available");
        }
    }
    public void CompleteResearchInstantly()
    {
        if (SelectedResearch.RequirementsComplete())
        {
            if (SelectedResearch.CurrentLevel < SelectedResearch.MaxLevel)
            {
                if (Inventory.Instance.RemoveGems(SelectedResearch.InstantCompleteCost))
                {
                    this.SelectedResearch.IncreaseLevel();
                    ShowResearchPopup(false);
                }
                else
                {
                    Debug.Log("Not enough gems");
                }
            }
            else
            {
                Debug.Log("Research already at max level");
            }
        }
        else
        {
            Debug.Log("Research Requirements not met");
        }
    }
    public ResearchTimer GetAvailableResearchTimer()
    {
        for (int i = 0; i < ResearchTimers.Count; i++)
        {
            if (ResearchTimers[i].Timer.IsAvailable)
            {
                if (ResearchTimers[i] == ResearchTimers[1])
                {
                    if (this.SecondTimerLocked)
                    {
                        return null;
                    }
                }
                return ResearchTimers[i];
            }
        }
        return null;
    }
    public void UnlockTimer()
    {
        if (this.SecondTimerLocked)
        {
            if (Inventory.Instance.RemoveGems(this.TimerCost))
            {
                this.ResearchTimers[1].gameObject.SetActive(true);
                this.SecondTimerLocked = false;
                this.UnlockTimerButton.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Second timer already purchased :)");
        }
    }
    public bool IsAlreadyResearching()
    {
        if (ResearchTimers[0].CurrentResearch == SelectedResearch || ResearchTimers[1].CurrentResearch == SelectedResearch)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void UpdateConnectionLines()
    {
        for (int i = 0; i < ResearchList.Count; i++)
        {
            if (ResearchList[i].RequirementsComplete())
            {
                if (ResearchList[i].ConnectionLine != null)
                {
                    var temp = ResearchList[i].ConnectionLine.ConnectionLines[ResearchList[i].ConnectionLine.ConnectionTypeIndex];
                    if (temp != null)
                    {
                        foreach (RectTransform child in temp)
                        {
                            var img = child.GetComponent<Image>();
                            img.color = Color.green;
                        }
                    }
                }
            }
        }
    }
}