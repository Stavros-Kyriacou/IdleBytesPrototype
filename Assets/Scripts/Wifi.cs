using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wifi : MonoBehaviour
{
    public static Wifi Instance;

    [HideInInspector] public int Level;
    public int SupportedComputers;
    public int MaxComputers;
    public int ProductionBonus;
    public double UpgradeCost;
    private float UpgradeCostIncrement = 1.1f;

    [Header("UI")]
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI ComputersOwnedText;
    public TextMeshProUGUI SupportedComputersText;
    public TextMeshProUGUI MaxComputersText;
    public TextMeshProUGUI UpgradeCostText;



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
        this.Level = 1;
        this.SupportedComputers = 1;
        UpdateUI();
    }
    public void Upgrade()
    {
        if (GameController.Instance.RemoveDollars(this.UpgradeCost))
        {
            //increase the level
            //increment upgrade cost

            //increase supported computers if possible

            this.Level++;
            this.UpgradeCost *= this.UpgradeCostIncrement;

            if ((this.Level % 5) == 0 && this.SupportedComputers < this.MaxComputers)
            {
                this.SupportedComputers = (this.Level / 5) + 1;
            }
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }
    public void UpdateUI()
    {
        this.LevelText.text = $"Level: {this.Level}";
        this.SupportedComputersText.text = $"Supported Computers: {this.SupportedComputers}";
        this.UpgradeCostText.text = $"Cost: ${this.UpgradeCost.ToString("F0")}";
    }
}