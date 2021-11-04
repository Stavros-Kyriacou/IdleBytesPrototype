using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketableComponent
{
    public int tier;
    public int level;
    public int watts;
    public int MOBLevelRequirement
    {
        get
        {
            if (this.tier == 1)
            {
                return 1;
            }
            else
            {
                return this.tier * 10;
            }
        }
    }
}
