using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VendingMachine : MonoBehaviour
{
    public static VendingMachine Instance;
    private int level = 1;
    private int maxLevel = 20;
    private double upgradeCost = 100;
    private float upgradeCostIncrement = 1.2f;
    private float hungerBonus = 1.1f;
    private float hungerBonusIncrement = 0.1f;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI hungerBonusText;

    private void OnEnable()
    {
        UpdateUI();
    }
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
    public double GetHungerBonus(float hunger)
    {
        return this.hungerBonus + hunger;
    }
    public void Upgrade()
    {
        if (level < maxLevel)
        {

            if (GameController.Instance.dollars >= upgradeCost)
            {
                GameController.Instance.dollars -= upgradeCost;
                level++;
                upgradeCost *= upgradeCostIncrement;
                hungerBonus += hungerBonusIncrement;
            }
            else
            {
                Debug.Log("Not enough money to purchase vending machine upgrade");
            }
        }
        else
        {
            Debug.Log("Vending Machine at max level");
        }
        UpdateUI();
    }
    public void UpdateUI()
    {
        levelText.text = $"Level: {level}";
        upgradeCostText.text = $"Upgrade Cost: ${upgradeCost.ToString("F0")}";
        hungerBonusText.text = $"Hunger Bonus: {hungerBonus}";
    }
    public void Prestige()
    {
        this.level = 1;
        this.hungerBonus = 1.1f;
        this.upgradeCost = 100;
        
        this.UpdateUI();
    }
}