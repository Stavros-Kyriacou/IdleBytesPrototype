using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    //[rows, columns]
    //[tier, level]
    public int[,] cpuInventory = new int[10, 10];
    public int[,] gpuInventory = new int[10, 10];
    public int[,] ramInventory = new int[10, 10];
    public int[,] hddInventory = new int[10, 10];
    public int[] lootBoxes = new int[5];
    public List<Text> levelTexts;
    public List<Image> componentImages;
    public List<Image> tierImages;
    private int componentType = 1;
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
                this.cpuInventory[i, j] = Random.Range(1, 11);
                this.gpuInventory[i, j] = Random.Range(1, 11);
                this.ramInventory[i, j] = Random.Range(1, 11);
                this.hddInventory[i, j] = Random.Range(1, 11);
            }
        }
        SelectComponent(1);
        SelectTier(1);
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

        switch (componentType)
        {
            case 1:
                //cpu
                for (int i = 0; i < this.levelTexts.Count; i++)
                {
                    levelTexts[i].text = $"Level {i + 1}: {cpuInventory[tier - 1, i].ToString()}";
                }
                break;
            case 2:
                //gpu
                for (int i = 0; i < this.levelTexts.Count; i++)
                {
                    levelTexts[i].text = $"Level {i + 1}: {gpuInventory[tier - 1, i].ToString()}";
                }
                break;
            case 3:
                //ram
                for (int i = 0; i < this.levelTexts.Count; i++)
                {
                    levelTexts[i].text = $"Level {i + 1}: {ramInventory[tier - 1, i].ToString()}";
                }
                break;
            case 4:
                //hdd
                for (int i = 0; i < this.levelTexts.Count; i++)
                {
                    levelTexts[i].text = $"Level {i + 1}: {hddInventory[tier - 1, i].ToString()}";
                }
                break;
            default:
                break;
        }
    }
    public void SelectComponent(int component)
    {
        foreach (var img in componentImages)
        {
            img.color = Color.white;
        }
        componentImages[component - 1].color = Color.green;
        this.componentType = component;
    }
}