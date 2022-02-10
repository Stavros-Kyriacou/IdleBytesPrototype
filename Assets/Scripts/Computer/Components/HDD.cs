using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDD : SocketableComponent
{
    private float baseOfflineProdBonus = 10f;
    public float offlineProductionBonus;
    public float offlineProductionBonusIncrement = 1.1f;
    public HDD()
    {
        this.tier = 1;
        this.level = 1;
        this.wattsIncrement = 3;
        this.offlineProductionBonus = this.baseOfflineProdBonus;
        CalculateStats();
    }
    public void Change(int tier, int level)
    {
        this.tier = tier;
        this.level = level;
        CalculateStats();
    }
    public void CalculateStats()
    {
        if (this.tier == 0 && this.level == 0)
        {
            this.watts = 0;
            this.offlineProductionBonus = 0;
        }
        else
        {
            this.watts = ((tier - 1) * (this.wattsIncrement * 10)) + (level * this.wattsIncrement);

            int numLoops = ((tier - 1) * 10) + (level - 1);
            float result = this.baseOfflineProdBonus;
            for (int i = 0; i < numLoops; i++)
            {
                result *= offlineProductionBonusIncrement;
            }
            this.offlineProductionBonus = result;
        }
    }
}
