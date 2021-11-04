using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    //Computer
    [HideInInspector] public int level = 1;
    [HideInInspector] public double dollarsPerSec = 10;
    public double totalDPS
    {
        get
        {
            return (this.dollarsPerSec + this.CPU.dollarsPerSec) * this.PSU.productionBonus;
        }
    }
    private float dollarsPerSecIncrement = 1.2f;
    private double upgradeCost = 100;
    public double discountedCost
    {
        get
        {
            return this.upgradeCost * this.MOB.Discount;
        }
    }
    private float upgradeCostIncrement = 1.1f;
    public Motherboard MOB;
    public PSU PSU;
    public CPU CPU;
    public GPU GPU;
    public RAM RAM;
    public HDD HDD;
    private void Start()
    {
        MOB = new Motherboard();
        PSU = new PSU();
        CPU = new CPU();
        GPU = new GPU();
        RAM = new RAM();
        HDD = new HDD();
    }
    public void Upgrade(int upgradeType)
    {
        switch (upgradeType)
        {
            case 1:
                if (GameController.Instance.dollars >= this.discountedCost)
                {
                    GameController.Instance.dollars -= this.discountedCost;
                    level++;
                    this.upgradeCost *= this.upgradeCostIncrement;
                    this.dollarsPerSec *= this.dollarsPerSecIncrement;
                }
                else
                {
                    Debug.Log("Not enough money to purchase!");
                }
                break;
            case 2:
                if (GameController.Instance.dollars >= this.MOB.upgradeCost)
                {
                    GameController.Instance.dollars -= this.MOB.upgradeCost;
                    this.MOB.Upgrade();
                }
                break;
            case 3:
                if (GameController.Instance.dollars >= this.PSU.upgradeCost)
                {
                    GameController.Instance.dollars -= this.PSU.upgradeCost;
                    this.PSU.Upgrade();
                }
                break;
            default:
                Debug.LogError("Upgrade type not set on computer stats screen");
                break;
        }
    }
    public int GetWattUsage()
    {
        return this.MOB.watts + this.CPU.watts + this.GPU.watts + this.RAM.watts + this.HDD.watts;
    }
    public void SocketComponent()
    {
        //reference to the component you want to socket
        //get the watts of that component

        //get the max watts allowed by PSU
        //get the current watts of PSU
        
        //if comp watts + current <= max watts
            //socket component
        //if comp watts + current > max watts
            //cannot socket
        
    }
}
