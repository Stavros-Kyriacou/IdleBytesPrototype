using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDD : SocketableComponent
{
    public float offlineProductionBonus = 10;
    public float offlineProductionBonusIncrement;
    public HDD()
    {
        this.tier = 1;
        this.level = 1;
        this.watts = 10;
    }
}
