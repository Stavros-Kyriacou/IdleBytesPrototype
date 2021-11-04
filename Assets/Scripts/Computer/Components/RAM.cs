using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAM : SocketableComponent
{
    public double dollarsPerSec = 10;
    public float dollarsPerSecIncrement;
    public RAM()
    {
        this.tier = 1;
        this.level = 1;
        this.watts = 10;
    }

}
