using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PerkMenu : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI PrestigeTokensText;

    [Header("Perk PopupMenu")]
    public RectTransform PerkPopupMenu;
    public TextMeshProUGUI PerkTitleText;
    public TextMeshProUGUI PerkLevelText;
    public TextMeshProUGUI PerkDescriptionText;
    public TextMeshProUGUI PerkCostText;
    public Button BuyPerkButton;

    //Local Variables
    private Perk SelectedPerk;


    private void Start()
    {
        Inventory.Instance.OnPrestigeTokensChanged.AddListener(UpdateTokensUI);
    }
    public void UpdateTokensUI()
    {
        this.PrestigeTokensText.text = $"Prestige Tokens: {Inventory.Instance.PrestigeTokens}";
    }
    public void TogglePerkPopup(bool visible)
    {
        if (visible)
        {
            PerkPopupMenu.anchoredPosition = Vector2.zero;
        }
        else
        {
            PerkPopupMenu.anchoredPosition = Vector2.up * 1000;
        }
    }
    public void UpdatePerkPopupUI(Perk selectedPerk)
    {
        this.PerkTitleText.text = selectedPerk.Title;
        this.PerkLevelText.text = $"Level: {selectedPerk.currentLevel}/{selectedPerk.MaxLevel}";
        this.PerkDescriptionText.text = selectedPerk.Description;
        this.PerkCostText.text = $"Cost: {selectedPerk.Cost} Tokens";

        this.SelectedPerk = selectedPerk;
    }
    public void BuyPerk()
    {
        if (this.SelectedPerk != null)
        {
            if (this.SelectedPerk.currentLevel < this.SelectedPerk.MaxLevel)
            {
                if (Inventory.Instance.RemovePrestigeTokens(this.SelectedPerk.Cost))
                {
                    this.SelectedPerk.IncreaseLevel();
                    UpdatePerkPopupUI(this.SelectedPerk);
                }
                else
                {
                    Debug.Log("You do not have enough prestige tokens!!");
                }
            }
            else
            {   
                Debug.Log("Perk is already max level, cannot upgrade further");
            }
        }
        else
        {
            Debug.Log("Perk not selected");
        }
    }
}