using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WorkbenchDeconstructionMenu : MonoBehaviour
{
    public WorkbenchManager WorkbenchManager;
    public List<ComponentInfo> ComponentInfo = new List<ComponentInfo>();
    public List<TextMeshProUGUI> ListItemTexts;
    public Slider slider;
    public TextMeshProUGUI sliderValueText;
    public TextMeshProUGUI scrapAmountText;
    public int scrapAmount;
    public ComponentInventory ComponentInventory;

    [Header("Scroll View")]
    public Transform ContentContainer;
    public GameObject ListItemPrefab;

    public void AddToList()
    {
        //check if a component, tier, level is selected and amount > 0
        if (ComponentInventory.SelectedComponent != 0 && ComponentInventory.SelectedTier != 0 && ComponentInventory.SelectedLevel != 0 && slider.value > 0)
        {
            //remove components from inventory
            if (Inventory.Instance.RemoveComponent(ComponentInventory.SelectedComponent, ComponentInventory.SelectedTier, ComponentInventory.SelectedLevel, (int)slider.value))
            {
                PopulateScrollView(ComponentInventory.SelectedComponent, ComponentInventory.SelectedTier, ComponentInventory.SelectedLevel, (int)slider.value);

                scrapAmount += 10 * ComponentInventory.SelectedTier * ComponentInventory.SelectedLevel * (int)slider.value;
                scrapAmountText.text = $"Scrap: {scrapAmount}";
            }
            else
            {
                Debug.Log("Not enough components in inventory");
            }
        }
        else
        {
            Debug.Log("Select a component, tier, level and amount");
        }
        UpdateSliderValue();
    }
    public void PopulateScrollView(int type, int tier, int level, int amount)
    {
        var go = Instantiate(ListItemPrefab);
        go.transform.SetParent(ContentContainer);
        go.transform.localScale = Vector2.one;

        var textMesh = go.GetComponent<TextMeshProUGUI>();
        var newInfo = new ComponentInfo(type, tier, level, amount);
        ComponentInfo.Add(newInfo);
        textMesh.text = $"Tier: {newInfo.Tier} Level: {newInfo.Level} {Ext.ComponentType(newInfo.Type)} x {newInfo.Amount}";
    }
    public void ResetList()
    {
        for (int i = 0; i < ComponentInfo.Count; i++)
        {
            Inventory.Instance.AddComponent(ComponentInfo[i].Type, ComponentInfo[i].Tier, ComponentInfo[i].Level, ComponentInfo[i].Amount);
            GameObject.Destroy(ContentContainer.GetChild(i).gameObject);
        }

        ComponentInfo = new List<ComponentInfo>();

        ResetScrapAmount();
        ComponentInventory.UpdateText();
        UpdateSliderValue();
    }
    public void Deconstruct()
    {
        Inventory.Instance.Scrap += scrapAmount;
        WorkbenchManager.UpdateScrapText();

        for (int i = 0; i < ComponentInfo.Count; i++)
        {
            GameObject.Destroy(ContentContainer.GetChild(i).gameObject);
        }
        ComponentInfo = new List<ComponentInfo>();

        ResetScrapAmount();
        UpdateSliderValue();
    }
    public void ResetScrapAmount()
    {
        scrapAmount = 0;
        scrapAmountText.text = "Scrap: 0";
    }
    public void UpdateSliderValue()
    {
        if (ComponentInventory.SelectedLevel != 0 && ComponentInventory.SelectedTier != 0)
        {
            switch (ComponentInventory.SelectedComponent)
            {
                case 1:
                    slider.maxValue = Inventory.Instance.cpuInventory[ComponentInventory.SelectedTier - 1, ComponentInventory.SelectedLevel - 1];
                    break;
                case 2:
                    slider.maxValue = Inventory.Instance.gpuInventory[ComponentInventory.SelectedTier - 1, ComponentInventory.SelectedLevel - 1];
                    break;
                case 3:
                    slider.maxValue = Inventory.Instance.ramInventory[ComponentInventory.SelectedTier - 1, ComponentInventory.SelectedLevel - 1];
                    break;
                case 4:
                    slider.maxValue = Inventory.Instance.hddInventory[ComponentInventory.SelectedTier - 1, ComponentInventory.SelectedLevel - 1];
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
public class ComponentInfo
{
    public int Type;
    public int Tier;
    public int Level;
    public int Amount;
    public ComponentInfo(int type, int tier, int level, int amount)
    {
        this.Type = type;
        this.Tier = tier;
        this.Level = level;
        this.Amount = amount;
    }
}