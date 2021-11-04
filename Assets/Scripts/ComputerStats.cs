using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerStats : MonoBehaviour
{
    public ComputerHandler compHandler;

    //computer stats
    [Header("Computer Stats")]
    public Text levelText;
    public Text dollarsPerSecText;
    public Text upgradeCostText;

    //motherboard stats
    [Header("Motherboard Stats")]
    public Text mobLevelText;
    public Text mobEffectText;
    public Text mobWattsText;
    public Text mobUpgradeCostText;

    //power supply stats
    [Header("Power Supply Stats")]
    public Text psuLevelText;
    public Text psuEffectText;
    public Text psuWattsText;
    public Text psuUpgradeCostText;

    //cpu stats
    [Header("CPU Stats")]
    public Text cpuLevelText;
    public Text cpuEffectText;
    public Text cpuWattsText;

    //gpu stats
    [Header("GPU Stats")]
    public Text gpuLevelText;
    public Text gpuEffectText;
    public Text gpuWattsText;

    //ram stats
    [Header("RAM Stats")]
    public Text ramLevelText;
    public Text ramEffectText;
    public Text ramWattsText;

    //hdd stats
    [Header("HDD Stats")]
    public Text hddLevelText;
    public Text hddEffectText;
    public Text hddWattsText;

    private void Awake()
    {
        this.compHandler = GetComponent<ComputerHandler>();
    }
    public void UpdateText()
    {
        if (this.compHandler.selectedComputer != null)
        {
            //computer
            this.levelText.text = $"Level: {this.compHandler.selectedComputer.level}";
            this.dollarsPerSecText.text = $"${this.compHandler.selectedComputer.totalDPS.ToString("F0")}/s";
            this.upgradeCostText.text = $"Upgrade Cost: ${(this.compHandler.selectedComputer.discountedCost).ToString("F0")}";

            //motherboard
            this.mobLevelText.text = $"Level: {this.compHandler.selectedComputer.MOB.level}";
            this.mobEffectText.text = $"Computer Upgrade Discount: {((1 - this.compHandler.selectedComputer.MOB.Discount) * 100).ToString("F1")}%";
            this.mobWattsText.text = $"Power consumption: {this.compHandler.selectedComputer.MOB.watts}W";
            this.mobUpgradeCostText.text = $"Upgrade Cost: ${this.compHandler.selectedComputer.MOB.upgradeCost.ToString("F0")}";

            //power supply
            this.psuLevelText.text = $"Level: {this.compHandler.selectedComputer.PSU.level}";
            this.psuEffectText.text = $"Production Bonus: {this.compHandler.selectedComputer.PSU.productionBonus}%";
            this.psuWattsText.text = $"Max Power: {this.compHandler.selectedComputer.GetWattUsage()}W/{this.compHandler.selectedComputer.PSU.currentWatts}W";
            this.psuUpgradeCostText.text = $"Upgrade Cost: ${this.compHandler.selectedComputer.PSU.upgradeCost.ToString("F0")}";

            //cpu
            this.cpuLevelText.text = $"Tier: {this.compHandler.selectedComputer.CPU.tier}     Level: {this.compHandler.selectedComputer.CPU.level}";
            this.cpuEffectText.text = $"Production Bonus: +${this.compHandler.selectedComputer.CPU.dollarsPerSec}/s";
            this.cpuWattsText.text = $"Power Consumption: {this.compHandler.selectedComputer.CPU.watts}W";

            //gpu
            this.gpuLevelText.text = $"Tier: {this.compHandler.selectedComputer.GPU.tier}     Level: {this.compHandler.selectedComputer.GPU.level}";
            this.gpuEffectText.text = $"Production Bonus: ${this.compHandler.selectedComputer.GPU.productionBonus}%";
            this.gpuWattsText.text = $"Power Consumption: {this.compHandler.selectedComputer.GPU.watts}W";

            //ram
            this.ramLevelText.text = $"Tier: {this.compHandler.selectedComputer.RAM.tier}     Level: {this.compHandler.selectedComputer.RAM.level}";
            this.ramEffectText.text = $"Production Bonus: +${this.compHandler.selectedComputer.RAM.dollarsPerSec}/s";
            this.ramWattsText.text = $"Power Consumption: {this.compHandler.selectedComputer.RAM.watts}W";

            //hdd
            this.hddLevelText.text = $"Tier: {this.compHandler.selectedComputer.HDD.tier}     Level: {this.compHandler.selectedComputer.HDD.level}";
            this.hddEffectText.text = $"Offline Production Bonus: +${this.compHandler.selectedComputer.HDD.offlineProductionBonus}%";
            this.hddWattsText.text = $"Power Consumption: {this.compHandler.selectedComputer.CPU.watts}W";
        }
    }
}
