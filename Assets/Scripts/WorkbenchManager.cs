using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class WorkbenchManager : MonoBehaviour
{
    public List<UpgradeWeight> upgradeWeights = new List<UpgradeWeight>();
    public GameObject levelUpgradeMenu;
    public GameObject tierUpgradeMenu;
    public int selectedComponent = 1;
    public int selectedTier;
    public int selectedLevel;
    public int selectedSocket;
    public int[,] craftingComponents = new int[5, 3];
    public List<Image> componentButtonImages;
    public List<Image> tierButtonImages;
    public List<Image> levelButtonImages;
    public List<Image> socketButtonImages;
    public List<TextMeshProUGUI> componentAmountTexts;
    public List<TextMeshProUGUI> socketTexts;
    public TextMeshProUGUI changeMenuButtonText;
    private void OnEnable()
    {
        tierUpgradeMenu.SetActive(true);
        levelUpgradeMenu.SetActive(false);
        changeMenuButtonText.text = "Upgrade Component Levels";
        this.SelectComponent(1);
        // this.SelectTier(1);
        // this.SelectLevel(1);
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
    public void SwapMenu()
    {
        if (tierUpgradeMenu.activeInHierarchy)
        {
            tierUpgradeMenu.SetActive(false);
            levelUpgradeMenu.SetActive(true);
            changeMenuButtonText.text = "Combine Components";
        }
        else
        {
            tierUpgradeMenu.SetActive(true);
            levelUpgradeMenu.SetActive(false);
            changeMenuButtonText.text = "Upgrade Component Levels";
        }
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
        foreach (var img in socketButtonImages)
        {
            img.color = Color.white;
        }
        foreach (var img in levelButtonImages)
        {
            img.color = Color.white;
        }
        foreach (var img in tierButtonImages)
        {
            img.color = Color.white;
        }
        socketButtonImages[index - 1].color = Color.green;

        this.selectedSocket = index;
    }
    public void ConfirmCraftingComponent()
    {
        //TODO: check if component inventory > 0                                        COMPLETE
        //TODO: check if the current tier/level component has already been selected     COMPLETE
        //TODO: add ability to replace the selected component with another              COMPLETE
        //TODO: add reset all button                                                    COMPLETE
        //TODO: reset all sockets when closing the workbench
        //TODO: add ability to deselect socket
        //TODO: when selecting the same component level, unsocket the component and deselected the button
        //TODO: only allow components of same type and tier to be socketed
        //TODO: update crafting outcome percentages
        //TODO: make the craft button work with dynamic scrap cost based on all component levels

        if (selectedTier > 0 && selectedTier < 11)
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
            Debug.Log("You must select a tier first");
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
            craftingComponents[selectedSocket - 1, 0] = selectedTier;
            craftingComponents[selectedSocket - 1, 1] = selectedLevel;
            craftingComponents[selectedSocket - 1, 2] = selectedComponent;
            socketTexts[selectedSocket - 1].text = "Tier: " + craftingComponents[selectedSocket - 1, 0] + "\nLevel: " + craftingComponents[selectedSocket - 1, 1] + "\nType: " + type;
            componentInventory[selectedTier - 1, selectedLevel - 1]--;
        }
        else
        {
            Debug.Log("Component could not be socketed for crafting, not in inventory");
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









}
public class UpgradeWeight
{
    public List<int> weights;
}
