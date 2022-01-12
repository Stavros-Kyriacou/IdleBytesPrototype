using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public int scrap;
    
    //[rows, columns]
    //[tier, level]
    public int[,] cpuInventory = new int[10, 10];
    public int[,] gpuInventory = new int[10, 10];
    public int[,] ramInventory = new int[10, 10];
    public int[,] hddInventory = new int[10, 10];
    public int[] lootBoxes = new int[5];
    public List<TextMeshProUGUI> levelTexts;
    public List<Image> componentButtonImages;
    public List<Image> tierImages;
    public List<Image> levelButtonImages;
    public TextMeshProUGUI effectText;
    public TextMeshProUGUI wattsText;
    public Sprite[] componentSprites;
    private int selectedComponent = 1;
    private int selectedTier = 1;
    private int selectedLevel = 1;
    public CPU cpu = new CPU();
    private GPU gpu = new GPU();
    private RAM ram = new RAM();
    private HDD hdd = new HDD();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        for (int i = 0; i < this.cpuInventory.GetLength(0); i++)
        {
            for (int j = 0; j < this.cpuInventory.GetLength(1); j++)
            {
                this.cpuInventory[i, j] = 1;
                this.gpuInventory[i, j] = 1;
                this.ramInventory[i, j] = 1;
                this.hddInventory[i, j] = 1;
            }
        }
        for (int i = 0; i < this.lootBoxes.Length; i++)
        {
            lootBoxes[i] = 10;
        }
        this.SelectComponent(1);
        this.SelectTier(1);
        this.SelectLevel(1);
    }
    public void PrintInventory()
    {
        string temp = "";
        Debug.Log("Printing CPU inventory");
        for (int i = 0; i < this.cpuInventory.GetLength(0); i++)
        {
            for (int j = 0; j < this.cpuInventory.GetLength(1); j++)
            {
                temp += this.cpuInventory[i, j];
                temp += ", ";
            }
            Debug.Log(temp);
            temp = "";
        }
    }
    public void SelectTier(int tier)
    {
        foreach (var img in tierImages)
        {
            img.color = Color.white;
        }
        tierImages[tier - 1].color = Color.green;

        this.selectedTier = tier;
        this.UpdateText();
    }
    public void SelectComponent(int component)
    {
        foreach (var img in componentButtonImages)
        {
            img.color = Color.white;
        }
        componentButtonImages[component - 1].color = Color.green;

        this.selectedComponent = component;
        this.UpdateText();
    }
    public void SelectLevel(int level)
    {
        foreach (var img in levelButtonImages)
        {
            img.color = Color.white;
        }
        levelButtonImages[level - 1].color = Color.green;
        this.selectedLevel = level;
        this.UpdateText();
    }
    public void UpdateText()
    {
        switch (selectedComponent)
        {
            case 1:
                //cpu
                for (int i = 0; i < this.levelTexts.Count; i++)
                {
                    levelTexts[i].text = $"x {cpuInventory[this.selectedTier - 1, i].ToString()}";
                    levelButtonImages[i].sprite = componentSprites[0];
                }
                this.cpu.Change(this.selectedTier, this.selectedLevel);
                this.effectText.text = $"Production Bonus: +{this.cpu.dollarsPerSec.ToString("F2")}/s";
                this.wattsText.text = $"Power consumption: {this.cpu.watts}W";
                break;
            case 2:
                //gpu
                for (int i = 0; i < this.levelTexts.Count; i++)
                {
                    levelTexts[i].text = $"x {gpuInventory[this.selectedTier - 1, i].ToString()}";
                    levelButtonImages[i].sprite = componentSprites[1];
                }
                this.gpu.Change(this.selectedTier, this.selectedLevel);
                this.effectText.text = $"Production Bonus: +{this.gpu.productionBonus.ToString("F2")}/s";
                this.wattsText.text = $"Power consumption: {this.gpu.watts}W";
                break;
            case 3:
                //ram
                for (int i = 0; i < this.levelTexts.Count; i++)
                {
                    levelTexts[i].text = $"x {ramInventory[this.selectedTier - 1, i].ToString()}";
                    levelButtonImages[i].sprite = componentSprites[2];
                }
                this.ram.Change(this.selectedTier, this.selectedLevel);
                this.effectText.text = $"Production Bonus: +{this.ram.dollarsPerSec.ToString("F2")}/s";
                this.wattsText.text = $"Power consumption: {this.ram.watts}W";
                break;
            case 4:
                //hdd
                for (int i = 0; i < this.levelTexts.Count; i++)
                {
                    levelTexts[i].text = $"x {hddInventory[this.selectedTier - 1, i].ToString()}";
                    levelButtonImages[i].sprite = componentSprites[3];
                }
                this.hdd.Change(this.selectedTier, this.selectedLevel);
                this.effectText.text = $"Offline Bonus: +{this.hdd.offlineProductionBonus.ToString("F2")}/s";
                this.wattsText.text = $"Power consumption: {this.hdd.watts}W";
                break;
            default:
                break;
        }
    }
}