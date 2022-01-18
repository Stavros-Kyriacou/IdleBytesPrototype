using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System;

public class WorkbenchTierMenu : MonoBehaviour
{
    public List<UpgradeWeight> upgradeWeights = new List<UpgradeWeight>();
    public int selectedComponent = 1;
    public int selectedTier;
    public int selectedLevel;
    public int selectedSocket;
    public int[,] craftingComponents = new int[5, 3];
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
    public List<Image> componentButtonImages;
    public List<Image> tierButtonImages;
    public List<Image> levelButtonImages;
    public List<Image> socketButtonImages;
    public List<TextMeshProUGUI> componentAmountTexts;
    public List<TextMeshProUGUI> socketTexts;
    public TextMeshProUGUI scrapCostText;
    public TextMeshProUGUI craftingTimeText;
    public TextMeshProUGUI scrapText;
    public List<ProgressBar> progressBars;
    public List<TextMeshProUGUI> chanceTexts;
    public RectTransform currentCraftsMenu;
    public List<CraftingTimer> craftingTimers;
    private void OnEnable()
    {
        this.SelectComponent(1);
        currentCraftsMenu.anchoredPosition = 600 * Vector2.up;
    }
    private void OnDisable()
    {
        ClearSockets();
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
        scrapText.text = $"Scrap: {Inventory.Instance.scrap}";
    }

