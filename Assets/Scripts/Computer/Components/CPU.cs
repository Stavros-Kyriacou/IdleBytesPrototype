using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU : SocketableComponent
{
    private double baseDollarsPerSec = 10;
    public double dollarsPerSec;
    public float dollarsPerSecIncrement = 1.2f;

    public CPU()
    {
        this.tier = 1;
        this.level = 1;
        this.wattsIncrement = 5;
        this.dollarsPerSec = this.baseDollarsPerSec;
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
        float result = (float)baseDollarsPerSec;
        for (int i = 0; i < numLoops; i++)
        {
            result *= dollarsPerSecIncrement;
        }
        this.dollarsPerSec = result;
    }
}