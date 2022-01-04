using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootBoxManager : MonoBehaviour
{
    public int selectedTier = 1;
    public List<TextMeshProUGUI> rewardTexts;
    public List<TextMeshProUGUI> amountTexts;
    public TextMeshProUGUI statusText;

    //Loot Box Weights
    private int[] rewardWeights = { 10, 10, 10, 10 };
    private int[] numRewards = { 2, 3, 4, 5, 6 };
    private int[] componentWeights = { 10, 10, 10, 10 };
    private int[] tier1ComponentWeights = { 300, 5, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] tier2ComponentWeights = { 300, 100, 20, 5, 0, 0, 0, 0, 0, 0 };
    private int[] tier3ComponentWeights = { 500, 400, 300, 80, 30, 0, 0, 0, 0, 0 };
    private int[] tier4ComponentWeights = { 0, 0, 300, 300, 300, 300, 300, 15, 0, 0 };
    private int[] tier5ComponentWeights = { 0, 0, 400, 400, 350, 300, 300, 15, 1, 0 };
    private void OnEnable()
    {
        UpdateText();
    }
    public void SelectTier(int tier)
    {
        this.selectedTier = tier;
        statusText.text = $"Opening Tier {selectedTier} Box";
    }
    public void OpenBox()
    {
        //check if amount is > 0
        //reduce amount of boxes

        if (Inventory.Instance.lootBoxes[selectedTier - 1] > 0)
        {
            for (int i = 0; i < numRewards[selectedTier - 1]; i++)
            {
                int rewardType = RollWeights(rewardWeights);
                switch (rewardType)
                {
                    case 0:
                        rewardTexts[i].text = $"{RollScrap()} scrap";
                        break;
                    case 1:
                        rewardTexts[i].text = $"{RollGems()} gems";
                        break;
                    case 2:
                        rewardTexts[i].text = RollComponent();
                        break;
                    case 3:
                        rewardTexts[i].text = "Some type of boost...";
                        break;
                    default:
                        break;
                }
            }
            Inventory.Instance.lootBoxes[selectedTier - 1]--;
            UpdateText();
        }
        else
        {
            Debug.Log("You are out of boxes, buy some more pls :)");
        }
    }
    public void UpdateText()
    {
        for (int i = 0; i < amountTexts.Count; i++)
        {
            amountTexts[i].text = $"x {Inventory.Instance.lootBoxes[i]}";
        }
        statusText.text = $"Opening Tier {selectedTier} Box";
    }

    public int RollWeights(int[] weights)
    {
        //Takes an array of item weights and makes a roll on the loot table
        //Returns the index of the item rolled in the array
        int total = 0;
        int index = 0;

        foreach (var weight in weights)
        {
            total += weight;
        }

        int roll = Random.Range(0, total + 1);

        for (int i = 0; i < weights.Length; i++)
        {
            if (roll <= weights[i])
            {
                index = i;
                break;
            }
            else
            {
                roll -= weights[i];
            }
        }
        return index;
    }
    public string RollComponent()
    {
        int componentType = RollWeights(componentWeights);
        int componentTier = 0;

        switch (selectedTier)
        {
            case 1:
                componentTier = RollWeights(tier1ComponentWeights);
                break;
            case 2:
                componentTier = RollWeights(tier2ComponentWeights);
                break;
            case 3:
                componentTier = RollWeights(tier3ComponentWeights);
                break;
            case 4:
                componentTier = RollWeights(tier4ComponentWeights);
                break;
            case 5:
                componentTier = RollWeights(tier5ComponentWeights);
                break;
            default:
                break;
        }
        componentTier += 1;
        string rewardString = "";
        switch (componentType)
        {
            case 0:
                rewardString = $"You rolled a tier {componentTier} CPU";
                // Debug.Log("CPU amount before adding: " + Inventory.Instance.cpuInventory[componentTier - 1, 0]);
                Inventory.Instance.cpuInventory[componentTier - 1, 0]++;
                // Debug.Log("CPU amount after adding: " + Inventory.Instance.cpuInventory[componentTier - 1, 0]);
                break;
            case 1:
                rewardString = $"You rolled a tier {componentTier} GPU";
                Inventory.Instance.gpuInventory[componentTier - 1, 0]++;
                break;
            case 2:
                rewardString = $"You rolled a tier {componentTier} RAM";
                Inventory.Instance.ramInventory[componentTier - 1, 0]++;
                break;
            case 3:
                rewardString = $"You rolled a tier {componentTier} HDD";
                Inventory.Instance.hddInventory[componentTier - 1, 0]++;
                break;
            default:
                break;
        }
        Inventory.Instance.UpdateText();
        return rewardString;
    }
    public int RollScrap()
    {
        int scrap = 0;
        switch (selectedTier)
        {
            case 1:
                scrap = Random.Range(5, 21);
                break;
            case 2:
                scrap = Random.Range(25, 41);
                break;
            case 3:
                scrap = Random.Range(45, 61);
                break;
            case 4:
                scrap = Random.Range(65, 81);
                break;
            case 5:
                scrap = Random.Range(85, 101);
                break;
            default:
                break;
        }
        return scrap;
    }
    public int RollGems()
    {
        int gems = 0;
        switch (selectedTier)
        {
            case 1:
                gems = Random.Range(5, 21);
                break;
            case 2:
                gems = Random.Range(25, 41);
                break;
            case 3:
                gems = Random.Range(45, 61);
                break;
            case 4:
                gems = Random.Range(65, 81);
                break;
            case 5:
                gems = Random.Range(85, 101);
                break;
            default:
                break;
        }
        return gems;
    }
}
