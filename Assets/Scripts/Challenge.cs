using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Challenge : MonoBehaviour
{
    public string description;
    private string progress;
    public Text descriptionText;
    public Text progressText;
    public Text rewardText;
    public int maxSteps;
    private int currentStep;
    public Reward reward;
    public ProgressBar progressBar;
    public Image buttonImage;
    public Color completedColour;
    public Color collectedColour;
    public bool rewardCollected = false;
    private void Start()
    {
        SetRewardText();
        descriptionText.text = description;
        UpdateText();
    }
    public void IncreaseProgress()
    {
        if (currentStep < maxSteps)
        {
            currentStep++;
        }

        Debug.Log("increase progress");
        progressBar.GetCurrentFill(currentStep, maxSteps);
        UpdateText();
        if (CheckCompleted() && !rewardCollected)
        {
            ChangeButtonColour(completedColour);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseProgress();
        }
    }
    public void CollectReward()
    {
        if (currentStep == maxSteps && !rewardCollected)
        {
            Debug.Log("Reward Collected!");
            rewardCollected = true;
            ChangeButtonColour(collectedColour);
        }
        else if (currentStep < maxSteps)
        {
            Debug.Log("Challenge not complete!");
        }
        else if (rewardCollected)
        {
            Debug.Log("Reward has already been collected!");
        }
    }
    public void UpdateText()
    {
        progressText.text = $"Progress: {currentStep}/{maxSteps}";
    }
    public void SetRewardText()
    {
        switch (reward)
        {
            case Reward.TwentyScrap:
                rewardText.text = "20 Scrap";
                break;
            case Reward.CommonLootbox:
                rewardText.text = "Common Lootbox";
                break;
            case Reward.UncommonLootbox:
                rewardText.text = "Uncommon Lootbox";
                break;
            case Reward.RareLootbox:
                rewardText.text = "Rare Lootbox";
                break;
            default:
                rewardText.text = "Reward Not Set";
                break;
        }

    }
    public bool CheckCompleted()
    {
        return currentStep == maxSteps ? true : false;
    }
    public void ChangeButtonColour(Color color)
    {
        buttonImage.color = color;
    }
}

