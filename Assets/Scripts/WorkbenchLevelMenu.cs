using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WorkbenchLevelMenu : MonoBehaviour
{
    public int selectedTier;
    public int selectedLevel;
    public int selectedComponent = 1;
    public int ScrapCost
    {
        get
        {
            return (selectedTier * 10) + (selectedLevel * 5);
        }
    }
    public List<Image> componentButtonImages;
    public List<Image> tierButtonImages;
    public List<Image> levelButtonImages;
    public List<TextMeshProUGUI> componentAmountTexts;
    public TextMeshProUGUI selectedComponentText;
    public TextMeshProUGUI scrapCostText;
    public TextMeshProUGUI craftResultText;
    public TextMeshProUGUI scrapText;

    private void OnEnable()
    {
        this.SelectComponent(1);
    }
    public void SelectComponent(int index)
    {
        foreach (var img in componentButtonImages)
        {
            img.color = Color.white;
        }
        componentButtonImages[index - 1].color = Color.green;

        this.selectedComponent = index;

        UpdateText();
    }
    public void SelectTier(int index)
    {
        foreach (var img in tierButtonImages)
        {
            img.color = Color.white;
        }
        tierButtonImages[index - 1].color = Color.green;

        this.selectedTier = index;
        UpdateText();
    }
    public void SelectLevel(int level)
    {
        foreach (var img in levelButtonImages)
        {
            img.color = Color.white;
        }
        levelButtonImages[level - 1].color = Color.green;

        this.selectedLevel = level;

        UpdateText();
    }
    public void UpdateText()
    {
        if (this.selectedTier != 0 && this.selectedComponent != 0)
        {
            string componentString = "";
            switch (selectedComponent)
            {
                case 1:
                    //cpu
                    for (int i = 0; i < this.componentAmountTexts.Count; i++)
                    {
                        componentString = "CPU";
                        componentAmountTexts[i].text = $"x {Inventory.Instance.cpuInventory[this.selectedTier - 1, i].ToString()}";
                    }
                    break;
                case 2:
                    //gpu
                    for (int i = 0; i < this.componentAmountTexts.Count; i++)
                    {
                        componentString = "GPU";
                        componentAmountTexts[i].text = $"x {Inventory.Instance.gpuInventory[this.selectedTier - 1, i].ToString()}";
                    }
                    break;
                case 3:
                    //ram
                    for (int i = 0; i < this.componentAmountTexts.Count; i++)
                    {
                        componentString = "RAM";
                        componentAmountTexts[i].text = $"x {Inventory.Instance.ramInventory[this.selectedTier - 1, i].ToString()}";
                    }
                    break;
                case 4:
                    //hdd
                    for (int i = 0; i < this.componentAmountTexts.Count; i++)
                    {
                        componentString = "HDD";
                        componentAmountTexts[i].text = $"x {Inventory.Instance.hddInventory[this.selectedTier - 1, i].ToString()}";
                    }
                    break;
                default:
                    break;
            }
            selectedComponentText.text = "Tier: " + selectedTier + "\nLevel: " + selectedLevel + "\n" + componentString;
            scrapCostText.text = $"{ScrapCost} Scrap";
            craftResultText.text = "Tier: " + selectedTier + "\nLevel: " + (selectedLevel + 1) + "\n" + componentString;
        }
    }
    public void Craft()
    {
        if (selectedLevel != 0 && selectedTier != 0 && selectedComponent != 0)
        {
            if (selectedLevel < 10)
            {
                if (Inventory.Instance.Scrap >= ScrapCost)
                {
                    //subtract scrap cost
                    //update scrap text
                    //remove selected component from inventory
                    //add new component of high level to inventory
                    int tier = this.selectedTier;
                    int level = this.selectedLevel;
                    int type = this.selectedComponent;

                    switch (type)
                    {
                        case 1:
                            if (Inventory.Instance.cpuInventory[tier - 1, level - 1] >= 1)
                            {
                                Inventory.Instance.cpuInventory[tier - 1, level - 1]--;
                                Inventory.Instance.cpuInventory[tier - 1, level]++;
                                Inventory.Instance.Scrap -= ScrapCost;
                                scrapText.text = $"Scrap: {Inventory.Instance.Scrap}";
                            }
                            else
                            {
                                Debug.Log("You dont have enough of that component");
                            }
                            break;
                        case 2:
                            if (Inventory.Instance.gpuInventory[tier - 1, level - 1] >= 1)
                            {
                                Inventory.Instance.gpuInventory[tier - 1, level - 1]--;
                                Inventory.Instance.gpuInventory[tier - 1, level]++;
                                Inventory.Instance.Scrap -= ScrapCost;
                                scrapText.text = $"Scrap: {Inventory.Instance.Scrap}";
                            }
                            else
                            {
                                Debug.Log("You dont have enough of that component");
                            }
                            break;
                        case 3:
                            if (Inventory.Instance.ramInventory[tier - 1, level - 1] >= 1)
                            {
                                Inventory.Instance.ramInventory[tier - 1, level - 1]--;
                                Inventory.Instance.ramInventory[tier - 1, level]++;
                                Inventory.Instance.Scrap -= ScrapCost;
                                scrapText.text = $"Scrap: {Inventory.Instance.Scrap}";
                            }
                            else
                            {
                                Debug.Log("You dont have enough of that component");
                            }
                            break;
                        case 4:
                            if (Inventory.Instance.hddInventory[tier - 1, level - 1] >= 1)
                            {
                                Inventory.Instance.hddInventory[tier - 1, level - 1]--;
                                Inventory.Instance.hddInventory[tier - 1, level]++;
                                Inventory.Instance.Scrap -= ScrapCost;
                                scrapText.text = $"Scrap: {Inventory.Instance.Scrap}";
                            }
                            else
                            {
                                Debug.Log("You dont have enough of that component");
                            }
                            break;
                        default:
                            break;
                    }
                    UpdateText();
                }
                else
                {
                    Debug.Log("Not enough scrap, loser!");
                }
            }
            else
            {
                Debug.Log("Can't go past level 10");
            }
        }
        else
        {
            Debug.Log("Select a component");
        }
    }
}
