using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ext : MonoBehaviour
{
    public static string ComponentType(int type)
    {
        string componentString = "";
        switch (type)
        {
            case 1:
                componentString = "CPU";
                break;
            case 2:
                componentString = "GPU";
                break;
            case 3:
                componentString = "RAM";
                break;
            case 4:
                componentString = "HDD";
                break;
            default:
                componentString = "Null";
                break;
        }
        return componentString;
    }
    public static int RollWeights(List<int> weights)
    {
        //Takes an list of item weights and makes a roll on the loot table
        //Returns the index of the item rolled in the array
        int total = 0;
        int index = 0;

        foreach (var weight in weights)
        {
            total += weight;
        }

        int roll = UnityEngine.Random.Range(0, total + 1);

        for (int i = 0; i < weights.Count; i++)
        {
            if (roll <= weights[i])
            {
                index = i;
                break;
            }
            else
            {
                roll -= weights[i];
            }
        }
        return index;
    }
}