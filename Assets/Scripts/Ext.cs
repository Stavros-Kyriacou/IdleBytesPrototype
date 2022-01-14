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
}