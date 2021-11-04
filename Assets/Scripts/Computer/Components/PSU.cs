using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSU
{
    public int level;
    public double upgradeCost;
    public float upgradeCostIncrement = 1.1f;
    public float productionBonus = 1.1f;
    public float productionBonusIncrement = .01f;
    public int maxWatts = 2000; //equal to having all tier 10, level 10 components
    public int currentWatts; //the local maximum
    public int wattIncrement = 5;
    public PSU()
    {
        this.level = 1;
        this.upgradeCost = 50;
        this.currentWatts = 100;
    }
    public void Upgrade()
    {
        this.level++;
        this.upgradeCost *= this.upgradeCostIncrement;
        this.productionBonus += this.productionBonusIncrement;
        if (this.currentWatts < this.maxWatts)
        {
            this.currentWatts += this.wattIncrement;
            if (this.currentWatts >= this.maxWatts)
            {
                this.currentWatts = this.maxWatts;
            }
        }
    }
}
