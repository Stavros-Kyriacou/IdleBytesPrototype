using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WorkbenchDeconstructionMenu : MonoBehaviour
{
    public int selectedTier;
    public int selectedLevel;
    public int selectedComponent = 1;
    public List<Image> componentButtonImages;
    public List<Image> tierButtonImages;
    public List<Image> levelButtonImages;
    public List<TextMeshProUGUI> componentAmountTexts;
    public Slider slider;
    public TextMeshProUGUI sliderValueText;
    public List<TextMeshProUGUI> deconstructionTexts;
    public int[,] components = new int[5, 4]; //columns: type, tier, level, amount
    public TextMeshProUGUI scrapAmountText;

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
        UpdateSliderValue();
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
            switch (selectedComponent)
            {
                case 1:
                    //cpu
                    for (int i = 0; i < this.componentAmountTexts.Count; i++)
                    {
                        componentAmountTexts[i].text = $"x {Inventory.Instance.cpuInventory[this.selectedTier - 1, i].ToString()}";
                    }
                    break;
                case 2:
                    //gpu
                    for (int i = 0; i < this.componentAmountTexts.Count; i++)
                    {
                        componentAmountTexts[i].text = $"x {Inventory.Instance.gpuInventory[this.selectedTier - 1, i].ToString()}";
                    }
                    break;
                case 3:
                    //ram
                    for (int i = 0; i < this.componentAmountTexts.Count; i++)
                    {
                        componentAmountTexts[i].text = $"x {Inventory.Instance.ramInventory[this.selectedTier - 1, i].ToString()}";
                    }
                    break;
                case 4:
                    //hdd
                    for (int i = 0; i < this.componentAmountTexts.Count; i++)
                    {
                        componentAmountTexts[i].text = $"x {Inventory.Instance.hddInventory[this.selectedTier - 1, i].ToString()}";
                    }
                    break;
                default:
                    break;
            }
        }

        for (int i = 0; i < components.GetLength(0); i++)
        {
            if (components[i, 0] == 0)
            {
                deconstructionTexts[i].text = "";
            }
            else
            {
                //columns: type, tier, level, amount
                deconstructionTexts[i].text = $"Tier: {components[i, 1]} Level: {components[i, 2]} {Ext.ComponentType(components[i, 0])} x {components[i, 3]}";
            }
        }
        scrapAmountText.text = $"Scrap: {CalculateScrapAmount()}";
        UpdateSliderValue();
    }
    public void AddToList()
    {
        //add data to the components array
        //search for the first empty spot in array

        //check if a component, tier, level is selected and amount > 0
        if (selectedComponent != 0 && selectedTier != 0 && selectedLevel != 0 && slider.value > 0)
        {
            int emptyIndex = -1;

            for (int i = 0; i < components.GetLength(0); i++)
            {
                if (components[i, 0] == 0)
                {
                    emptyIndex = i;
                    break;
                }
            }

            if (emptyIndex != -1)
            {
                //can be placed in the array

                //remove components from inventory
                if (Inventory.Instance.RemoveComponent(selectedComponent, selectedTier, selectedLevel, (int)slider.value))
                {
                    //columns: type, tier, level, amount
                    components[emptyIndex, 0] = selectedComponent;
                    components[emptyIndex, 1] = selectedTier;
                    components[emptyIndex, 2] = selectedLevel;
                    components[emptyIndex, 3] = (int)slider.value;
                }
                else
                {
                    Debug.Log("Not enough components in inventory");
                }
            }
            else
            {
                Debug.Log("List if full, either deconstruct or reset list");
            }
        }
        else
        {
            Debug.Log("Select a component, tier, level and amount");
        }
        UpdateText();
    }
    public void ResetList()
    {
        foreach (var t in deconstructionTexts)
        {
            t.text = "";
        }
        components = new int[5, 4];
    }
    public void Deconstruct()
    {
        Inventory.Instance.scrap += CalculateScrapAmount();
        ResetList();
        UpdateText();
    }
    public int CalculateScrapAmount()
    {
        int total = 0;

        for (int i = 0; i < components.GetLength(0); i++)
        {
            //columns: type, tier, level, amount
            int tier = components[i, 1];
            int level = components[i, 2];
            int amount = components[i, 3];

            total += tier * level * amount;
        }
        return total;
    }
    public void UpdateSliderValue()
    {
        if (selectedLevel != 0 && selectedTier != 0)
        {
            switch (selectedComponent)
            {
                case 1:
                    slider.maxValue = Inventory.Instance.cpuInventory[selectedTier - 1, selectedLevel - 1];
                    break;
                case 2:
                    slider.maxValue = Inventory.Instance.gpuInventory[selectedTier - 1, selectedLevel - 1];
                    break;
                case 3:
                    slider.maxValue = Inventory.Instance.ramInventory[selectedTier - 1, selectedLevel - 1];
                    break;
                case 4:
                    slider.maxValue = Inventory.Instance.hddInventory[selectedTier - 1, selectedLevel - 1];
                    break;
                default:
                    break;
            }
        }
    }
    public void OnSliderChanged()
    {
        sliderValueText.text = slider.value.ToString();
    }
}