    public void SelectComponent(int index)
    {
        foreach (var img in componentButtonImages)
        {
            img.color = Color.white;
        }
        componentButtonImages[index - 1].color = Color.green;

        this.selectedComponent = index;
        if (selectedTier > 0 && selectedLevel > 0)
        {
            UpdateText();
        }
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
        if (selectedTier > 0)
        {
            UpdateText();
        }
    }
    public void SelectSocket(int index)
    {
        if (this.selectedSocket == index)
        {
            //deselect socket
            this.selectedSocket = 0;
            socketButtonImages[index - 1].color = Color.white;
            if (selectedLevel != 0)
                levelButtonImages[selectedLevel - 1].color = Color.white;
        }
        else
        {
            foreach (var img in socketButtonImages)
            {
                img.color = Color.white;
            }
            foreach (var img in levelButtonImages)
            {
                img.color = Color.white;
            }
            socketButtonImages[index - 1].color = Color.green;

            this.selectedSocket = index;
        }
    }
    public void ConfirmCraftingComponent()
    {
        //IDEA
        //Change how items are taken from the inventory
        //Don't remove components from the inventory untill you press the craft button
        //This could be useful in the future if we encounter any errors with the current system

        //TODO: check if component inventory > 0                                                                                COMPLETE
        //TODO: check if the current tier/level component has already been selected                                             COMPLETE
        //TODO: add ability to replace the selected component with another                                                      COMPLETE
        //TODO: add reset all button                                                                                            COMPLETE
        //TODO: only allow components of same type and tier to be socketed                                                      COMPLETE
        //TODO: reset all sockets when closing the workbench                                                                    COMPLETE                     
        //TODO: add ability to deselect socket                                                                                  COMPLETE                                   
        //TODO: when selecting the same component level, unsocket the component and deselected the button                       NOT NEEDED
        //TODO: update crafting outcome percentages                                                                             COMPLETE
        //TODO: make the craft button work with dynamic scrap cost based on all component levels
        //TODO: crafting time
        //TODO: can only socket up to tier 9 components
        //TODO: allow for multiple upgrades at the same time





        if (selectedTier > 0 && selectedTier < 11 && selectedSocket != 0)
        {
            if (selectedTier != 10)
            {
                switch (selectedComponent)
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
                Debug.Log("You cannot upgrade Tier 10 components");
            }
        }
        else
        {
            Debug.Log("You must select a socket and tier first");
        }
        UpdateText();
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

        //only socket the component if there is one in the inventory
        if (componentInventory[selectedTier - 1, selectedLevel - 1] >= 1)
        {
            //check if the component type and tier is the same as all other socketed components
            //loop through craftingComponents
            //check if the component type matches

            //if all of the component types = 0
            //allow any type of component to be socketed
            //else
            //make sure that it matches all other socketed components

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
                craftingComponents[selectedSocket - 1, 0] = selectedTier;
                craftingComponents[selectedSocket - 1, 1] = selectedLevel;
                craftingComponents[selectedSocket - 1, 2] = selectedComponent;
                socketTexts[selectedSocket - 1].text = "Tier: " + craftingComponents[selectedSocket - 1, 0] + "\nLevel: " + craftingComponents[selectedSocket - 1, 1] + "\nType: " + type;
                componentInventory[selectedTier - 1, selectedLevel - 1]--;
            }
            else
            {
                //check what type of component is already socketed
                int socketedComponentType = 0;
                for (int i = 0; i < craftingComponents.GetLength(0); i++)
                {
                    if (craftingComponents[i, 2] != 0)
                    {
                        socketedComponentType = craftingComponents[i, 2];
                        break;
                    }
                }
                //only allow it to be socketed if it matches
                if (selectedComponent == socketedComponentType && selectedTier == tierSocketed)
                {
                    craftingComponents[selectedSocket - 1, 0] = selectedTier;
                    craftingComponents[selectedSocket - 1, 1] = selectedLevel;
                    craftingComponents[selectedSocket - 1, 2] = selectedComponent;
                    socketTexts[selectedSocket - 1].text = "Tier: " + craftingComponents[selectedSocket - 1, 0] + "\nLevel: " + craftingComponents[selectedSocket - 1, 1] + "\nType: " + type;
                    componentInventory[selectedTier - 1, selectedLevel - 1]--;
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
        //add all components in sockets back to inventories
        for (int i = 0; i < craftingComponents.GetLength(0); i++)
        {
            if (craftingComponents[i, 0] != 0 && craftingComponents[i, 1] != 0)
            {
                switch (craftingComponents[i, 2])
                {
                    case 1:
                        Inventory.Instance.cpuInventory[craftingComponents[i, 0] - 1, craftingComponents[i, 1] - 1]++;
                        break;
                    case 2:
                        Inventory.Instance.gpuInventory[craftingComponents[i, 0] - 1, craftingComponents[i, 1] - 1]++;
                        break;
                    case 3:
                        Inventory.Instance.ramInventory[craftingComponents[i, 0] - 1, craftingComponents[i, 1] - 1]++;
                        break;
                    case 4:
                        Inventory.Instance.hddInventory[craftingComponents[i, 0] - 1, craftingComponents[i, 1] - 1]++;
                        break;
                    default:
                        break;
                }
            }
        }

        //reset craftingComponents[]
        craftingComponents = new int[5, 3];

        //reset socketTexts
        for (int i = 0; i < socketTexts.Count; i++)
        {
            socketTexts[i].text = "Tier: 0\nLevel:0\nType:";
        }
        ResetCraftInfo();
        UpdateText();
    }
    public void UpdateText()
    {
        switch (selectedComponent)
        {
            case 1:
                //cpu
                for (int i = 0; i < this.componentAmountTexts.Count; i++)
                {
                    if (this.selectedTier != 0)
                        componentAmountTexts[i].text = $"x {Inventory.Instance.cpuInventory[this.selectedTier - 1, i].ToString()}";
                }
                break;
            case 2:
                //gpu
                for (int i = 0; i < this.componentAmountTexts.Count; i++)
                {
                    if (this.selectedTier != 0)
                        componentAmountTexts[i].text = $"x {Inventory.Instance.gpuInventory[this.selectedTier - 1, i].ToString()}";
                }
                break;
            case 3:
                //ram
                for (int i = 0; i < this.componentAmountTexts.Count; i++)
                {
                    if (this.selectedTier != 0)
                        componentAmountTexts[i].text = $"x {Inventory.Instance.ramInventory[this.selectedTier - 1, i].ToString()}";
                }
                break;
            case 4:
                //hdd
                for (int i = 0; i < this.componentAmountTexts.Count; i++)
                {
                    if (this.selectedTier != 0)
                        componentAmountTexts[i].text = $"x {Inventory.Instance.hddInventory[this.selectedTier - 1, i].ToString()}";
                }
                break;
            default:
                break;
        }
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
        TimeSpan time = TimeSpan.FromSeconds(5 * craftingComponents[0, 0]);
        craftingTimeText.text = time.ToString(@"hh\:mm\:ss");
    }
    public void Craft()
    {
        //base scrap cost increases depending on the tier of upgrade
        //the combined level of the socketed components affects the price
        //using high level components has a cheaper upgrade cost than using low level

        if (AllSocketed)
        {
            //check if you have enough scrap
            //subtract cost from inventory
            //clear crafting sockets
            //start crafting timer
            //option to cancel crafting

            //once timer is finished
            //add new component to inventory
            if (Inventory.Instance.scrap >= ScrapCost)
            {
                int craftDuration = 5 * craftingComponents[0, 0];
                int craftTier = craftingComponents[0, 0] + 1;
                int craftLevel = RollWeights(upgradeWeights[TotalSocketedLevel - 1].weights) + 1;
                int craftType = craftingComponents[0, 2];
                Debug.Log($"Craft tier: {craftTier}, Craft Level: {craftLevel}, Craft Type: {craftType}");

                Inventory.Instance.scrap -= ScrapCost;
                scrapText.text = $"Scrap: {Inventory.Instance.scrap}";

                DeleteComponents();
                ResetCraftInfo();

                craftingTimers[0].StartCraft(craftDuration, craftTier, craftLevel, craftType);
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
    public void DeleteComponents()
    {
        //reset craftingComponents[]
        craftingComponents = new int[5, 3];

        //reset socketTexts
        for (int i = 0; i < socketTexts.Count; i++)
        {
            socketTexts[i].text = "Tier: 0\nLevel:0\nType:";
        }
        UpdateText();
    }
    public int RollWeights(List<int> weights)
    {
        //Takes an array of item weights and makes a roll on the loot table
        //Returns the index of the item rolled in the array
        int total = 0;
        int index = 0;

        foreach (var weight in weights)
        {
            total += weight;
        }

        int roll = UnityEngine.Random.Range(0, total + 1);

        for (int i = 0; i < weights.Count; i++)
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
    public void ToggleCraftsMenu()
    {
        if (currentCraftsMenu.anchoredPosition == Vector2.zero)
        {
            currentCraftsMenu.anchoredPosition = Vector2.up * 600;
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
public class UpgradeWeight
{
    public List<int> weights;
}