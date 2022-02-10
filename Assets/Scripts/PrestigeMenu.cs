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

    public void UpdateUI(double dollarsThisFrame)
    {
        this.DollarsThisPrestige += dollarsThisFrame;
        this.DollarsEarnedText.text = $"Dollars this prestige: ${this.DollarsThisPrestige.ToString("F0")}";
    }
}
