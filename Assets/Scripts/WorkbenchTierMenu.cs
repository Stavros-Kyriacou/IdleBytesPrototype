using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System;

public class WorkbenchTierMenu : MonoBehaviour
{
    private int selectedSocket;
    private int[,] craftingComponents = new int[5, 3]; //Rows: Tier, Level, ComponentType
    private List<UpgradeWeight> upgradeWeights = new List<UpgradeWeight>();
    public List<Image> socketButtonImages;
    public List<TextMeshProUGUI> socketTexts;
    public TextMeshProUGUI scrapCostText;
    public TextMeshProUGUI craftingTimeText;
    public List<ProgressBar> progressBars;
    public List<TextMeshProUGUI> chanceTexts;
    public RectTransform currentCraftsMenu;
    public ComponentInventory ComponentInventory;
    public WorkbenchManager WorkbenchManager;
    public List<CraftingTimer> craftingTimers;
    public CancelCraftMenu cancelCraftMenu;
    public bool AllSocketed
    {
        get
        {
            for (int i = 0; i < craftingComponents.GetLength(0); i++)
            {
                if (craftingComponents[i, 0] == 0)
                    return false;
            }
            return true;
        }
        private set { }
    }
    public int TotalSocketedLevel
    {
        get
        {
            int total = 0;
            for (int i = 0; i < craftingComponents.GetLength(0); i++)
            {
                total += craftingComponents[i, 1];
            }
            return total;
        }
        private set { }
    }
    public int ScrapCost
    {
        get
        {
            //base price = tier * 100
            //modifier
            //1000 - (totalCombinedLevel * 5)
            return (craftingComponents[0, 0] * 100) + (1000 - (TotalSocketedLevel * 5));
        }
        private set { }
    }
    private void Awake()
    {
        var dataset = Resources.Load<TextAsset>("UpgradeWeights");
        var dataLines = dataset.text.Split('\n');

        for (int i = 0; i < dataLines.Length; i++)
        {
            var data = dataLines[i].Split(',');
            var weights = data.Select(s => int.Parse(s)).ToList();
            upgradeWeights.Add(new UpgradeWeight() { weights = weights });
        }
    }
    private void Start()
    {
        currentCraftsMenu.anchoredPosition = 1000 * Vector2.up;
    }
    public void SelectSocket(int index)
    {
        if (this.selectedSocket == index)
        {
            //when clicking the same already selected socket, select it
            this.selectedSocket = 0;
            socketButtonImages[index - 1].color = Color.white;
            ComponentInventory.DeselectLevel();
        }
        else
        {
            //select the new socket
            foreach (var img in socketButtonImages)
            {
                img.color = Color.white;
            }
            ComponentInventory.DeselectLevel();
            socketButtonImages[index - 1].color = Color.green;

            this.selectedSocket = index;
        }
    }
    public void ConfirmCraftingComponent()
    {
        if (ComponentInventory.SelectedTier > 0 && ComponentInventory.SelectedTier < 10 && selectedSocket != 0)
        {
            //if a Tier 1-9 component and Socket is selected
            switch (ComponentInventory.SelectedComponent)
            {
                case 1:
                    SocketComponent("CPU", Inventory.Instance.cpuInventory);
                    break;
                case 2:
                    SocketComponent("GPU", Inventory.Instance.gpuInventory);
                    break;
                case 3:
                    SocketComponent("RAM", Inventory.Instance.ramInventory);
                    break;
                case 4:
                    SocketComponent("HDD", Inventory.Instance.hddInventory);
                    break;
                default:
                    break;
            }
        }
        else
        {
            Debug.Log("You must select a socket and tier first");
        }
        ComponentInventory.UpdateText();
    }
    public void SocketComponent(string type, int[,] componentInventory)
    {
        //if a component is already socketed, remove the current component from the socket and add it back to the inventory
        if (craftingComponents[selectedSocket - 1, 0] != 0 && craftingComponents[selectedSocket - 1, 1] != 0 && craftingComponents[selectedSocket - 1, 2] != 0)
        {
            //add component back to inventory
            switch (craftingComponents[selectedSocket - 1, 2])
            {
                case 1:
                    Inventory.Instance.cpuInventory[craftingComponents[selectedSocket - 1, 0] - 1, craftingComponents[selectedSocket - 1, 1] - 1]++;
                    break;
                case 2:
                    Inventory.Instance.gpuInventory[craftingComponents[selectedSocket - 1, 0] - 1, craftingComponents[selectedSocket - 1, 1] - 1]++;
                    break;
                case 3:
                    Inventory.Instance.ramInventory[craftingComponents[selectedSocket - 1, 0] - 1, craftingComponents[selectedSocket - 1, 1] - 1]++;
                    break;
                case 4:
                    Inventory.Instance.hddInventory[craftingComponents[selectedSocket - 1, 0] - 1, craftingComponents[selectedSocket - 1, 1] - 1]++;
                    break;
                default:
                    break;
            }
            //remove from socket
            craftingComponents[selectedSocket - 1, 0] = 0;
            craftingComponents[selectedSocket - 1, 1] = 0;
            craftingComponents[selectedSocket - 1, 2] = 0;
            socketTexts[selectedSocket - 1].text = "Tier: " + craftingComponents[selectedSocket - 1, 0] + "\nLevel: " + craftingComponents[selectedSocket - 1, 1] + "\nType: " + type;
        }

        //Only allow component to be socketed if its availablie in inventory
        if (componentInventory[ComponentInventory.SelectedTier - 1, ComponentInventory.SelectedLevel - 1] >= 1)
        {
            //Check if a component has been socketed, if it has record what tier it was
            bool componentSocketed = false;
            int tierSocketed = 0;
            for (int i = 0; i < craftingComponents.GetLength(0); i++)
            {
                if (craftingComponents[i, 2] != 0)
                {
                    //if true then a component is socketed
                    componentSocketed = true;
                }
                if (craftingComponents[i, 0] > 0)
                {
                    tierSocketed = craftingComponents[i, 0];
                }
            }

            if (!componentSocketed)
            {
                //if a component hasnt been socketed yet, allow any type to be socketed
                craftingComponents[selectedSocket - 1, 0] = ComponentInventory.SelectedTier;
                craftingComponents[selectedSocket - 1, 1] = ComponentInventory.SelectedLevel;
                craftingComponents[selectedSocket - 1, 2] = ComponentInventory.SelectedComponent;
                socketTexts[selectedSocket - 1].text = "Tier: " + craftingComponents[selectedSocket - 1, 0] + "\nLevel: " + craftingComponents[selectedSocket - 1, 1] + "\nType: " + type;
                componentInventory[ComponentInventory.SelectedTier - 1, ComponentInventory.SelectedLevel - 1]--;
            }
            else
            {
                //If a component is socketed already, record its type
                int socketedComponentType = 0;
                for (int i = 0; i < craftingComponents.GetLength(0); i++)
                {
                    if (craftingComponents[i, 2] != 0)
                    {
                        socketedComponentType = craftingComponents[i, 2];
                        break;
                    }
                }

                //Only socket it if its Type and Tier matches the other socketed components
                if (ComponentInventory.SelectedComponent == socketedComponentType && ComponentInventory.SelectedTier == tierSocketed)
                {
                    craftingComponents[selectedSocket - 1, 0] = ComponentInventory.SelectedTier;
                    craftingComponents[selectedSocket - 1, 1] = ComponentInventory.SelectedLevel;
                    craftingComponents[selectedSocket - 1, 2] = ComponentInventory.SelectedComponent;
                    socketTexts[selectedSocket - 1].text = "Tier: " + craftingComponents[selectedSocket - 1, 0] + "\nLevel: " + craftingComponents[selectedSocket - 1, 1] + "\nType: " + type;
                    componentInventory[ComponentInventory.SelectedTier - 1, ComponentInventory.SelectedLevel - 1]--;
                }
                else
                {
                    Debug.Log("Could not socket component, must be same type and tier as all others socketed");
                }
            }
        }
        else
        {
            Debug.Log("Component could not be socketed for crafting, not in inventory");
        }
        if (AllSocketed)
        {
            UpdateCraftingPercentages();
        }
    }
    public void ClearSockets()
    {
        //Add all components in sockets back to inventories
        for (int i = 0; i < craftingComponents.GetLength(0); i++)
        {
            Inventory.Instance.AddComponent(craftingComponents[i, 2], craftingComponents[i, 0], craftingComponents[i, 1], 1);
        }

        //Reset craftingComponents[]
        craftingComponents = new int[5, 3];

        //reset socketTexts
        for (int i = 0; i < socketTexts.Count; i++)
        {
            socketTexts[i].text = "Tier: 0\nLevel:0\nType:";
        }
        ResetCraftInfo();
        ComponentInventory.DeselectLevel();
        ComponentInventory.UpdateText();
    }
    public void UpdateCraftingPercentages()
    {
        int totalWeight = 0;
        var weights = upgradeWeights[TotalSocketedLevel - 1].weights;

        for (int i = 0; i < weights.Count; i++)
        {
            totalWeight += weights[i];
        }

        float chance = 0f;
        for (int i = 0; i < weights.Count; i++)
        {
            chance = ((float)weights[i] / (float)totalWeight) * 100;

            chanceTexts[i].text = $"Lvl {i + 1}: {chance.ToString("F2")}%";

            progressBars[i].GetCurrentFill(weights[i], totalWeight);
        }
        scrapCostText.text = $"Cost: {ScrapCost} Scrap";
        TimeSpan time = TimeSpan.FromSeconds((5 * craftingComponents[0, 0]) + 10);
        craftingTimeText.text = time.ToString(@"hh\:mm\:ss");
    }
    public void Craft()
    {
        //base scrap cost increases depending on the tier of upgrade
        //the combined level of the socketed components affects the price
        //using high level components has a cheaper upgrade cost than using low level

        if (AllSocketed)
        {
            if (Inventory.Instance.Scrap >= ScrapCost)
            {
                int craftDuration = (5 * craftingComponents[0, 0]) + 10;
                int craftTier = craftingComponents[0, 0] + 1;
                int craftLevel = Ext.RollWeights(upgradeWeights[TotalSocketedLevel - 1].weights) + 1;
                int craftType = craftingComponents[0, 2];

                var availableTimer = FindAvailableCraftTimer();

                if (availableTimer != null)
                {
                    Debug.Log($"Craft tier: {craftTier}, Craft Level: {craftLevel}, Craft Type: {craftType}");

                    Inventory.Instance.Scrap -= ScrapCost;
                    WorkbenchManager.UpdateScrapText();

                    availableTimer.StartCraft(craftDuration, craftTier, craftLevel, craftType, ScrapCost, craftingComponents);

                    DeleteComponents();
                    ResetCraftInfo();
                    ComponentInventory.DeselectLevel();
                    ComponentInventory.UpdateText();
                }
                else
                {
                    Debug.Log("No craft slot available");
                }
            }
            else
            {
                Debug.Log("Not enough scrap");
            }
        }
        else
        {
            Debug.Log("Must have 5 components socketed to craft");
        }
    }
    public CraftingTimer FindAvailableCraftTimer()
    {
        for (int i = 0; i < craftingTimers.Count; i++)
        {
            if (craftingTimers[i].TimerAvailable)
            {
                return craftingTimers[i];
            }
        }
        return null;
    }
    public void DeleteComponents()
    {
        //reset craftingComponents[]
        craftingComponents = new int[5, 3];

        //reset socketTexts
        for (int i = 0; i < socketTexts.Count; i++)
        {
            socketTexts[i].text = "Tier: 0\nLevel:0\nType:";
        }
        // UpdateText();
    }
    public void ToggleCraftsMenu()
    {
        if (currentCraftsMenu.anchoredPosition == Vector2.zero)
        {
            currentCraftsMenu.anchoredPosition = Vector2.up * 1000;
        }
        else
        {
            currentCraftsMenu.anchoredPosition = Vector2.zero;
        }
    }
    public void ResetCraftInfo()
    {
        for (int i = 0; i < chanceTexts.Count; i++)
        {
            chanceTexts[i].text = $"Level {i}: 0%";
            progressBars[i].GetCurrentFill(0, 1);
        }
        craftingTimeText.text = "00:00:00";
    }
}