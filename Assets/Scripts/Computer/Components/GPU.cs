using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPU : SocketableComponent
{
    public float productionBonus = 1.1f;
    public float productionBonusIncrement;
    public GPU()
    {
        this.tier = 1;
        this.level = 1;
        this.watts = 10;
    }
}


