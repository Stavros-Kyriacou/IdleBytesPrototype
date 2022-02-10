using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PrestigeMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI PrestigeTokensText;
    public TextMeshProUGUI DollarsEarnedText;
    public TextMeshProUGUI TokensThisPrestigeText;
    private double DollarsThisPrestige;
    private int TokensToEarn;

    public void UpdateUI(double dollarsThisFrame)
    {
        this.DollarsThisPrestige += dollarsThisFrame;
        this.TokensToEarn = (int)this.DollarsThisPrestige / 100;

        this.DollarsEarnedText.text = $"Dollars this prestige: ${this.DollarsThisPrestige.ToString("F0")}";
        this.TokensThisPrestigeText.text = $"Tokens this prestige: {this.TokensToEarn}";
    }
    public void Prestige()
    {
        //everything that is affected by prestiging (computers, wifi, vending machine) have their own Prestige method called to reset
        //TODO: implement prestige method for the above

        //set $ to 0 on the game controller
        //add the tokens to inventory

        GameController.Instance.Prestige();
        VendingMachine.Instance.Prestige();
        Inventory.Instance.AddPrestigeTokens(this.TokensToEarn);

        this.DollarsThisPrestige = 0;
        this.TokensToEarn = 0;
        this.PrestigeTokensText.text = $"Prestige Tokens: {Inventory.Instance.PrestigeTokens}";
    }
}