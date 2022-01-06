using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motherboard
{
    public int level;
    private int maxLevel = 100;
    public double upgradeCost;
    public float upgradeCostIncrement = 1.4f;
    private float discount = 0f;
    public float Discount
    {
        get
        {
            return 1 - this.discount;
        }
    }
    private float discountIncrement = .002f;
    public float DiscountIncrement
    {
        get
        {
            return 1 - this.discount;
        }
    }
    public int watts;
    private int wattsIncrement = 10;
    public Motherboard()
    {
        this.level = 1;
        this.upgradeCost = 50;
        this.watts = 10;
    }
    public void Upgrade()
    {
        if (this.level < this.maxLevel)
        {
            this.level++;
            this.upgradeCost *= this.upgradeCostIncrement;
            this.discount += this.discountIncrement;
            this.watts += this.wattsIncrement;
        }
    }
    public int GetNextLevelWatts()
    {
        return this.wattsIncrement * (this.level + 1);
    }
}