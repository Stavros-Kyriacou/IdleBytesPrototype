using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    //[rows, columns]
    //[tier, level]
    public int[,] cpuInventory = new int[10, 10];
    public int[,] gpuInventory = new int[10, 10];
    public int[,] ramInventory = new int[10, 10];
    public int[,] hddInventory = new int[10, 10];
    public int[] lootBoxes = new int[5];
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        // cpuInventory[0,0] = 1;
        // cpuInventory[0,1] = 2;
        // cpuInventory[0,2] = 3;
        // cpuInventory[0,3] = 4;
        // cpuInventory[0,4] = 5;
        // cpuInventory[0,5] = 6;
        // cpuInventory[0,6] = 7;
        // cpuInventory[0,7] = 8;
        // cpuInventory[0,8] = 9;
        // cpuInventory[0,9] = 10;

        for (int i = 0; i < this.cpuInventory.GetLength(0); i++)
        {
            for (int j = 0; j < this.cpuInventory.GetLength(1); j++)
            {
                this.cpuInventory[i, j] = Random.Range(1, 11);
            }
        }
        // for (int i = 0; i < this.gpuInventory.GetLength(0); i++)
        // {
        //     for (int j = 0; j < this.gpuInventory.GetLength(1); j++)
        //     {
        //         this.gpuInventory[i, j] = Random.Range(1, 11);
        //     }
        // }
        // for (int i = 0; i < this.ramInventory.GetLength(0); i++)
        // {
        //     for (int j = 0; j < this.ramInventory.GetLength(1); j++)
        //     {
        //         this.ramInventory[i, j] = Random.Range(1, 11);
        //     }
        // }
        // for (int i = 0; i < this.hddInventory.GetLength(0); i++)
        // {
        //     for (int j = 0; j < this.hddInventory.GetLength(1); j++)
        //     {
        //         this.hddInventory[i, j] = Random.Range(1, 11);
        //     }
        // }
        PrintInventory();
    }
    public void PrintInventory()
    {
        string temp = "";
        Debug.Log("Printing CPU inventory");
        for (int i = 0; i < this.cpuInventory.GetLength(0); i++)
        {
            for (int j = 0; j < this.cpuInventory.GetLength(1); j++)
            {
                temp += this.cpuInventory[i, j];
                temp += ", ";
            }
            Debug.Log(temp);
            temp = "";
        }

    }

}
