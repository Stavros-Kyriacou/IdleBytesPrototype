using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPU : SocketableComponent
{
    private float baseProductionBonus = 1.1f;
    public float productionBonus;
    public float productionBonusIncrement = 1.07f;
    public GPU()
    {
        this.tier = 1;
        this.level = 1;
        this.wattsIncrement = 10;
        this.productionBonus = this.baseProductionBonus;
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
        this.watts = ((tier - 1) * (this.wattsIncrement * 10)) + (level * this.wattsIncrement); 
        
        int numLoops = ((tier - 1) * 10) + (level - 1);
        float result = baseProductionBonus;
        for (int i = 0; i < numLoops; i++)
        {
            result *= productionBonusIncrement;
        }
        this.productionBonus = result;
    }
}