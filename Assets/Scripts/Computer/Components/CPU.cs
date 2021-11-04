using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU : SocketableComponent
{
    public double dollarsPerSec = 10;
    public float dollarsPerSecIncrement;

    public CPU()
    {
        this.tier = 1;
        this.level = 1;
        this.watts = 10;
    }
}
