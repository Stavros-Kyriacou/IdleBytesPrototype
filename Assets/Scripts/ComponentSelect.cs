using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class ComponentSelect : MonoBehaviour
{
    public Text currentSelectionText;
    public Text numberOwnedText;
    public GameObject componentInventory;
    public Text selectedCPUText;
    public Text selectedCPUWatts;
    public Text selectedGPUText;
    public Text selectedGPUWatts;
    public Text selectedRAMText;
    public Text selectedRAMWatts;
    public Text selectedHDDText;
    public Text selectedHDDWatts;
    [HideInInspector] public int selectedTier = 1;
    [HideInInspector] public int selectedLevel = 1;
    private int componentType;
    public UnityEvent OnSelectionChange = new UnityEvent();
    private int[] selectedCPU = new int[2];
    private int[] selectedGPU = new int[2];
    private int[] selectedRAM = new int[2];
    private int[] selectedHDD = new int[2];

    private void OnEnable()
    {
        this.componentInventory.SetActive(false);
        OnSelectionChange?.Invoke();
    }
    public void SelectTier(int tier)
    {
        this.selectedTier = tier;
        OnSelectionChange?.Invoke();
    }
    public void SelectLevel(int level)
    {
        this.selectedLevel = level;
        OnSelectionChange?.Invoke();
    }
    public void UpdateText()
    {
        this.currentSelectionText.text = $"Tier: {this.selectedTier}    Level: {this.selectedLevel}";
        switch (this.componentType)
        {
            case 1:
                this.numberOwnedText.text = $"Owned: {Inventory.Instance.cpuInventory[this.selectedTier - 1, this.selectedLevel - 1]}";
                break;
            case 2:
                this.numberOwnedText.text = $"Owned: {Inventory.Instance.gpuInventory[this.selectedTier - 1, this.selectedLevel - 1]}";
                break;
            case 3:
                this.numberOwnedText.text = $"Owned: {Inventory.Instance.ramInventory[this.selectedTier - 1, this.selectedLevel - 1]}";
                break;
            case 4:
                this.numberOwnedText.text = $"Owned: {Inventory.Instance.hddInventory[this.selectedTier - 1, this.selectedLevel - 1]}";
                break;
            default:
                break;
        }
    }
    public void OpenComponentInventory(int componentType)
    {
        this.componentType = componentType;
        this.selectedLevel = 1;
        this.selectedTier = 1;
        this.componentInventory.SetActive(true);
        OnSelectionChange?.Invoke();
    }
    public void CloseComponentInventory()
    {
        this.componentInventory.SetActive(false);
    }
    public void ConfirmSelection()
    {
        switch (componentType)
        {
            case 1:
                this.selectedCPU[0] = this.selectedTier;
                this.selectedCPU[1] = this.selectedLevel;
                Debug.Log($"Selected CPU - Tier: {this.selectedTier}   Level:{this.selectedLevel}");
                break;
            case 2:
                this.selectedGPU[0] = this.selectedTier;
                this.selectedGPU[1] = this.selectedLevel;
                Debug.Log($"Selected GPU - Tier: {this.selectedTier}   Level:{this.selectedLevel}");
                break;
            case 3:
                this.selectedRAM[0] = this.selectedTier;
                this.selectedRAM[1] = this.selectedLevel;
                Debug.Log($"Selected RAM - Tier: {this.selectedTier}   Level:{this.selectedLevel}");
                break;
            case 4:
                this.selectedHDD[0] = this.selectedTier;
                this.selectedHDD[1] = this.selectedLevel;
                Debug.Log($"Selected HDD - Tier: {this.selectedTier}   Level:{this.selectedLevel}");
                break;
            default:
                break;
        }
        this.CloseComponentInventory();
    }
}
