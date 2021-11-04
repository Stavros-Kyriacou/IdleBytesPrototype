using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public int researchSpeed;
    public int productionBonus;
    public int scrapCostReduction;
    public int gemBonus;
    public Text researchSpeedText;
    public Text productionBonusText;
    public Text scrapCostReductionText;
    public Text gemBonusText;

    private void Awake()
    {
        Instance = this;
        UpdateUI();
    }
    public void UpdateStats()
    {
        this.researchSpeed = PerkTree.Instance.perks[0].currentLevel * 10;
        this.productionBonus = PerkTree.Instance.perks[1].currentLevel * 3;
        this.scrapCostReduction = PerkTree.Instance.perks[2].currentLevel * 10;
        this.gemBonus = PerkTree.Instance.perks[3].currentLevel * 5;
        UpdateUI();
    }
    public void UpdateUI()
    {
        this.researchSpeedText.text = string.Format("Research Speed: +{0}%", this.researchSpeed);
        this.productionBonusText.text = string.Format("Production Bonus: +{0}%", this.productionBonus);
        this.scrapCostReductionText.text = string.Format("Scrap Cost Reduction: -{0}%", this.scrapCostReduction);
        this.gemBonusText.text = string.Format("Gem Earnings: +{0}%", this.gemBonus);
    }
}
