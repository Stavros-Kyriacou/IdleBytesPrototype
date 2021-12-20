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
    public Text CPUText;
    public Text CPUEffectText;
    public Text CPUWattsText;
    public Text GPUText;
    public Text GPUEffectText;
    public Text GPUWattsText;
    public Text RAMText;
    public Text RAMEffectText;
    public Text RAMWattsText;
    public Text HDDText;
    public Text HDDEffectText;
    public Text HDDWattsText;
    [HideInInspector] public int selectedTier = 1;
    [HideInInspector] public int selectedLevel = 1;
    private int componentType;
    public UnityEvent OnSelectionChange = new UnityEvent();
    private int[] selectedCPU = new int[2];
    private int[] selectedGPU = new int[2];
    private int[] selectedRAM = new int[2];
    private int[] selectedHDD = new int[2];
    public ComputerHandler compHandler;
    public ComputerStats computerStats;
    public CPU cpu = new CPU();
    public GPU gpu = new GPU();
    public RAM ram = new RAM();
    public HDD hdd = new HDD();

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
    public void Test()
    {
        this.selectedCPU[0] = compHandler.selectedComputer.CPU.tier;
        this.selectedCPU[1] = compHandler.selectedComputer.CPU.level;
        this.cpu.Change(compHandler.selectedComputer.CPU.tier, compHandler.selectedComputer.CPU.level);

        this.selectedGPU[0] = compHandler.selectedComputer.GPU.tier;
        this.selectedGPU[1] = compHandler.selectedComputer.GPU.level;
        this.gpu.Change(compHandler.selectedComputer.GPU.tier, compHandler.selectedComputer.GPU.level);

        this.selectedRAM[0] = compHandler.selectedComputer.RAM.tier;
        this.selectedRAM[1] = compHandler.selectedComputer.RAM.level;
        this.ram.Change(compHandler.selectedComputer.RAM.tier, compHandler.selectedComputer.RAM.level);

        this.selectedHDD[0] = compHandler.selectedComputer.HDD.tier;
        this.selectedHDD[1] = compHandler.selectedComputer.HDD.level;
        this.hdd.Change(compHandler.selectedComputer.HDD.tier, compHandler.selectedComputer.HDD.level);

        for (int i = 1; i < 5; i++)
        {
            this.componentType = i;
            this.UpdateText();
        }
    }
    public void UpdateText()
    {
        this.currentSelectionText.text = $"Tier: {this.selectedTier}    Level: {this.selectedLevel}";
        switch (this.componentType)
        {
            case 1:
                this.numberOwnedText.text = $"Owned: {Inventory.Instance.cpuInventory[this.selectedTier - 1, this.selectedLevel - 1]}";
                this.CPUText.text = $"Tier: {this.selectedCPU[0]}    Level: {this.selectedCPU[1]}";
                this.CPUWattsText.text = $"Power consumption: {this.cpu.watts}W";
                this.CPUEffectText.text = $"Production Bonus: +{this.cpu.dollarsPerSec.ToString("F2")}/s";
                break;
            case 2:
                this.numberOwnedText.text = $"Owned: {Inventory.Instance.gpuInventory[this.selectedTier - 1, this.selectedLevel - 1]}";
                this.GPUText.text = $"Tier: {this.selectedGPU[0]}    Level: {this.selectedGPU[1]}";
                this.GPUWattsText.text = $"Power consumption: {this.gpu.watts}W";
                this.GPUEffectText.text = $"Production Bonus: {this.gpu.productionBonus.ToString("F2")}%";
                break;
            case 3:
                this.numberOwnedText.text = $"Owned: {Inventory.Instance.ramInventory[this.selectedTier - 1, this.selectedLevel - 1]}";
                this.RAMText.text = $"Tier: {this.selectedRAM[0]}    Level: {this.selectedRAM[1]}";
                this.RAMWattsText.text = $"Power consumption: {this.ram.watts}W";
                this.RAMEffectText.text = $"Production Bonus: +{this.ram.dollarsPerSec.ToString("F2")}/s";
                break;
            case 4:
                this.numberOwnedText.text = $"Owned: {Inventory.Instance.hddInventory[this.selectedTier - 1, this.selectedLevel - 1]}";
                this.HDDText.text = $"Tier: {this.selectedHDD[0]}    Level: {this.selectedHDD[1]}";
                this.HDDWattsText.text = $"Power consumption: {this.hdd.watts}W";
                this.HDDEffectText.text = $"Offline Bonus: {this.hdd.offlineProductionBonus.ToString("F2")}%";
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
                this.cpu.Change(this.selectedTier, this.selectedLevel);
                Debug.Log("Watts: " + this.cpu.watts);
                Debug.Log("$/s: " + this.cpu.dollarsPerSec);
                break;
            case 2:
                this.selectedGPU[0] = this.selectedTier;
                this.selectedGPU[1] = this.selectedLevel;
                this.gpu.Change(this.selectedTier, this.selectedLevel);
                Debug.Log($"Selected GPU - Tier: {this.selectedTier}   Level:{this.selectedLevel}");
                break;
            case 3:
                this.selectedRAM[0] = this.selectedTier;
                this.selectedRAM[1] = this.selectedLevel;
                this.ram.Change(this.selectedTier, this.selectedLevel);
                Debug.Log($"Selected RAM - Tier: {this.selectedTier}   Level:{this.selectedLevel}");
                break;
            case 4:
                this.selectedHDD[0] = this.selectedTier;
                this.selectedHDD[1] = this.selectedLevel;
                this.hdd.Change(this.selectedTier, this.selectedLevel);
                Debug.Log($"Selected HDD - Tier: {this.selectedTier}   Level:{this.selectedLevel}");
                break;
            default:
                break;
        }
        this.CloseComponentInventory();
        this.UpdateText();
    }
    public void ApplySelection()
    {
        this.compHandler.selectedComputer.CPU.Change(this.selectedCPU[0], this.selectedCPU[1]);
        this.compHandler.selectedComputer.GPU.Change(this.selectedGPU[0], this.selectedGPU[1]);
        this.compHandler.selectedComputer.RAM.Change(this.selectedRAM[0], this.selectedRAM[1]);
        this.compHandler.selectedComputer.HDD.Change(this.selectedHDD[0], this.selectedHDD[1]);
        GameController.Instance.CalculateDollarsPerSec();
        computerStats.UpdateText();
    }
}
